using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotNetCore_Dappper.Infrastructure.Configurate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace DotNetCore_Dappper.Infrastructure.Attribute
{
    public class ApiPolicyAttribute: AuthorizeAttribute
    {
        public bool AuthorizeCore(HttpContext actionContext)
        {
            //前端请求api时会将token存放在名为"auth"的请求头中
            var authHeader = from h in actionContext.Request.Headers
                where h.Key == "auth"
                select h.Value.FirstOrDefault();
            string token = authHeader.FirstOrDefault();
            if (!string.IsNullOrEmpty(token))
            {
                try
                {
                    //对token进行解密
                    string secureKey = ConfigManager.GetValue("Jwt:JwtKey");
                    //AuthInfo authInfo = JWT.JsonWebToken.DecodeToObject<AuthInfo>(token, secureKey);
                    //if (authInfo != null)
                    //{
                    //    //将用户信息存放起来，供后续调用
                    //    actionContext.RequestContext.RouteData.Values.Add("auth", authInfo);
                    //    return true;
                    //}
                    //else
                    //    return false;
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            else
                return false;
        }
    }
}
