using BusinessLayer.Modal.Request;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using View_Wine.Models;

namespace View_Wine.Controllers
{

    public class SupplierController : Controller
    {

        Uri _baseAddress = new Uri("http://localhost:5067/odata");
        private readonly HttpClient _httpClient;

        public SupplierController()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = _baseAddress;
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<SupplierModal> suppliers = new List<SupplierModal>();
            HttpResponseMessage httpResponseMessage = _httpClient.GetAsync(_baseAddress + "/supplier").Result;
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                string data = httpResponseMessage.Content.ReadAsStringAsync().Result;
                suppliers = JsonConvert.DeserializeObject<List<SupplierModal>>(data);
            }

            var dataLayerRequest = suppliers.Select(w => new SupplierDTO
            {
                SupplierId = w.SupplierId,
                Name = w.Name,
            }).ToList(); // Ensure you convert to a List

            return View(dataLayerRequest); // Pass the correct model type
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(SupplierDTO SupplierDTO)
        {
            if (ModelState.IsValid)
            {
                // Gửi yêu cầu đến API để tạo WineRequest mới
                var response = await _httpClient.PostAsJsonAsync(_baseAddress + "/supplier/create", SupplierDTO);

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

            return View(SupplierDTO); // Nếu có lỗi, hiển thị lại form với thông tin hiện tại
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                // Lấy chi tiết của WineRequest theo id
                var supplierResponse = await _httpClient.GetAsync($"{_baseAddress}/supplier/get-by-id?id={id}");
                if (!supplierResponse.IsSuccessStatusCode)
                {
                    return NotFound();
                }

                var supplierData = await supplierResponse.Content.ReadAsStringAsync();
                var supplierDto = JsonConvert.DeserializeObject<SupplierModal>(supplierData);

                if (supplierDto == null)
                {
                    return NotFound();
                }

                // Chuyển đổi từ WineRequestDTO sang WineRequestEditViewModal
                var supplierEditViewModel = new SupplierModal
                {
                    SupplierId = supplierDto.SupplierId,
                    Name = supplierDto.Name,

                };

                return View(supplierEditViewModel); // Truyền đối tượng wineRequestEditViewModel cho view
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }




        [HttpPost]
        public async Task<IActionResult> Edit(SupplierDTO SupplierDTO)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Tạo URL cho API update
                    var updateUrl = $"http://localhost:5067/odata/supplier/update?id={SupplierDTO.SupplierId}";

                    // Gửi yêu cầu PUT để cập nhật WineRequest
                    var response = await _httpClient.PutAsJsonAsync(updateUrl, SupplierDTO);

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
            return View(SupplierDTO);
        }


        [HttpGet]
        public async Task<IActionResult> Delete(int id, int ms)
        {
            var response = await _httpClient.GetAsync(_baseAddress + $"/supplier/get-by-id?id={id}");

            // Kiểm tra xem yêu cầu có thành công không
            if (!response.IsSuccessStatusCode)
            {
                return NotFound();
            }

            var data = await response.Content.ReadAsStringAsync();

            // Giả sử bạn cần deserialize dữ liệu về WineRequest
            var supplierRequest = JsonConvert.DeserializeObject<SupplierDTO>(data);

            if (supplierRequest == null)
            {
                return NotFound();
            }

            return View(supplierRequest); // Truyền đối tượng đơn cho view
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var response = await _httpClient.GetAsync(_baseAddress + $"/supplier/get-by-id?id={id}");

            // Kiểm tra xem yêu cầu có thành công không
            if (!response.IsSuccessStatusCode)
            {
                return NotFound();
            }

            var data = await response.Content.ReadAsStringAsync();

            // Giả sử bạn cần deserialize dữ liệu về WineRequest
            var supplierRequest = JsonConvert.DeserializeObject<SupplierDTO>(data);

            if (supplierRequest == null)
            {
                return NotFound();
            }

            return View(supplierRequest); // Truyền đối tượng đơn cho view
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _httpClient.DeleteAsync(_baseAddress + $"/supplier/delete?id={id}"); // Gọi API

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index"); // Quay lại danh sách sau khi xóa thành công
            }

            // Xử lý lỗi nếu có
            return RedirectToAction("Index"); // Hoặc bạn có thể trả về một trang thông báo lỗi
        }

    }
}