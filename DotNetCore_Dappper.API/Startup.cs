using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetCore_Dappper.Infrastructure.Redis;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Swagger;

namespace DotNetCore_Dappper.API
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
            //添加 jwt 认证服务
            services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = "JwtBearer";
                    options.DefaultChallengeScheme = "JwtBearer";
                })
                .AddJwtBearer("JwtBearer", jwtBearerOptions =>
                {
                    jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration.GetSection("Jwt").GetValue<string>("JwtKey"))),

                        ValidateIssuer = true,
                        ValidIssuer = Configuration.GetSection("Jwt").GetValue<string>("JwtIssuer"),

                        ValidateAudience = true,
                        ValidAudience = Configuration.GetSection("Jwt").GetValue<string>("JwtAudience"),

                        ValidateLifetime = true, //validate the expiration and not before values in the token

                        ClockSkew = TimeSpan.FromMinutes(5) //5 minute tolerance for the expiration date
                    };
                });
            services.AddMvc();

            //services.AddSingleton<RedisHelper>(new RedisHelper(1));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "DotNetCore_Dapper API",
                    Description = "A simple example ASP.NET Core Web API For Dapper ORM ",
                    TermsOfService = "None",
                    Contact = new Contact { Name = "Caosc", Email = "1289158751@qq.com", Url = "https://www.woaihuaye.com" },
                    License = new License { Name = "Use under LICX", Url = "https://example.com/license" }
                });
                //为Swagger的JSON和UI设置XML注释
                var basePath = AppContext.BaseDirectory;
                var xmlPath = Path.Combine(basePath, "DotNetCore_Dappper.API.xml");
                c.IncludeXmlComments(xmlPath);
            });

            //services.AddAuthorization(options =>
            //{
            //    options.AddPolicy(
            //        "RolePolicy",
            //        authBuilder =>
            //        {
            //            authBuilder.RequireClaim("RoleId", "111");
            //        });
            //});
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseAuthentication();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseMvc();
        }
    }
}
