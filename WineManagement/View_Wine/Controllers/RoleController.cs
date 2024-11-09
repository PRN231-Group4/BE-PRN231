using BusinessLayer.Modal.Request;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using View_Wine.Models;

namespace View_Wine.Controllers
{
    
        public class RoleController : BaseController
    {

            Uri _baseAddress = new Uri("http://localhost:5067/odata");
            private readonly HttpClient _httpClient;

            public RoleController()
            {
                _httpClient = new HttpClient();
                _httpClient.BaseAddress = _baseAddress;
            }

            [HttpGet]


            public IActionResult Index()
            {
            // Retrieve roleId from session
            var roleId = HttpContext.Session.GetInt32("roleId");

            // Check if the user has the appropriate role
            if (roleId != 2)
            {
                // Optionally, you can redirect to an error page or the home page
                return RedirectToAction("AccessDenied", "Home");
            }
            List<RoleModal> roles = new List<RoleModal>();
                HttpResponseMessage httpResponseMessage = _httpClient.GetAsync(_baseAddress + "/role").Result;
                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    string data = httpResponseMessage.Content.ReadAsStringAsync().Result;
                    roles = JsonConvert.DeserializeObject<List<RoleModal>>(data);
                }

                var dataLayerRequest = roles.Select(w => new RoleDTO
                {
                    RoleId = w.RoleId,
                    Name = w.Name,
                }).ToList(); // Ensure you convert to a List

                return View(dataLayerRequest); // Pass the correct model type
            }

            [HttpGet]
            public async Task<IActionResult> Create()
            {
            var roleId = HttpContext.Session.GetInt32("roleId");


            // Check if the user has the appropriate role
            if (roleId != 2)
            {
                // Optionally, you can redirect to an error page or the home page
                return RedirectToAction("AccessDenied", "Home");
            }
            return View();
            }

            [HttpPost]
            public async Task<IActionResult> Create(RoleDTO RoleDTO)
            {
                if (ModelState.IsValid)
                {
                    // Gửi yêu cầu đến API để tạo WineRequest mới
                    var response = await _httpClient.PostAsJsonAsync(_baseAddress + "/role/create", RoleDTO);

                    // Kiểm tra xem API có trả về lỗi hay không
                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index"); // Sau khi tạo thành công, quay lại danh sách
                    }
                    else
                    {
                        // Lấy chi tiết lỗi từ API nếu có
                        var errorContent = await response.Content.ReadAsStringAsync();
                        ModelState.AddModelError(string.Empty, $"Error from API: {errorContent}");
                    }
                }

                return View(RoleDTO); // Nếu có lỗi, hiển thị lại form với thông tin hiện tại
            }


            [HttpGet]
            public async Task<IActionResult> Edit(int id)
        {
            var roleId = HttpContext.Session.GetInt32("roleId");


            // Check if the user has the appropriate role
            if (roleId != 2)
            {
                // Optionally, you can redirect to an error page or the home page
                return RedirectToAction("AccessDenied", "Home");
            }
            try
                {
                    // Lấy chi tiết của WineRequest theo id
                    var roleResponse = await _httpClient.GetAsync($"{_baseAddress}/role/get-by-id?id={id}");
                    if (!roleResponse.IsSuccessStatusCode)
                    {
                        return NotFound();
                    }

                    var roleData = await roleResponse.Content.ReadAsStringAsync();
                    var roleDto = JsonConvert.DeserializeObject<RoleModal>(roleData);

                    if (roleDto == null)
                    {
                        return NotFound();
                    }

                    // Chuyển đổi từ WineRequestDTO sang WineRequestEditViewModal
                    var roleEditViewModel = new RoleModal
                    {
                        RoleId = roleDto.RoleId,
                        Name = roleDto.Name,

                    };

                    return View(roleEditViewModel); // Truyền đối tượng wineRequestEditViewModel cho view
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }




            [HttpPost]
            public async Task<IActionResult> Edit(RoleDTO RoleDTO)
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        // Tạo URL cho API update
                        var updateUrl = $"http://localhost:5067/odata/role/update?id={RoleDTO.RoleId}";

                        // Gửi yêu cầu PUT để cập nhật WineRequest
                        var response = await _httpClient.PutAsJsonAsync(updateUrl, RoleDTO);

                        if (response.IsSuccessStatusCode)
                        {
                            // Nếu thành công, chuyển hướng về trang Index
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            // Lấy thông tin lỗi từ response và hiển thị cho người dùng
                            var errorContent = await response.Content.ReadAsStringAsync();
                            ModelState.AddModelError(string.Empty, $"Error from API: {errorContent}");
                        }
                    }
                    catch (Exception ex)
                    {
                        // Xử lý lỗi ngoại lệ
                        ModelState.AddModelError(string.Empty, $"An error occurred: {ex.Message}");
                    }
                }

                // Nếu có lỗi hoặc ModelState không hợp lệ, trả về view với dữ liệu hiện tại
                return View(RoleDTO);
            }


            [HttpGet]
            public async Task<IActionResult> Delete(int id, int ms)
            {
            var roleId = HttpContext.Session.GetInt32("roleId");


            // Check if the user has the appropriate role
            if (roleId != 2)
            {
                // Optionally, you can redirect to an error page or the home page
                return RedirectToAction("AccessDenied", "Home");
            }
            var response = await _httpClient.GetAsync(_baseAddress + $"/role/get-by-id?id={id}");

                // Kiểm tra xem yêu cầu có thành công không
                if (!response.IsSuccessStatusCode)
                {
                    return NotFound();
                }

                var data = await response.Content.ReadAsStringAsync();

                // Giả sử bạn cần deserialize dữ liệu về WineRequest
                var roleRequest = JsonConvert.DeserializeObject<RoleDTO>(data);

                if (roleRequest == null)
                {
                    return NotFound();
                }

                return View(roleRequest); // Truyền đối tượng đơn cho view
            }

            [HttpGet]

            public async Task<IActionResult> Details(int id)
            {
            var roleId = HttpContext.Session.GetInt32("roleId");


            // Check if the user has the appropriate role
            if (roleId != 2)
            {
                // Optionally, you can redirect to an error page or the home page
                return RedirectToAction("AccessDenied", "Home");
            }
            var response = await _httpClient.GetAsync(_baseAddress + $"/role/get-by-id?id={id}");

                // Kiểm tra xem yêu cầu có thành công không
                if (!response.IsSuccessStatusCode)
                {
                    return NotFound();
                }

                var data = await response.Content.ReadAsStringAsync();

                // Giả sử bạn cần deserialize dữ liệu về WineRequest
                var roleRequest = JsonConvert.DeserializeObject<RoleDTO>(data);

                if (roleRequest == null)
                {
                    return NotFound();
                }

                return View(roleRequest); // Truyền đối tượng đơn cho view
            }

            [HttpPost]
            public async Task<IActionResult> Delete(int id)
            {
                var response = await _httpClient.DeleteAsync(_baseAddress + $"/role/delete?id={id}"); // Gọi API

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index"); // Quay lại danh sách sau khi xóa thành công
                }

                // Xử lý lỗi nếu có
                return RedirectToAction("Index"); // Hoặc bạn có thể trả về một trang thông báo lỗi
            }


        }
    }


