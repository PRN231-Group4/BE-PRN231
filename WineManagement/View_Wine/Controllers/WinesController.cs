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
using static System.Net.Mime.MediaTypeNames;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using DataLayer.Enum;

namespace View_Wine.Controllers
{
    public class WinesController : BaseController
    {
        Uri _baseAddress = new Uri("http://localhost:5067/odata");
        private readonly HttpClient _httpClient;
        private readonly Cloudinary _cloudinary;

        public WinesController(Cloudinary cloudinary)
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = _baseAddress;
            _cloudinary = cloudinary;
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
            List<WineModal> wineList = new List<WineModal>();
            HttpResponseMessage httpResponseMessage = _httpClient.GetAsync(_baseAddress + "/wine/getallwine?$filter=Status eq 'Active'").Result;
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
        public async Task<IActionResult> Create(WineModal modal)
        {
            try
            {
                //Check img
                if (modal.Image != null && modal.Image.Length > 0)
                {
                    var uploadParams = new ImageUploadParams
                    {
                        File = new FileDescription(modal.Image.FileName, modal.Image.OpenReadStream()),
                        UseFilename = true,
                        UniqueFilename = true,
                        Overwrite = true
                    };

                    //sync URL request and respond
                    var uploadResult = await _cloudinary.UploadAsync(uploadParams);
                    //added
                    modal.ImgUrl = uploadResult.SecureUrl.ToString();
                }
                //If img troll
                if (modal.Image == null)
                {
                    TempData["errorMessage"] = "No image was uploaded.";
                    return View();
                }
                //encryp data
                string data = JsonConvert.SerializeObject(modal);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage responseMessage = await _httpClient
                    .PostAsync(_httpClient.BaseAddress + "/wine/createwine/create", content);

                if (responseMessage.IsSuccessStatusCode)
                {

                    TempData["successMessage"] = "Product Created";
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

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var roleId = HttpContext.Session.GetInt32("roleId");

            // Check if the user has the appropriate role
            if (roleId != 1)
            {
                // Optionally, you can redirect to an error page or the home page
                return RedirectToAction("AccessDenied", "Home");
            }
            WineModal modal = new WineModal();
            HttpResponseMessage respond = _httpClient.GetAsync(_baseAddress + "/wine/getwinebyid/get-by-id/" + id).Result;
            if (respond.IsSuccessStatusCode)
            {
                string data = respond.Content.ReadAsStringAsync().Result;
                modal = JsonConvert.DeserializeObject<WineModal>(data);
            }
            return View(modal);
        }


        

        [HttpPost]
        public async Task<IActionResult> Edit(WineModal modal)
        {
            try
            {
                //Check img
                if (modal.Image != null && modal.Image.Length > 0)
                {
                    var uploadParams = new ImageUploadParams
                    {
                        File = new FileDescription(modal.Image.FileName, modal.Image.OpenReadStream()),
                        UseFilename = true,
                        UniqueFilename = true,
                        Overwrite = true
                    };

                    //sync URL request and respond
                    var uploadResult = await _cloudinary.UploadAsync(uploadParams);
                    //added
                    modal.ImgUrl = uploadResult.SecureUrl.ToString();
                }
                //If img troll
                if (modal.Image == null)
                {
                    TempData["errorMessage"] = "No image was uploaded.";
                    return View();
                }
                //encryp data
                string data = JsonConvert.SerializeObject(modal);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage responseMessage = _httpClient
                    .PutAsync(_httpClient.BaseAddress + "/wine/updatewine/update/" + modal.WineId, content).Result;

                if (responseMessage.IsSuccessStatusCode)
                {

                    TempData["successMessage"] = "Product Updated";
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
       
        [HttpGet]
        public IActionResult Details(int id)
        {
            var roleId = HttpContext.Session.GetInt32("roleId");

            // Check if the user has the appropriate role
            if (roleId != 1)
            {
                // Optionally, you can redirect to an error page or the home page
                return RedirectToAction("AccessDenied", "Home");
            }
            WineModal modal = new WineModal();
            HttpResponseMessage respond = _httpClient.GetAsync(_baseAddress + "/wine/getwinebyid/get-by-id/" + id).Result;
            if (respond.IsSuccessStatusCode)
            {
                string data = respond.Content.ReadAsStringAsync().Result;
                modal = JsonConvert.DeserializeObject<WineModal>(data);
            }
            return View(modal);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var roleId = HttpContext.Session.GetInt32("roleId");

            // Check if the user has the appropriate role
            if (roleId != 1)
            {
                // Optionally, you can redirect to an error page or the home page
                return RedirectToAction("AccessDenied", "Home");
            }
            WineModal modal = new WineModal();
            HttpResponseMessage respond = _httpClient.GetAsync(_baseAddress + "/wine/getwinebyid/get-by-id/" + id).Result;
            if (respond.IsSuccessStatusCode)
            {
                string data = respond.Content.ReadAsStringAsync().Result;
                modal = JsonConvert.DeserializeObject<WineModal>(data);
            }
            return View(modal);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(WineModal modal)
        {
            try
            {
                // Cập nhật trạng thái sản phẩm thành "InActive"
                modal.Status = WineStatusEnum.InActive.ToString(); // Hoặc giá trị trạng thái mà bạn muốn

                string data = JsonConvert.SerializeObject(modal);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

                // Gọi API để cập nhật trạng thái
                HttpResponseMessage responseMessage = await _httpClient
                    .PutAsync(_httpClient.BaseAddress + "/wine/updatewinestatusfailed/update-status/" + modal.WineId, content);

                if (responseMessage.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Product status updated successfully";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View(modal); // Trả lại modal để giữ thông tin
            }

            return View(modal); // Trả lại modal nếu có lỗi
        }
    }
}