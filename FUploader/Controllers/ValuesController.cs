using FUploader.DataLayer.APIResources;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FUploader.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UploderController : ControllerBase
    {
        // GET: api/<UploderController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<UploderController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<UploderController>
        [HttpPost]
        public IResult uploadFiles([FromBody] UploadTask uploadtask)
        {
            if (!ModelState.IsValid)
            {
                return Results.BadRequest();
            }

            return Results.Json(uploadtask);
        
        }

        // PUT api/<UploderController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<UploderController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
