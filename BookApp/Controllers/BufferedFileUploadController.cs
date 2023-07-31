using BookApp.Errors;
using BookApp.Models;
using BookApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BufferedFileUploadController : ControllerBase
    {

        public BufferedFileUploadLocalService _fileUploadService;
        public BufferedFileUploadController(BufferedFileUploadLocalService bufferedFileUploadService)
        {
            _fileUploadService = bufferedFileUploadService;
        }

        [HttpPost]
        public async Task<ActionResult> Index(FileUpload file)
        {
            try
            {


                var fileBytes = await System.IO.File.ReadAllBytesAsync(Path.Combine(Environment.CurrentDirectory, "UploadedFiles", file.FileName));

                if (await _fileUploadService.SaveFileToDatabase(file.FileName, fileBytes))
                {
                    return Ok();
                }
                else
                {
                    return BadRequest(new ApiResponse(500, "Failed to save file to database."));
                }



            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse(500, ex.Message));
            }
        }
    }
}
