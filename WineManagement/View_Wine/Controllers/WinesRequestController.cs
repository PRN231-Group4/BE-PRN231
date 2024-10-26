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
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                // Lấy chi tiết của WineRequest theo id
                var wineRequestResponse = await _httpClient.GetAsync($"{_baseAddress}/winerequest/get-by-id?id={id}");
                if (!wineRequestResponse.IsSuccessStatusCode)
                {
                    return NotFound();
                }

                var wineRequestData = await wineRequestResponse.Content.ReadAsStringAsync();
                var wineRequestDto = JsonConvert.DeserializeObject<WineRequestDTO>(wineRequestData);

                if (wineRequestDto == null)
                {
                    return NotFound();
                }

                // Chuyển đổi từ WineRequestDTO sang WineRequestEditViewModal
                var wineRequestEditViewModel = new WineRequestModal
                {
                    RequestId = wineRequestDto.RequestId,
                    SupplierId = wineRequestDto.SupplierId,
                    WineId = wineRequestDto.WineId,
                    ManagerId = wineRequestDto.ManagerId,
                    Quantity = wineRequestDto.Quantity,
                    Status = wineRequestDto.Status,
                    Description = wineRequestDto.Description,
                    RequestDate = wineRequestDto.RequestDate 
                };

                // Lấy danh sách Supplier
                var supplierResponse = await _httpClient.GetAsync(_baseAddress + "/supplier");
                var supplierData = await supplierResponse.Content.ReadAsStringAsync();
                var suppliers = JsonConvert.DeserializeObject<List<Supplier>>(supplierData);
                ViewBag.SupplierList = new SelectList(suppliers, "SupplierId", "Name", wineRequestDto.SupplierId);

                // Lấy danh sách Wine
                var wineResponse = await _httpClient.GetAsync(_baseAddress + "/wine/getallwine");
                var wineData = await wineResponse.Content.ReadAsStringAsync();
                var wines = JsonConvert.DeserializeObject<List<Wine>>(wineData);
                ViewBag.WineList = new SelectList(wines, "WineId", "Name", wineRequestDto.WineId);

                // Lấy danh sách Staff (Manager)
                var staffResponse = await _httpClient.GetAsync(_baseAddress + "/winerequest/all-staff");
                var staffData = await staffResponse.Content.ReadAsStringAsync();
                var staffs = JsonConvert.DeserializeObject<List<Account>>(staffData);
                ViewBag.StaffList = new SelectList(staffs, "AccountId", "Username", wineRequestDto.ManagerId);

                return View(wineRequestEditViewModel); // Truyền đối tượng wineRequestEditViewModel cho view
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }




        [HttpPost]
        public async Task<IActionResult> Edit(WineRequestDTO wineRequestDto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Tạo URL cho API update
                    var updateUrl = $"http://localhost:5067/odata/winerequest/update?id={wineRequestDto.RequestId}";

                    // Gửi yêu cầu PUT để cập nhật WineRequest
                    var response = await _httpClient.PutAsJsonAsync(updateUrl, wineRequestDto);

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
            return View(wineRequestDto);
        }


        [HttpGet]
        public async Task<IActionResult> Delete(int id,int ms)
        {
            var response = await _httpClient.GetAsync(_baseAddress + $"/winerequest/get-by-id?id={id}");

            // Kiểm tra xem yêu cầu có thành công không
            if (!response.IsSuccessStatusCode)
            {
                return NotFound();
            }

            var data = await response.Content.ReadAsStringAsync();

            // Giả sử bạn cần deserialize dữ liệu về WineRequest
            var wineRequest = JsonConvert.DeserializeObject<WineRequestDTO>(data);

            if (wineRequest == null)
            {
                return NotFound();
            }

            return View(wineRequest); // Truyền đối tượng đơn cho view
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var response = await _httpClient.GetAsync(_baseAddress + $"/winerequest/get-by-id?id={id}");

            // Kiểm tra xem yêu cầu có thành công không
            if (!response.IsSuccessStatusCode)
            {
                return NotFound();
            }

            var data = await response.Content.ReadAsStringAsync();

            // Giả sử bạn cần deserialize dữ liệu về WineRequest
            var wineRequest = JsonConvert.DeserializeObject<WineRequestDTO>(data);

            if (wineRequest == null)
            {
                return NotFound();
            }

            return View(wineRequest); // Truyền đối tượng đơn cho view
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _httpClient.DeleteAsync(_baseAddress +$"/winerequest/delete?id={id}"); // Gọi API

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index"); // Quay lại danh sách sau khi xóa thành công
            }

            // Xử lý lỗi nếu có
            return RedirectToAction("Index"); // Hoặc bạn có thể trả về một trang thông báo lỗi
        }


    }
}
