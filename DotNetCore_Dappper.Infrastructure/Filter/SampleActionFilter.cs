using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotNetCore_Dappper.Infrastructure.Extensions;
using DotNetCore_Dappper.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DotNetCore_Dappper.Infrastructure.Filter
{
    public class SampleActionFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var errorMessages = new List<string>();
                foreach (var key in context.ModelState.Keys)
                {
                    var state = context.ModelState[key];
                    var errorModel = state?.Errors?.First();
                    if (errorModel != null)
                        errorMessages.Add($"{key}:{errorModel.ErrorMessage}");
                }
                var result = new ResponseBase(0, null, errorMessages);
                context.Result = result.ToActionResult();
                return;
            }
        }
    }
}
