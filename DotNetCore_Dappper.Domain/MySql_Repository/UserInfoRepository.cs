using System;
using System.Collections.Generic;
using System.Text;
using DotNetCore_Dappper.Domain.IRepository;
using DotNetCore_Dappper.Domain.RepositoryBase;
using DotNetCore_Dappper.Model.Entity;

namespace DotNetCore_Dappper.Domain.MySql_Repository
{
    public class UserInfoRepository: DapperExtensionsRepositoryBase<UserInfo>,IUserInfoRepository
    {
        public IEnumerable<string> GetUserName()
        {
            string sql = "select  name from userInfo";
            return Query<string>(sql);
        }


        public IEnumerable<UserInfo> GetUserInfos()
        {
            return Query("select * from userInfo");
        }
    }
}
