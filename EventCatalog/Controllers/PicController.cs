using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventCatalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

   
    public class PicController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;
        public PicController(IWebHostEnvironment env)
        {
            _env = env;
        }


        [HttpGet("{pictureurl}")]
        public IActionResult GetImage(string pictureurl)
        {
            var webRoot = _env.WebRootPath;
            var path = Path.Combine($"{webRoot}/Uploads/",$"{pictureurl}");
            var buffer = System.IO.File.ReadAllBytes(path);
            return File(buffer, "image/jpeg");

        }
    }
}