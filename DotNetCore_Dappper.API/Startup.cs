using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using DotNetCore_Dappper.API.Controllers;
using DotNetCore_Dappper.Infrastructure.Filter;
using DotNetCore_Dappper.Infrastructure.Ioc;
using DotNetCore_Dappper.Infrastructure.Log4Net;
using DotNetCore_Dappper.Infrastructure.Middleware;
using DotNetCore_Dappper.Infrastructure.Redis;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
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
        public IServiceProvider ConfigureServices(IServiceCollection services)
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

            // 替换框架所有者，用ServiceBasedControllerActivator替换DefaultControllerActivator（意味着框架现在会尝试从IServiceProvider中解析控制器实例）
            services.Replace(ServiceDescriptor.Transient<IControllerActivator, ServiceBasedControllerActivator>());

            services.AddMvc(options =>
            {
                // add filters
                //options.Filters.Add<HttpGlobalExceptionFilter>();
                options.Filters.Add(new SampleActionFilter());
            });

            // add swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "DotNetCore_Dapper API",
                    Description = "A simple example ASP.NET Core Web API For Dapper ORM ",
                    TermsOfService = "None",
                    Contact = new Contact { Name = "Caosc", Email = "1289158751@qq.com", Url = "https://github.com/shunchuan" },
                    License = new License { Name = "Use under LICX", Url = "https://example.com/license" }
                });
                //为Swagger的JSON和UI设置XML注释
                var basePath = AppContext.BaseDirectory;
                var xmlPath = Path.Combine(basePath, "DotNetCore_Dappper.API.xml");
                c.IncludeXmlComments(xmlPath);
            });

            // add autofac Ioc container
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterModule<DefaultModule>();
            AutofacRegisterProperties(containerBuilder);
            containerBuilder.Populate(services);
            var container = containerBuilder.Build();
            return container.Resolve<IServiceProvider>(); //return new AutofacServiceProvider(container);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            //工厂方式注册日志组件
            //loggerFactory.AddLog4Net();
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
            app.AppUseMiddleware();
        }

        /// <summary>
        /// 使用autofac进行属性注入
        /// blog:http://www.10tiao.com/html/337/201801/2660101085/1.html
        /// </summary>
        /// <param name="containerBuilder"></param>
        public void AutofacRegisterProperties(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterType<ValuesController>().PropertiesAutowired();
            containerBuilder.RegisterType<UserController>().PropertiesAutowired();
        }
    }
}
