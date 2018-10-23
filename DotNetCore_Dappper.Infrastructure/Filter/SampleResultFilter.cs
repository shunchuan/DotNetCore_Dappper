using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DotNetCore_Dappper.Infrastructure.Filter
{
    public class SampleResultFilter : IResultFilter
    {
        public void OnResultExecuted(ResultExecutedContext context)
        {
            throw new NotImplementedException();
        }

        public void OnResultExecuting(ResultExecutingContext context)
        {
            
            throw new NotImplementedException();
        }
    }
}
