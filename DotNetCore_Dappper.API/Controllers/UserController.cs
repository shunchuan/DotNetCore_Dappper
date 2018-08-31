using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DotNetCore_Dappper.Infrastructure.Extensions;
using DotNetCore_Dappper.Infrastructure.Jwt;
using DotNetCore_Dappper.Model;
using DotNetCore_Dappper.Model.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DotNetCore_Dappper.API.Controllers
{
    [Produces("application/json")]
    [Route("api/User")]
    public class UserController : Controller
    {
        // GET: api/User
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [AllowAnonymous]
        // GET: api/User/5
        [HttpGet("{username}/{password}", Name = "Get")]
        public async Task<IActionResult> Get(string username,string password)
        {
            var responseBase = new ResponseBase("1", "",
                new JwtManager().GenerateToken(new JwtClaimModel() {UserName = username,RoleId = "TheRoleID", RoleName = "TheRoleName"}));
            return
                responseBase.ToActionResult();
        }
        
        // POST: api/User
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }
        
        // PUT: api/User/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
