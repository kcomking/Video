using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Serialization;
using Swashbuckle.AspNetCore.Swagger;
using Video.Api.Extensions;
using Video.Core.Entities;
using Video.Core.Interface;
using Video.Infrastructrue.Database;
using Video.Infrastructrue.Repository;
using Video.Infrastructrue.Services;

namespace Video.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddIdentity<Account, Role>()
                .AddUserStore<AccountRepository>()
                .AddRoleStore<RoleRepository>()
                .AddClaimsPrincipalFactory<UserClaimsPrincipalFactory>()
                .AddDefaultTokenProviders();
            services.AddJwtAuthentication(Configuration);
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                })
                .AddFluentValidation();

            services.AddAuthorization();
            //services.ConfigureApplicationCookie(options => options.LoginPath = "/signin");
            services.Configure<IdentityOptions>(options =>
            {
                // 我们在 SigninUserViewModel 中的 PasswordRules 类中进行验证
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 0;
                options.Password.RequireDigit = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;

                //                options.User.RequireUniqueEmail = true;
            });

            #region Swagger            
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v0.1.0",
                    Title = "Video.Api",
                    Description = "框架说明文档",
                    TermsOfService = "None",
                    Contact = new Swashbuckle.AspNetCore.Swagger.Contact { Name = "Video.Api", Email = "xxxx@xxxx.com", Url = "" }
                });
            });            
            #endregion
             
            //
            services.AddDbContext<DBContext>(p => { p.UseSqlite(Configuration.GetConnectionString("ConnectionString")); });
            services.AddHttpsRedirection(option =>
            {
                option.RedirectStatusCode = StatusCodes.Status307TemporaryRedirect;
                option.HttpsPort = 5000;
            });

            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
             

            services.AddTransient<ITypeHelperService, TypeHelperService>();
            services.AddHsts(p =>
            {
                p.Preload = true;
                p.IncludeSubDomains = true;
                p.MaxAge = TimeSpan.FromDays(60);
                p.ExcludedHosts.Add("example.com");
                p.ExcludedHosts.Add("www.example.com");
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
               // app.UseDeveloperExceptionPage();
               app.UseVideoExceptionHandler(loggerFactory);
            }
            else
            {
                app.UseHsts();
            }
            #region Swagger
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "ApiHelp V1");
            });
            #endregion
            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
