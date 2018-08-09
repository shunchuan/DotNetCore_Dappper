
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using DotNetCore_Dappper.Model;
using DotNetCore_Dappper.Model.Enmu;

namespace DotNetCore_Dappper.Infrastructure
{
    public class ReadDatabase
    {
        /// <summary>
        /// 声明一个已经是否声明自身类的对象
        /// </summary>
        private static volatile ReadDatabase _instance = null;

        /// <summary>
        /// 锁
        /// </summary>
        private static readonly object LockHelper = new object();

        private readonly string DB_CONFIG_PATH = @"\Config\System\DataBase.xml";

        /// <summary>
        /// 
        /// </summary>
        private ReadDatabase() { }

        /// <summary>
        /// 创建单实例（函数方式）
        /// </summary>
        /// <returns></returns>
        public static ReadDatabase CreateInstance
        {
            get
            {
                if (_instance == null)
                {
                    lock (LockHelper)
                    {
                        if (_instance == null)
                            _instance = new ReadDatabase();
                    }
                }

                return _instance;
            }
        }

        /// <summary>
        /// 读取数据库配置文件参数，如果本地文件未修改，则读缓存
        /// </summary>
        /// <returns></returns>
        public DatabaseModel ReaDatabaseConfig()
        {
            DatabaseModel model = new DatabaseModel();
            var fileModifyTime = File.GetLastWriteTime(Directory.GetCurrentDirectory() + DB_CONFIG_PATH);
            var lastModifyTime = CallContext.GetData(MagicValue.CONFIG_LAST_MODIFY_TIME_KEY) as DateTime?;

            // 读取为第一次 或 修改时间不同 或缓存的数据为null
            if (null == lastModifyTime || fileModifyTime != lastModifyTime ||
                null == CallContext.GetData(MagicValue.CONFIG_DATABASE_MODEL_KEY))
            {
                model.Dbtype = ReadTypeOfDataBase();
                model.DbConnectStr = ReadConnectionStrOfDataBase();
                CallContext.SetData(MagicValue.CONFIG_LAST_MODIFY_TIME_KEY, fileModifyTime);
                CallContext.SetData(MagicValue.CONFIG_DATABASE_MODEL_KEY, model);
            }
            else
            {
                model = CallContext.GetData(MagicValue.CONFIG_DATABASE_MODEL_KEY) as DatabaseModel;
            }

            return model;
        }

        /// <summary>
        /// 读取数据库类型 mysql, oracle, sqlserver, access
        /// </summary>
        /// <returns></returns>
        public DBTYPE ReadTypeOfDataBase()
        {
            DBTYPE dbType = DBTYPE.None;
            string path = Directory.GetCurrentDirectory() + DB_CONFIG_PATH;

            XmlDocument doc = new XmlDocument();

            string type = "";

            doc.Load(path);

            var nodes = doc.SelectSingleNode("database").ChildNodes;

            foreach (XmlNode item in nodes)
            {
                XmlElement xe = (XmlElement)item;
                if (xe.Name == "type")
                {
                    type = xe.InnerText;
                    break;
                }
            }

            switch (type.ToLower())
            {
                case "mysql":
                    dbType = DBTYPE.MySql;
                    break;
                case "oracle":
                    dbType = DBTYPE.Oracle;
                    break;
                case "sqlserver":
                    dbType = DBTYPE.SqlServer;
                    break;
                case "access":
                    dbType = DBTYPE.Access;
                    break;
            }
            return dbType;
        }

        /// <summary>
        /// 读取数据库链接字符串
        /// </summary>
        /// <returns></returns>
        public string ReadConnectionStrOfDataBase()
        {
            string path = Directory.GetCurrentDirectory() + DB_CONFIG_PATH;

            XmlDocument doc = new XmlDocument();

            string connectionStr = "";

            doc.Load(path);

            var nodes = doc.SelectSingleNode("database").ChildNodes;

            foreach (XmlNode item in nodes)
            {
                XmlElement xe = (XmlElement)item;
                if (xe.Name == "connectionStr")
                {
                    connectionStr = xe.InnerText;
                    break;
                }
            }
            return connectionStr;
        }
    }
}
