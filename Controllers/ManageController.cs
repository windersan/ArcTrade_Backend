using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ArcTrade.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManageController : ControllerBase
    {
        // GET: api/Manage
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Manage/<userid>
        [HttpGet("{userid}", Name = "Get")]
        public IActionResult Get(int userid)
        {
            List<Application> applications = new List<Application>();
            ApplicationService svc = new ApplicationService();
            AuthenticationService auth_svc = new AuthenticationService();

            if (auth_svc.Authorize(userid))
            {
                List<int> applicants = svc.GenerateApplicants();
                foreach (int applicant in applicants)
                {
                    Application application = svc.GenerateApplication(applicant);
                    applications.Add(application);
                }

                return Ok(applications);
            }

            else
                return Unauthorized();
        }

        // POST: api/Manage
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Manage/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
