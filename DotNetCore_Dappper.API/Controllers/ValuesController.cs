﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetCore_Dappper.Business;
using DotNetCore_Dappper.Model.Entity;
using Microsoft.AspNetCore.Mvc;

namespace DotNetCore_Dappper.API.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        //// GET api/values
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        // GET api/values/5
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new UserBusiness().GetUserName();
        }

        //[HttpGet]
        //public IEnumerable<UserInfo> Get()
        //{
        //    return new UserBusiness().GetUserInfos();
        //}

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
