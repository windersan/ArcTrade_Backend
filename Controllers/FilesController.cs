using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ArcTrade.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
       
        // GET: api/Files/5
        [HttpGet("{id}", Name = "GetFile")]
        public FileStreamResult Get(int id)
        {
            FileService svc = new FileService();

            MemoryStream ms = svc.Download(id);

            return new FileStreamResult(ms, "application/pdf");
        }
  
        [HttpGet]
        public void Get()       
        {        
        }

        // POST: api/Files
        [HttpPost]
        public IActionResult Post()
        {
            FileService svc = new FileService();
            MemoryStream ms = new MemoryStream();

            this.Request.Body.CopyTo(ms);
            int id = svc.Upload(ms);

            UploadedResume resume = new UploadedResume();
            resume.Id = id;

            return Ok(resume);
        }

        // PUT: api/Files/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] UploadedResume resume)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            
        }
    }
}
