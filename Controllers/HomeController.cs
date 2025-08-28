using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ST10070824_ABCRetail.Models;
using ST10070824_ABCRetail.Services;

namespace ST10070824_ABCRetail.Controllers
{
    public class HomeController : Controller
    {
        private readonly AzureQueueService _queueService;
        private readonly AzureBlobServices _azureBlobService;

        public HomeController(AzureBlobServices azureBlobService, AzureQueueService queueService)
        {
            _azureBlobService = azureBlobService;
            _queueService = queueService;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(IFormFile file)
        {
            if (file != null)
            {
                var result = await _azureBlobService.UploadFileAsync(file, "test");
                ViewBag.Message = "Uploaded successfully!";
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Order order)
        {
            order.orderID = Guid.NewGuid().ToString();
            await _queueService.SendMessageAsync(order);
            return RedirectToAction("CreateOrder"); //changed from index
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
