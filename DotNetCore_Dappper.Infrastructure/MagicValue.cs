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
        /// 
        /// </summary>
        public static string CONFIG_DATABASE_MODEL_KEY => "CONFIG_DATABASE_MODEL_KEY";
    }
}
