using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;

namespace DotNetCore_Dappper.Infrastructure.Configurate
{
    public static class ConfigManager
    {
        private static readonly IConfiguration Config = null;
        static ConfigManager()
        {
            string currentClassDir = AppContext.BaseDirectory;
            if (Config == null)
            {
                Config = new ConfigurationBuilder()
                    .SetBasePath(currentClassDir)
                    .AddJsonFile("appsettings.json", false, true)
                    //.Add(new JsonConfigurationSource { Path = "appsettings.json", Optional = false, ReloadOnChange = true })
                    .Build();
            }
        }

        /// <summary>  
        /// 获取系统公共配置文件  
        /// </summary>  
        /// <typeparam name="T"></typeparam>  
        /// <param name="key"></param>  
        /// <returns></returns>  
        public static T GetValue<T>(string key) where T : class, new()
        {
            T sysConfig = new T();
            try
            {
                Config.GetSection(key).Bind(sysConfig);
            }
            catch (Exception ex)
            {
                sysConfig = null;
            }
            return sysConfig;
        }

        /// <summary>  
        /// 获取单一节点配置文件  
        /// </summary>  
        /// <param name="key">节点，如果是多级节点需要按照:分隔的方式传递</param>  
        /// <returns></returns>  
        public static string GetValue(string key)
        {
            return Config.GetSection(key)?.Value.ToString().Trim();
        }
    }
}
