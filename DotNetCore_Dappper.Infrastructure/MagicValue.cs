using System;
using System.Collections.Generic;
using System.Text;

namespace DotNetCore_Dappper.Infrastructure
{
    public static class MagicValue
    {
        /// <summary>
        /// 数据库配置文件修改时间对应的CallContext Key
        /// </summary>
        public static string CONFIG_LAST_MODIFY_TIME_KEY => "CONFIG_LAST_MODIFY_TIME_KEY";

        /// <summary>
        /// 数据库配置实体类存储键
        /// </summary>
        public static string CONFIG_DATABASE_MODEL_KEY => "CONFIG_DATABASE_MODEL_KEY";

        /// <summary>
        /// 配置文件中Redis前缀键位置
        /// </summary>
        public static string REDIS_PREFIX_KEY => "Redis:RedisKey";

        /// <summary>
        /// Redis连接字符串
        /// </summary>
        public static string REDIS_CONNECTSTR_KEY => "Redis:ConnectStr";
    }
}
