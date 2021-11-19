using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MKTFY.Models.ViewModels.Upload;
using MKTFY.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MKTFY.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        private readonly IUploadService _uploadService;

        public UploadController(IUploadService uploadService)
        {
            _uploadService = uploadService;
        }

        /// <summary>
        /// Upload 1 or more files
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<List<UploadResultVM>>> UploadImage()
        {
            // Validate the file types
            var supportedTypes = new[] { ".png", ".gif", ".jpg", ".jpeg" };
            var uploadedExtensions = Request.Form.Files.Select(i => System.IO.Path.GetExtension(i.FileName));
            var mismatchFound = uploadedExtensions.Any(i => !supportedTypes.Contains(i));
            if (mismatchFound)
                return BadRequest(new { message = "At least one uploaded file is not a valid image type" });
            // Validate the file size
            int maxAllowedFileSize = 15000000;
            var uploadedFileSizes = Request.Form.Files.Select(i => i.Length);
            var exceedsFileSize = uploadedFileSizes.Any(i => i > maxAllowedFileSize);
            if (exceedsFileSize)
                return BadRequest(new { message = $"At least one uploaded file exceeds the max file size of {maxAllowedFileSize/1000} KB " });

            var results = await _uploadService.UploadFiles(Request.Form.Files.ToList());
            return Ok(results);
            
        }



    }
}
