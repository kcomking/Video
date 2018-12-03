using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Video.Api.Extensions
{
    public static class ExceptionHandlingExtensions
    {
        public static void UseVideoExceptionHandler(this IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            app.UseExceptionHandler(builder =>
            {
                builder.Run(async context =>
                {
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    context.Response.ContentType = "application/json";

                    var ex = context.Features.Get<IExceptionHandlerFeature>();
                    if (ex != null)
                    {
                        var logger = loggerFactory.CreateLogger("BlogDemo.Api.Extensions.ExceptionHandlingExtensions");
                        logger.LogError(500, ex.Error, ex.Error.Message);
                    }
                    var result = JsonConvert.SerializeObject(new { error = ex?.Error?.Message ?? "An Error Occurred.",state=false });
                    await context.Response.WriteAsync(result);
                });
            });
        }
    }
}
