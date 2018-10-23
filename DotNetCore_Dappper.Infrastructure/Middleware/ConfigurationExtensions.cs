using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Text;

namespace DotNetCore_Dappper.Infrastructure.Middleware
{
    public static class ConfigurationExtensions
    {
        /// <summary>
        /// 注册中间件
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder AppUseMiddleware(this IApplicationBuilder app)
        {
            return app.UseMiddleware<SampleMiddleware>();
        }
    }
}
