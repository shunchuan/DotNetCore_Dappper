using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Logging;

namespace DotNetCore_Dappper.Infrastructure.Filter
{
    public class HttpGlobalExceptionFilter : IExceptionFilter
    {

        private readonly ILogger _logger;

        public HttpGlobalExceptionFilter(ILogger logger)
        {
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            _logger.LogError("触发了全局Exception");

            _logger.LogError(context.Exception,"触发了全局Exception",null );
        }
    }
}
