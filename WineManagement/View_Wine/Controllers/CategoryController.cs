using BusinessLayer.Modal.Request;
using DataLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using View_Wine.Models;

namespace View_Wine.Controllers
{
    public class CategoryController : BaseController
    {
      
            Uri _baseAddress = new Uri("http://localhost:5067/odata");
            private readonly HttpClient _httpClient;

            public CategoryController()
            {
                _httpClient = new HttpClient();
                _httpClient.BaseAddress = _baseAddress;
            }

            [HttpGet]
            public IActionResult Index()
            {
            var roleId = HttpContext.Session.GetInt32("roleId");

            // Check if the user has the appropriate role
            if (roleId != 2)
            {
                // Optionally, you can redirect to an error page or the home page
                return RedirectToAction("AccessDenied", "Home");
            }
            List<CategoryModal> categories = new List<CategoryModal>();
                HttpResponseMessage httpResponseMessage = _httpClient.GetAsync(_baseAddress + "/category").Result;
                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    string data = httpResponseMessage.Content.ReadAsStringAsync().Result;
                    categories = JsonConvert.DeserializeObject<List<CategoryModal>>(data);
                }

                var dataLayerRequest = categories.Select(w => new CategoryDTO
                {
                    CategoryId = w.CategoryId,
                    Description = w.Description,
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
            public async Task<IActionResult> Create(CategoryDTO categoryDTO)
            {
                if (ModelState.IsValid)
                {
                    // Gửi yêu cầu đến API để tạo WineRequest mới
                    var response = await _httpClient.PostAsJsonAsync(_baseAddress + "/category/create", categoryDTO);

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

                return View(categoryDTO); // Nếu có lỗi, hiển thị lại form với thông tin hiện tại
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
                    var categoryResponse = await _httpClient.GetAsync($"{_baseAddress}/category/get-by-id?id={id}");
                    if (!categoryResponse.IsSuccessStatusCode)
                    {
                        return NotFound();
                    }

                    var categoryData = await categoryResponse.Content.ReadAsStringAsync();
                    var categoryDto = JsonConvert.DeserializeObject<CategoryModal>(categoryData);

                    if (categoryDto == null)
                    {
                        return NotFound();
                    }

                    // Chuyển đổi từ WineRequestDTO sang WineRequestEditViewModal
                    var categoryEditViewModel = new CategoryModal
                    {
                        CategoryId = categoryDto.CategoryId,
                        Name = categoryDto.Name,
                        Description = categoryDto.Description,

                    };

                    return View(categoryEditViewModel); // Truyền đối tượng wineRequestEditViewModel cho view
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }




            [HttpPost]
            public async Task<IActionResult> Edit(CategoryDTO categoryDTO)
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        // Tạo URL cho API update
                        var updateUrl = $"http://localhost:5067/odata/category/update?id={categoryDTO.CategoryId}";

                        // Gửi yêu cầu PUT để cập nhật WineRequest
                        var response = await _httpClient.PutAsJsonAsync(updateUrl, categoryDTO);

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
                return View(categoryDTO);
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
            var response = await _httpClient.GetAsync(_baseAddress + $"/category/get-by-id?id={id}");

                // Kiểm tra xem yêu cầu có thành công không
                if (!response.IsSuccessStatusCode)
                {
                    return NotFound();
                }

                var data = await response.Content.ReadAsStringAsync();

                // Giả sử bạn cần deserialize dữ liệu về WineRequest
                var categoryRequest = JsonConvert.DeserializeObject<CategoryDTO>(data);

                if (categoryRequest == null)
                {
                    return NotFound();
                }

                return View(categoryRequest); // Truyền đối tượng đơn cho view
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
            var response = await _httpClient.GetAsync(_baseAddress + $"/category/get-by-id?id={id}");

                // Kiểm tra xem yêu cầu có thành công không
                if (!response.IsSuccessStatusCode)
                {
                    return NotFound();
                }

                var data = await response.Content.ReadAsStringAsync();

                // Giả sử bạn cần deserialize dữ liệu về WineRequest
                var categoryRequest = JsonConvert.DeserializeObject<CategoryDTO>(data);

                if (categoryRequest == null)
                {
                    return NotFound();
                }

                return View(categoryRequest); // Truyền đối tượng đơn cho view
            }

            [HttpPost]
            public async Task<IActionResult> Delete(int id)
            {
                var response = await _httpClient.DeleteAsync(_baseAddress + $"/category/delete?id={id}"); // Gọi API

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index"); // Quay lại danh sách sau khi xóa thành công
                }

                // Xử lý lỗi nếu có
                return RedirectToAction("Index"); // Hoặc bạn có thể trả về một trang thông báo lỗi
            }


        }
    }
