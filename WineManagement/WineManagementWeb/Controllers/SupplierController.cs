using DataLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using WineManagementWeb.Models;

namespace WineManagementWeb.Controllers
{
    public class SupplierController : BaseController
    {
        public class SupplierResponse
        {
            [JsonProperty("value")]
            public List<Supplier> Value { get; set; }
        }
        public SupplierController(IConfiguration configuration) : base(configuration) { }
        // GET: SupplierController
        public async Task<ActionResult> Index()
        {

            HttpResponseMessage res = await _httpClient.GetAsync(SupplierURL);
            if (!res.IsSuccessStatusCode)
            {
            }
            return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });

            string rData = await res.Content.ReadAsStringAsync();
            var response = JsonConvert.DeserializeObject<SupplierResponse>(rData);
            return View(response.Value);
        }

        // GET: SupplierController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: SupplierController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SupplierController/Create
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

        // GET: SupplierController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: SupplierController/Edit/5
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

        // GET: SupplierController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: SupplierController/Delete/5
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
