using Microsoft.AspNetCore.Mvc;
using ST10070824_ABCRetail.Services;


namespace ST10070824_ABCRetail.Controllers
{
    public class FileController : Controller
    {

        private readonly AzureFileServices _azureFileServices;

        public FileController(AzureFileServices azureFileServices)
        {
            _azureFileServices = azureFileServices;
        }
        [HttpGet]
        public IActionResult Upload()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Upload(string fileName, string content)
        {
            if (string.IsNullOrEmpty(fileName) || string.IsNullOrEmpty(content))
            {
                ModelState.AddModelError(string.Empty, "File and content required.");
                return View();
            }         
            await _azureFileServices.UploadFileAsync(fileName, content);
            ViewBag.Message = "Successfully uploaded!";
            return View();
        }

    }
}
