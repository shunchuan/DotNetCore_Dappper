using System;
using System.Collections.Generic;
using System.Text;
using DotNetCore_Dappper.Domain.IRepository;

namespace DotNetCore_Dappper.Domain.MySql_Repository
{
    public class UserInfo: IUserInfo
    {
        public IEnumerable<string> GetUserName()
        {
            string sql = "select  name from userInfo";
            return DbHelper.Query<string>(sql);
        }
    }
}
