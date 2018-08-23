using System;
using System.Collections.Generic;
using System.Text;
using DotNetCore_Dappper.Domain;
using DotNetCore_Dappper.Domain.Entity;
using DotNetCore_Dappper.Domain.IRepository;
using MSSql = DotNetCore_Dappper.Domain.MSSql_Repository;

namespace DotNetCore_Dappper.Business
{
    public class UserBusiness
    {
        public IEnumerable<string> GetUserName()
        {
            var userInfo = DataAccess<IUserInfoRepository>.CreateObject("UserInfo");
            return userInfo.GetUserName();
        }

        public IEnumerable<UserInfo> GetUserInfos()
        {
            var userInfo = DataAccess<IUserInfoRepository>.CreateObject("UserInfo");
            return userInfo.GetAll<UserInfo>();
        }
    }
}
