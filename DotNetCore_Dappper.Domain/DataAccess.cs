using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using DotNetCore_Dappper.Infrastructure;
using DotNetCore_Dappper.Model;

namespace DotNetCore_Dappper.Domain
{
    public class DataAccess<T>
    {
        public static T CreateObject(string classname)
        {

            if (string.IsNullOrEmpty(classname))
                throw new ArgumentNullException();
            string name = System.IO.Path.GetFileNameWithoutExtension(typeof(T).Module.Name);

            StringBuilder sb = new StringBuilder();
            DatabaseModel model = ReadDatabase.CreateInstance.DatabaseConfig();
            sb.Append(name + ".");
            switch (model.Type.ToUpper())
            {
                case "MYSQL":
                    sb.Append("MySql");
                    break;
                case "MSSQL":
                    sb.Append("MSSql");
                    break;
            }

            sb.Append("_Repository");

            sb.Append("." + classname);
            return (T) Activator.CreateInstance(Assembly.Load(name).GetType(sb.ToString(), false));
        }
    }
}
