using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Video.Api.Extensions;
using Video.Api.Models;
using Video.Core.Entities;
using Video.Core.Interface;
using Video.Core.Pages;
using Video.Infrastructrue.Database;
using Video.Infrastructrue.Extensions;
using Video.Infrastructrue.Services;

namespace Video.Api.Controllers
{
    [Route("api/accounts")]
    [Produces("application/json")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly UserManager<Account> _userManager;
        private readonly SignInManager<Account> _signInManager;
        private readonly IAccountRepository _accountRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly JwtIssuerOptions _jwtOptions;
        private readonly ILogger<AccountController> _logger;
        private readonly ITypeHelperService _typeHelperService; 

        public AccountController(
            UserManager<Account> userManager,
            SignInManager<Account> signInManager,
            IAccountRepository accountRepository,
            IUnitOfWork unitOfWork,
            IOptions<JwtIssuerOptions> jwtOptions,
            ILogger<AccountController> logger, ITypeHelperService typeHelperService )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _accountRepository = accountRepository;
            _unitOfWork = unitOfWork;
            _jwtOptions = jwtOptions.Value;
            _logger = logger;
            _typeHelperService = typeHelperService; 
        }

        


        [HttpGet("/GetAccounts")]
        public async Task<IActionResult> GetAccounts([FromQuery]QueryParameters query)
        {
            if (!_typeHelperService.TypeHasProperties<Account>(query.OrderBy))
            {
                return BadRequest("OrderBy fields not exist.");
            }

            if (!_typeHelperService.TypeHasProperties<Account>(query.Fields))
            {
                return BadRequest("Fields not exist.");
            }
            var account = await _accountRepository.GetPagesAsync(query);
            var accountWithFields = account.ToDynamicIEnumerable(query.Fields);
            //_logger.LogError("tests");
            //throw new Exception("test");
            return Json(accountWithFields);
        }


        private static long ToUnixEpochDate(DateTime date)
            => (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);

        [HttpPost]
        [Route("/signin")]
        public IActionResult Login([FromForm]LoginViewModel model, [FromQuery]string returnUrl)
        {
            var identity = new ClaimsIdentity(new GenericIdentity(model.UserName, "Token"), new[]
            {
                new Claim("id","123" ),
                new Claim("rol", "api_access"),
                new Claim(ClaimTypes.Name, model.UserName),

                new Claim(JwtRegisteredClaimNames.Sub, model.UserName),
                new Claim(JwtRegisteredClaimNames.Jti,  _jwtOptions.JtiGenerator()),
                new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(_jwtOptions.IssuedAt).ToString(), ClaimValueTypes.Integer64),
            });

            var handler = new JwtSecurityTokenHandler();

            var securityToken = handler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _jwtOptions.Issuer,
                Audience = _jwtOptions.Audience,
                SigningCredentials = _jwtOptions.SigningCredentials,
                NotBefore = _jwtOptions.NotBefore,
                Subject = identity,
                Expires = _jwtOptions.Expiration,
            });

            var encodedJwt = handler.WriteToken(securityToken);

            return Ok(new
            {
                status = 1,
                result = new
                {
                    id = identity.Claims.Single(c => c.Type == "id").Value,
                    auth_token = encodedJwt,
                    expires_in = (int)_jwtOptions.ValidFor.TotalSeconds
                }
            });
        }

        [HttpGet("user")]
        [Authorize]
        public IActionResult UserInfo()
        {
            return Ok(new
            {
                status = 1,
                result = new
                {
                    UserName = "dotnet_lover",
                }
            });
        }

        [HttpPost]
        [Route("/signout")]
        public async Task<IActionResult> DoSignOut()
        {
            
            if (!HttpContext.IsAuthenticated())
            {
                return BadRequest();
            }

            await _signInManager.SignOutAsync();
            return Ok();
        }

 

        [HttpPost]
        [Route("/register")]
        public async Task<IActionResult> DoRegister([FromForm]LoginViewModel userViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var newUser = new Account()
            {
                
            };

            var result = await _userManager.CreateAsync(newUser, userViewModel.Password);
            if (!result.Succeeded)
            {
                var errorMessage = string.Join(";", result.Errors.Select(err => err.Description));
                ModelState.AddModelError("UserName", errorMessage);
                return BadRequest("Register");
            }

            await _signInManager.PasswordSignInAsync(
                userViewModel.UserName,
                userViewModel.Password,
                isPersistent: false,
                lockoutOnFailure: true);
            return Ok();
        }

 
    }
}