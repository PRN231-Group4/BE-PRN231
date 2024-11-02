using BusinessLayer.Modal.Request; // Adjust based on your actual namespaces
using DataLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace View_Wine.Controllers
{
    public class WineDeliveryController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly Uri _baseAddress = new Uri("http://localhost:5067/odata");

        public WineDeliveryController()
        {
            _httpClient = new HttpClient { BaseAddress = _baseAddress };
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<WineBatchDTO> wineBatches = new List<WineBatchDTO>();
            HttpResponseMessage httpResponseMessage = await _httpClient.GetAsync(_baseAddress + "/winebatch"); // Đảm bảo URL đúng

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                string data = await httpResponseMessage.Content.ReadAsStringAsync();
                wineBatches = JsonConvert.DeserializeObject<List<WineBatchDTO>>(data);
            }

            return View(wineBatches); // Pass the list of wine batches to the view
        }

        [HttpPost]
        public async Task<IActionResult> Deliver(int id)
        {
            var response = await _httpClient.PostAsync($"/winebatch/deliver?id={id}", null);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            // Handle error response
            ModelState.AddModelError(string.Empty, "Error during delivery process.");
            return RedirectToAction("Index");
        }
    }
}