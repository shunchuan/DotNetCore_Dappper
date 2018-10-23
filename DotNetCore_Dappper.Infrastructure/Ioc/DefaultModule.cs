using Autofac;
using DotNetCore_Dappper.Infrastructure.Log4Net;
using DotNetCore_Dappper.Infrastructure.Redis;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Extensions.Logging;

namespace DotNetCore_Dappper.Infrastructure.Autofac
{
    public class DefaultModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {            //注入测试服务            builder.RegisterType<TestService>().As<ITestService>();     
            builder.Register(c => new Log4NetProvider("log4net.config").CreateLogger("Log4NetRepostory")).
                As<ILogger>().PropertiesAutowired().SingleInstance();
            builder.Register(c => new RedisHelper(1)).As<RedisHelper>().PropertiesAutowired().SingleInstance();
        }
    }
}