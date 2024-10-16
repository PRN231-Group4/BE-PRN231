using FE_MVC_WineManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace FE_MVC_WineManagement.Controllers
{
    public class WineController : Controller
    {
        Uri baseAddress = new Uri("http://localhost:27704/api");
        private readonly HttpClient _httpClient;

        public WineController()
        {
            _httpClient.BaseAddress = baseAddress;
            _httpClient = new HttpClient();
        }
        [HttpGet]
        public IActionResult Index()
        {
            List<WineModel> winelist = new List<WineModel>();   
            HttpResponseMessage response = _httpClient.GetAsync(_httpClient.BaseAddress + "/wine/Get").Result;
            if(response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                winelist = JsonConvert.DeserializeObject<List<WineModel>>(data);
            }
            return View(winelist);
        }
    }
}
