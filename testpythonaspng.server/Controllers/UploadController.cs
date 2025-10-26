using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace testpythonaspng.Server.Controllers
{
    public class MyDto
    {

       // public IFormFile File { get; set; }
       public string name { get; set; }
       public IFormFile File { get; set; }
    } 
    [Route("api/[controller]")]
    [ApiController]
    public class UploadController : ControllerBase
    {

        [HttpPost]
        public async Task<IActionResult> UploadData([FromForm] MyDto myDto)
        {
            IFormFile file = Request.Form.Files[0];
            string fileName = myDto.name;
            string fullPath = Path.Combine("uploads/", fileName);

            var buffer = 1024 * 1024;
            using var stream = new FileStream(fullPath, FileMode.Create, FileAccess.Write, FileShare.None, buffer, useAsync: false);
            await file.CopyToAsync(stream);
            await stream.FlushAsync();
            string result = "OK";
            
            return Ok(result);
        }
    }
}
