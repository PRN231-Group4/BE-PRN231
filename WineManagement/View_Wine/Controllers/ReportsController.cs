using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DataLayer.Models;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Newtonsoft.Json;
using System.Text;
using View_Wine.Models;

namespace View_Wine.Controllers
{
    public class ReportsController : BaseController
    {
        Uri _baseAddress = new Uri("http://localhost:5067/api");
        private readonly HttpClient _httpClient;

        public ReportsController()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = _baseAddress;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var roleId = HttpContext.Session.GetInt32("roleId");

            // Check if the user has the appropriate role
            if (roleId != 1)
            {
                // Optionally, you can redirect to an error page or the home page
                return RedirectToAction("AccessDenied", "Home");
            }
            List<ReportModal> wineList = new List<ReportModal>();
            HttpResponseMessage httpResponseMessage = _httpClient.GetAsync(_baseAddress + "/report/getallreport").Result;
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                string data = httpResponseMessage.Content.ReadAsStringAsync().Result;
                wineList = JsonConvert.DeserializeObject<List<ReportModal>>(data).ToList();
            }
            return View(wineList); //Modal nao thi display modal do , dung co lam dung scaffold 
        }

        [HttpGet]
        public IActionResult Create()
        {
            var roleId = HttpContext.Session.GetInt32("roleId");

            // Check if the user has the appropriate role
            if (roleId != 1)
            {
                // Optionally, you can redirect to an error page or the home page
                return RedirectToAction("AccessDenied", "Home");
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(ReportModal modal)
        {
            try
            {               
                //encryp data
                string data = JsonConvert.SerializeObject(modal);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage responseMessage = await _httpClient
                    .PostAsync(_httpClient.BaseAddress + "/report/create/create", content);

                if (responseMessage.IsSuccessStatusCode)
                {

                    TempData["successMessage"] = "Report Created";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
            return View();
        }

       
        //[HttpGet]
        //public IActionResult Delete(int id)
        //{
        //    WineModal modal = new WineModal();
        //    HttpResponseMessage respond = _httpClient.GetAsync(_baseAddress + "/wine/getwinebyid/get-by-id/" + id).Result;
        //    if (respond.IsSuccessStatusCode)
        //    {
        //        string data = respond.Content.ReadAsStringAsync().Result;
        //        modal = JsonConvert.DeserializeObject<WineModal>(data);
        //    }
        //    return View(modal);
        //}

        //[HttpPost]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    try
        //    {
        //        HttpResponseMessage responseMessage = _httpClient
        //            .DeleteAsync(_httpClient.BaseAddress + "/wine/updatewine/update/" + id).Result;

        //        if (responseMessage.IsSuccessStatusCode)
        //        {

        //            TempData["successMessage"] = "Product Updated";
        //            return RedirectToAction("Index");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        TempData["errorMessage"] = ex.Message;
        //        return View();
        //    }
        //    return View();
        //}
    }
}
