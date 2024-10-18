using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using View_Wine.Models;
using DataLayer.Models;

namespace View_Wine.Controllers
{
    public class WinesController : Controller
    {
        Uri _baseAddress = new Uri("http://localhost:5067/odata");
        private readonly HttpClient _httpClient;

        public WinesController()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = _baseAddress;
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<WineModal> wineList = new List<WineModal>();
            HttpResponseMessage httpResponseMessage = _httpClient.GetAsync(_baseAddress + "/wine/getallwine").Result;
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                string data = httpResponseMessage.Content.ReadAsStringAsync().Result;
                wineList = JsonConvert.DeserializeObject<List<WineModal>>(data);
            }

            var dataLayerWines = wineList.Select(w => new Wine
            {
                WineId = w.WineId,
                CategoryId = w.CategoryId, // Ensure this is mapped if needed
                Name = w.Name,
                Origin = w.Origin,
                Volume = w.Volume,
                AlcContent = w.AlcContent,
                Description = w.Description,
                Status = w.Status,
                // Handle Category mapping if needed
                Category = null // Set this if you want to handle categories
            }).ToList(); // Ensure you convert to a List

            return View(dataLayerWines); // Pass the correct model type
        }
    }
}