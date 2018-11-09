using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ArcTrade.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        // GET api/auth
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {     
            return new string[] { "success" };       
        }

        // GET api/auth/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            AuthenticationService svc = new AuthenticationService();
            svc.Authorize(id);

            return Ok();
        }

        // POST api/auth
        [HttpPost]
        public ActionResult Post([FromBody] UsernamePasswordPair auth)
        {
            AuthenticationService svc = new AuthenticationService();

            User user = svc.Authenticate(auth);

            if (user.Id !=0)
                return Ok(user);
            else
                return Unauthorized();

        }

        // PUT api/auth/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] User user)
        {
            //AuthenticationService svc = new AuthenticationService();
            //svc.Authorize(id);

            //if (user.Authorization != 0)
            //{

            //}
        }

        // DELETE api/auth/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
