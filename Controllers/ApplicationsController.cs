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
        public IActionResult Get()
        {
            List<Application> applications = new List<Application>();

            return Ok(applications);
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Application application = new Application();

            String _ConnectionString = ADO.conn_str;
            SqlConnection conn = new SqlConnection(_ConnectionString);
            conn.Open();
            SqlDataReader reader = null;

            string SqlQuery = "select * from applications where UserId = '" + id + "'";
            SqlCommand cmd = new SqlCommand(SqlQuery, conn);
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                //dt = reader.GetDateTime(0);
            }


            reader.Close();

            return Ok(application);
        }

        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] UploadedResume resume)
        {
            String _ConnectionString = ADO.conn_str;
            SqlConnection conn = new SqlConnection(_ConnectionString);
            conn.Open();

            string SqlQuery = "update Applications set Resumed = " + resume.Id + " where UserId = " + id;
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
