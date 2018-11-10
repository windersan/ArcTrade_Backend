using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ArcTrade.Controllers
{
    [Route("api/[controller]")]
    public class ApplicationsController : Controller
    {
        // GET: api/<controller>
        [HttpGet]
        public string Get()
        {
            return "";
        }

        // GET api/<controller>/<userid>
        [HttpGet("{id}", Name = "GetApplication")]
        public IActionResult Get(int id)
        {
            ApplicationService svc = new ApplicationService();
            AuthenticationService auth_svc = new AuthenticationService();

            if (auth_svc.Authorize(id))
            {
                Application application = svc.GenerateApplication(id);

                return Ok(application);
            }

            else
                return Unauthorized();
        }

        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody]Application application)
        {
            ApplicationService svc = new ApplicationService();

            svc.Upload(application);

            return;
        }

        // PUT api/<controller>/<userid>
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] UploadedResume resume)
        {
            String _ConnectionString = ADO.conn_str;
            SqlConnection conn = new SqlConnection(_ConnectionString);
            conn.Open();

            string SqlQuery = "update Applications set ResumeId = " + resume.Id + " where UserId = " + id;
            SqlCommand cmd = new SqlCommand(SqlQuery, conn);
            cmd.ExecuteNonQuery();

            conn.Close();
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
