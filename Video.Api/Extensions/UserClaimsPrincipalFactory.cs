using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Video.Core.Entities;

namespace Video.Api.Extensions
{
    public class UserClaimsPrincipalFactory : IUserClaimsPrincipalFactory<Account>
    {
        public Task<ClaimsPrincipal> CreateAsync(Account user)
        {
            var claims = new List<Claim> {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString(), ClaimValueTypes.Integer32),
                new Claim(ClaimTypes.Name, user.Name, ClaimValueTypes.String),
                new Claim("SigninTime", System.DateTime.UtcNow.Ticks.ToString(), ClaimValueTypes.Integer64)
            };

            var identity = new ClaimsIdentity(claims, IdentityConstants.ApplicationScheme);
            return Task.FromResult(new ClaimsPrincipal(identity));
        }
    }
}