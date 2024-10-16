using DataLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using View_Wine.Models;

namespace View_Wine.Controllers
{
    public class WinesTestController : BaseController
    {
        public class WineResponse
        {
            [JsonProperty("value")]
            public List<Wine> Value { get; set; }
        }
        public WinesTestController(IConfiguration configuration) : base(configuration) { }
        // GET: WinesTestController
        public async Task<ActionResult> Index()
        {

            HttpResponseMessage res = await _httpClient.GetAsync(WineURL);
            if (!res.IsSuccessStatusCode)
            {
            }
            return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });

            string rData = await res.Content.ReadAsStringAsync();
            var response = JsonConvert.DeserializeObject<WineResponse>(rData);
            return View(response.Value);
        }

        // GET: WinesTestController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: WinesTestController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: WinesTestController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: WinesTestController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: WinesTestController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: WinesTestController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: WinesTestController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
