using FUploader.Core.FireBase;
using FUploader.DataLayer.APIResources;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FUploader.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UploderController : ControllerBase
    {
        FirebaseAuth _fbauth;
        UploadManager _fbuploader;

        public UploderController(FirebaseAuth fbauth, UploadManager fbuploader)
        {
            _fbauth = fbauth;
            _fbuploader = fbuploader;    
        }
      
        [HttpPost]
        public async Task<IResult> UploadFile([FromBody] UploadTask uploadtask)
        {
            
            
            if (!ModelState.IsValid)
            {
                return Results.BadRequest();
            }

            var url = await _fbuploader.UploadFile(uploadtask.FilePath, uploadtask.Token);

            return Results.Ok(new { dowloadFrom = url });
        
        }

        [HttpPost]
        public IResult ErrorTest()
        {
            throw new Exception("Exception is generated in test goal");
        }

        [HttpPost]
        public async Task<IResult> Login([FromBody] EmailPasswordCredentional credentional)
        {
            if (!ModelState.IsValid)
            {
                return Results.BadRequest();
            }

            var token = await _fbauth.Loggin(credentional.Email, credentional.Password);

            return Results.Ok(new { token = token });
        }

    }
}
