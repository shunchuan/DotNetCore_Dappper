using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Hosting;

namespace DotNetCore_Dappper.Infrastructure.Filter
{
    public class HttpGlobalExceptionFilter : IExceptionFilter
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly ILogger _logger;

        public HttpGlobalExceptionFilter(IHostingEnvironment hostingEnvironment, ILogger logger)
        {
            _hostingEnvironment = hostingEnvironment;
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            if (!_hostingEnvironment.IsDevelopment())
            {
                return;
            }
            _logger.LogError("触发了全局Exception");

            _logger.LogError(context.Exception,"触发了全局Exception",null );
        }
    }
}
