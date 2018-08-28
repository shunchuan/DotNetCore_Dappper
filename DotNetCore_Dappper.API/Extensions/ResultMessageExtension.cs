using System;
using System.Collections.Generic;
using System.Text;
using DotNetCore_Dappper.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace DotNetCore_Dappper.API.Extensions
{
    public static class ResultMessageExtension
    {
        /// <summary>
        /// ResponseBase根据状态自动转换为IActionResult
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        public static IActionResult ToActionResult(this ResponseBase result)
        {
            switch (result.Status)
            {
                case "1":
                {
                    return new OkObjectResult(result);
                }
                case "0":
                {
                    return new BadRequestObjectResult(result);
                }
                default:
                    return new OkObjectResult(result);
            }
        }
    }
}
