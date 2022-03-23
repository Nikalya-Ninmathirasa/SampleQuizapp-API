using Microsoft.AspNetCore.Mvc;
using UserAuthentication.Models;

namespace UserAuthentication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Imageupload : ControllerBase
    {
        public static IWebHostEnvironment _webHostEnvironment;

        public Imageupload(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpPost]
        public async  Task<string>Post([FromForm] FileUpload fileUpload)
        {
            try
            {
                if (fileUpload.Image.Length > 0)
                {
                    string path = _webHostEnvironment.WebRootPath + "\\uploads\\";
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    using (FileStream filestream = System.IO.File.Create(path + fileUpload.Image.FileName))
                    {
                        fileUpload.Image.CopyTo(filestream);
                        filestream.Flush();
                        return "Upload Done";
                    }
                }
                else
                {
                    return "Failed";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        [HttpGet ("{fileName}")]
        public async Task<IActionResult> Get([FromRoute] string fileName)
        {
            string path = _webHostEnvironment.WebRootPath + "\\uploads\\";
            var filePath = path + fileName + ".jpg";

            if (System.IO.File.Exists(filePath))
            {
                byte[] b = System.IO.File.ReadAllBytes(filePath);
                return File(b, "image/jpg");

               
            }
            return null;

        }




    }
    
} 

