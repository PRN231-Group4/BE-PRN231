using BusinessLayer.Modal.Request;
using DataLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using View_Wine.Models;

namespace View_Wine.Controllers
{
    public class WinesRequestController : Controller
    {
        Uri _baseAddress = new Uri("http://localhost:5067/odata");
        private readonly HttpClient _httpClient;

        public WinesRequestController()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = _baseAddress;
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<WineRequestModal> wineRequests = new List<WineRequestModal>();
            HttpResponseMessage httpResponseMessage = _httpClient.GetAsync(_baseAddress + "/winerequest").Result;
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                string data = httpResponseMessage.Content.ReadAsStringAsync().Result;
                wineRequests = JsonConvert.DeserializeObject<List<WineRequestModal>>(data);
            }

            var dataLayerRequest = wineRequests.Select(w => new WineRequestDTO
            {
                RequestId = w.RequestId,
                SupplierName = w.SupplierName,
                ManagerName = w.ManagerName,
                Wine = w.Wine,
                WineId = w.WineId,
                Quantity = w.Quantity,
                RequestDate = w.RequestDate,
                Description = w.Description,
                Status = w.Status,
            }).ToList(); // Ensure you convert to a List

            return View(dataLayerRequest); // Pass the correct model type
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            try
            {
                // Tạo các task cho việc gọi API
                var supplierTask = _httpClient.GetAsync(_baseAddress + "/supplier");
                var wineTask = _httpClient.GetAsync(_baseAddress +"/wine/getallwine");
                var staffTask = _httpClient.GetAsync(_baseAddress + "/winerequest/all-staff");

                // Chờ tất cả các task hoàn thành cùng lúc
                await Task.WhenAll(supplierTask, wineTask, staffTask);

                // Xử lý kết quả của từng task
                var supplierResponse = await supplierTask;
                if (!supplierResponse.IsSuccessStatusCode)
                {
                    throw new Exception("Fail Fetch Supplier");
                }
                var supplierData = await supplierResponse.Content.ReadAsStringAsync();
                var suppliers = JsonConvert.DeserializeObject<List<Supplier>>(supplierData);
                ViewBag.SupplierList = new SelectList(suppliers, "SupplierId", "Name");

                var wineResponse = await wineTask;
                if (!wineResponse.IsSuccessStatusCode)
                {
                    throw new Exception("Fail Fetch Wine");
                }
                var wineData = await wineResponse.Content.ReadAsStringAsync();
                var wines = JsonConvert.DeserializeObject<List<Wine>>(wineData);
                ViewBag.WineList = new SelectList(wines, "WineId", "Name");

                var staffResponse = await staffTask;
                if (!staffResponse.IsSuccessStatusCode)
                {
                    throw new Exception("Fail Fetch Staff");
                }
                var staffData = await staffResponse.Content.ReadAsStringAsync();
                var staffs = JsonConvert.DeserializeObject<List<Account>>(staffData);
                ViewBag.StaffList = new SelectList(staffs, "AccountId", "Username");

                return View();
            }
            catch (Exception ex)
            {
                // Xử lý lỗi chung và trả về lỗi
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(WineRequestDTO wineRequestDto)
        {
            if (ModelState.IsValid)
            {
                // Gửi yêu cầu đến API để tạo WineRequest mới
                var response = await _httpClient.PostAsJsonAsync(_baseAddress + "/winerequest/create", wineRequestDto);

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

            return View(wineRequestDto); // Nếu có lỗi, hiển thị lại form với thông tin hiện tại
        }


        [HttpGet]
        public IActionResult Edit(int id)
        {
            WineRequestDTO wineRequest = null;

            HttpResponseMessage response = _httpClient.GetAsync(_baseAddress + $"/winerequest/{id}").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                wineRequest = JsonConvert.DeserializeObject<WineRequestDTO>(data);
            }

            return View(wineRequest); // Hiển thị form với dữ liệu của WineRequest cần chỉnh sửa
        }

        [HttpPut]
        public IActionResult Edit(WineRequestDTO wineRequestDto)
        {
            if (ModelState.IsValid)
            {
                // Gửi yêu cầu PUT để cập nhật WineRequest
                var putTask = _httpClient.PutAsJsonAsync(_baseAddress + $"/winerequest/{wineRequestDto.RequestId}", wineRequestDto);
                putTask.Wait();

                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index"); // Sau khi cập nhật thành công, quay lại danh sách
                }
            }

            return View(wineRequestDto); // Nếu có lỗi, hiển thị lại form với dữ liệu hiện tại
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            // Gửi yêu cầu đến API để xóa WineRequest
            var deleteTask = _httpClient.DeleteAsync(_baseAddress + $"/winerequest/{id}");
            deleteTask.Wait();

            var result = deleteTask.Result;
            if (result.IsSuccessStatusCode)
            {
                return RedirectToAction("Index"); // Sau khi xóa thành công, quay lại danh sách
            }

            // Nếu có lỗi, có thể hiển thị một trang thông báo lỗi
            return RedirectToAction("Index");
        }
    }
}
