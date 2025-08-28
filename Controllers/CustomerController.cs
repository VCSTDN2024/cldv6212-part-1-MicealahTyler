using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Azure;
using Azure.Data.Tables;
using ST10070824_ABCRetail.Models;
using TechTalk.SpecFlow.CommonModels;

namespace ST10070824_ABCRetail.Controllers
{
    public class CustomerController : Controller
    {
        private readonly string _storageAccountConnecionString;
        private readonly string _tableName = "customerProfile";
        private readonly TableClient _tableClient;

        public CustomerController(IConfiguration configuration)
        {
            _storageAccountConnecionString = configuration["DefaultEndpointsProtocol=https;AccountName=st10070824part1;AccountKey=1Sm/Ydk+gcO0TVWQXlkPDyA4vhCh10Z2UN/Xr9sAo+2W/tNSR1+Rsg5VQPI0L2K3QZn2XbkjSFgU+AStRVeoBA==;EndpointSuffix=core.windows.net"];
            _tableClient = new TableClient(_storageAccountConnecionString, _tableName);
            _tableClient.CreateIfNotExists();
        
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>Create(Customer customer)
        {
            if (ModelState.IsValid)
            {
                await _tableClient.AddEntityAsync(customer);
                return RedirectToAction(nameof(Success));

            }
            return View(customer);
        }
        
        public IActionResult Success()
        {
            return View();
        }
    }
}
