using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using DotNetCore_Dappper.Infrastructure.Configurate;
using DotNetCore_Dappper.Model.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace DotNetCore_Dappper.Infrastructure.JWT
{
    public class JwtManager
    {
        private static JwtModel GetJwtModel()
        {
            if (CallContext.GetData(MagicValue.JWT_CONFIG_MODEL_KEY) is JwtModel jwtModel)
            {
                return jwtModel;
            }
            
            var jwtToken = ConfigManager.GetValue<JwtModel>("Jwt");
            if (string.IsNullOrEmpty(jwtToken.JwtKey?.Trim()))
            {
                throw new ArgumentNullException();
            }
            CallContext.SetData(MagicValue.JWT_CONFIG_MODEL_KEY, jwtToken);
            return jwtToken;
        }

        public JwtDto GenerateToken(JwtClaimModel model)
        {
            var jwtToken = GetJwtModel();

            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtToken.JwtKey));
            Type t = model.GetType();
            var claims = new List<Claim>();
            foreach (var property in t.GetProperties())
            {
                claims.Add(new Claim(property.Name, (string) t.GetProperty(property.Name).GetValue(model) ?? ""));
            }

            claims.Add(new Claim(JwtRegisteredClaimNames.Nbf,
                new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds().ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Exp,
                new DateTimeOffset(DateTime.Now.AddDays(1)).ToUnixTimeSeconds().ToString()));

            var token = new JwtSecurityToken(
                issuer: jwtToken.JwtIssuer,
                audience: jwtToken.JwtAudience,
                claims: claims,
                notBefore: DateTime.Now,
                expires: DateTime.Now.AddDays(jwtToken.JwtExpireDays),
                signingCredentials: new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256)
            );
            return new JwtDto()
            {
                Type = JwtBearerDefaults.AuthenticationScheme,
                AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
                Expires = DateTime.Now.AddDays(jwtToken.JwtExpireDays)
            };
        }

        //public bool 
    }
}
