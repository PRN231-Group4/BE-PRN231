using BusinessLayer.Modal.Request;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using View_Wine.Models;

namespace View_Wine.Controllers
{
    public class LoginController : Controller
    {
        Uri _baseAddress = new Uri("http://localhost:5067/api");
        private readonly HttpClient _httpClient;

        public LoginController()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = _baseAddress;
        }

        [HttpGet]
        public IActionResult Index()
        {
            // Tạo một instance của LoginModel đúng
            var loginModel = new LoginModal();
            return View(loginModel); // Gửi instance đến view
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginModal model)
        {
            if (ModelState.IsValid)
            {
                var json = JsonConvert.SerializeObject(model);
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _httpClient.PostAsync("http://localhost:5067/api/auth/login", content);

                if (response.IsSuccessStatusCode)
                {
                    string result = await response.Content.ReadAsStringAsync();
                    var apiResponse = JsonConvert.DeserializeObject<ApiResponse>(result);

                    if (apiResponse != null && apiResponse.Code == 200 && apiResponse.Data != null)
                    {
                        var userData = apiResponse.Data;

                        if (!string.IsNullOrEmpty(userData.Token))
                        {
                            // Lưu token vào session
                            HttpContext.Session.SetString("authToken", userData.Token);
                            // Lưu tên người dùng và vai trò vào session
                            HttpContext.Session.SetString("username", userData.Account.Username);
                            HttpContext.Session.SetInt32("roleId", userData.Account.RoleId); // Lưu RoleId
                            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("authToken"));

                            // Chuyển hướng theo vai trò
                            switch (userData.Account.RoleId)
                            {
                                case 1:
                                    return RedirectToAction("Index", "Wine"); // Chuyển hướng đến trang quản trị
                                case 4:
                                    return RedirectToAction("Index", "WineBatch"); // Chuyển hướng đến trang của Manager
                              
                                default:
                                    return RedirectToAction("Index", "Home"); // Chuyển hướng đến trang mặc định
                            }
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, "Lỗi: Không nhận được token từ API.");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Lỗi: Không nhận được thông tin người dùng.");
                    }
                }
                else
                {
                    string errorContent = await response.Content.ReadAsStringAsync();
                    ModelState.AddModelError(string.Empty, $"Lỗi từ API: {errorContent}");
                }
            }

            return View(model);
        }

    }  
}
