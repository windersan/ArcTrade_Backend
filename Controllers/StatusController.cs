using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ArcTrade.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusController : ControllerBase
    {


        // PUT: api/Status/5
        [HttpPut("{id}", Name = "UpdateStatus")]
        public void Put(int id, [FromBody] ApplicationStatus status)
        {
            SqlConnection conn = new SqlConnection(ADO.conn_str);
            try
            {
                conn.Open();

                string SqlQuery = "update Applications set ApplicationStatus = '" + status.Status + "' where UserId = " + id;
                SqlCommand cmd = new SqlCommand(SqlQuery, conn);
                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex) { }
            finally { conn.Close(); }
        }
    }
}
