using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using View_Wine.Models;
using DataLayer.Models;
using System.Text;
using System.Net.Http;
using System.Reflection.Metadata;

namespace View_Wine.Controllers
{
    public class WinesController : Controller
    {
        Uri _baseAddress = new Uri("http://localhost:5067/api");
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
                wineList = JsonConvert.DeserializeObject<List<WineModal>>(data).ToList();
            }
            return View(wineList); //Modal nao thi display modal do , dung co lam dung scaffold 
        }

        [HttpGet]
        public IActionResult Create()
        {    
            return View();
        }
        [HttpPost]
        public IActionResult Create(WineModal modal)
        {
            try
            {
                string data = JsonConvert.SerializeObject(modal);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage responseMessage = _httpClient
                    .PostAsync(_httpClient.BaseAddress + "/wine/createwine/create", content).Result;
                if (responseMessage.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Product Created";
                    return RedirectToAction("Index");
                }
            }
            catch(Exception ex) 
            {
                TempData["errorMessage"] = ex.Message;

                return View();

            }
            return View();
        }
    }
}