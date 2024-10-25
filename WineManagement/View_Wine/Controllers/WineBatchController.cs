using BusinessLayer.Modal.Request;
using DataLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using View_Wine.Models;

namespace View_Wine.Controllers
{
    public class WineBatchController : Controller
    {
       Uri _baseAddress = new Uri("http://localhost:5067/odata");
        private readonly HttpClient _httpClient;

        public WineBatchController()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = _baseAddress;
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<WineBatchModal> wineRequests = new List<WineBatchModal>();
            HttpResponseMessage httpResponseMessage = _httpClient.GetAsync(_baseAddress + "/winebatch").Result;
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                string data = httpResponseMessage.Content.ReadAsStringAsync().Result;
                wineRequests = JsonConvert.DeserializeObject<List<WineBatchModal>>(data);
            }

            var dataLayerRequest = wineRequests.Select(w => new WineBatchDTO
            {
                BatchId = w.BatchId,
                WineId = w.WineId,
                RequestId = w.RequestId,
                BatchNumber = w.BatchNumber,
                ImportDate = w.ImportDate,
                Quantity = w.Quantity,
                ProductionYear  = w.ProductionYear,
                Status = w.Status,
            }).ToList(); // Ensure you convert to a List

            return View(dataLayerRequest); // Pass the correct model type
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            try
            {
              
                var wineTask = _httpClient.GetAsync(_baseAddress + "/wine/getallwine");
                var requestTask = _httpClient.GetAsync(_baseAddress + "/winerequest");


                // Chờ tất cả các task hoàn thành cùng lúc
                await Task.WhenAll( wineTask, requestTask);

                // Xử lý kết quả của từng task


                var wineResponse = await wineTask;
                if (!wineResponse.IsSuccessStatusCode)
                {
                    throw new Exception("Fail Fetch Wine");
                }
                var wineData = await wineResponse.Content.ReadAsStringAsync();
                var wines = JsonConvert.DeserializeObject<List<Wine>>(wineData);
                ViewBag.WineList = new SelectList(wines, "WineId", "Name");

                var requestResponse = await requestTask;
                if (!requestResponse.IsSuccessStatusCode)
                {
                    throw new Exception("Fail Fetch winerequest");
                }
                var requestData = await requestResponse.Content.ReadAsStringAsync();
                var requests = JsonConvert.DeserializeObject<List<WineRequest>>(requestData);
                ViewBag.RequestList = new SelectList(requests, "RequestId", "RequestId");

                return View();
            }
            catch (Exception ex)
            {
                // Xử lý lỗi chung và trả về lỗi
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(WineBatchDTO wineBatchDTO)
        {
            if (ModelState.IsValid)
            {
                // Gửi yêu cầu đến API để tạo WineRequest mới
                var response = await _httpClient.PostAsJsonAsync(_baseAddress + "/winebatch/create", wineBatchDTO);

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

            return View(wineBatchDTO); // Nếu có lỗi, hiển thị lại form với thông tin hiện tại
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                // Lấy chi tiết của WineRequest theo id
                var wineBatchResponse = await _httpClient.GetAsync($"{_baseAddress}/winebatch/get-by-id?id={id}");
                if (!wineBatchResponse.IsSuccessStatusCode)
                {
                    return NotFound();
                }

                var wineBatchData = await wineBatchResponse.Content.ReadAsStringAsync();
                var wineBatchDto = JsonConvert.DeserializeObject<WineBatchDTO>(wineBatchData);

                if (wineBatchDto == null)
                {
                    return NotFound();
                }

                // Chuyển đổi từ WineRequestDTO sang WineRequestEditViewModal
                var wineBatchEditViewModel = new WineBatchModal
                {
                    BatchId = wineBatchDto.BatchId,
                    BatchNumber = wineBatchDto.BatchNumber,
                    ImportDate = wineBatchDto.ImportDate,
                    ProductionYear = wineBatchDto.ProductionYear,
                    Quantity = wineBatchDto.Quantity,
                    RequestId = wineBatchDto.RequestId
                    
                };

               

                // Lấy danh sách Wine
                var wineResponse = await _httpClient.GetAsync(_baseAddress + "/wine/getallwine");
                var wineData = await wineResponse.Content.ReadAsStringAsync();
                var wines = JsonConvert.DeserializeObject<List<Wine>>(wineData);
                ViewBag.WineList = new SelectList(wines, "WineId", "Name", wineBatchDto.WineId);

         
                return View(wineBatchEditViewModel); // Truyền đối tượng wineRequestEditViewModel cho view
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }




        [HttpPost]
        public async Task<IActionResult> Edit(WineBatchDTO wineBatchDTO)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Tạo URL cho API update
                    var updateUrl = $"http://localhost:5067/odata/winebatch/update?id={wineBatchDTO.BatchId}";

                    // Gửi yêu cầu PUT để cập nhật WineRequest
                    var response = await _httpClient.PutAsJsonAsync(updateUrl, wineBatchDTO);

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
            return View(wineBatchDTO);
        }


        [HttpGet]
        public async Task<IActionResult> Delete(int id, int ms)
        {
            var response = await _httpClient.GetAsync(_baseAddress + $"/winebatch/get-by-id?id={id}");

            // Kiểm tra xem yêu cầu có thành công không
            if (!response.IsSuccessStatusCode)
            {
                return NotFound();
            }

            var data = await response.Content.ReadAsStringAsync();

            // Giả sử bạn cần deserialize dữ liệu về WineRequest
            var wineBatchRequest = JsonConvert.DeserializeObject<WineBatchDTO>(data);

            if (wineBatchRequest == null)
            {
                return NotFound();
            }

            return View(wineBatchRequest); // Truyền đối tượng đơn cho view
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var response = await _httpClient.GetAsync(_baseAddress + $"/winebatch/get-by-id?id={id}");

            // Kiểm tra xem yêu cầu có thành công không
            if (!response.IsSuccessStatusCode)
            {
                return NotFound();
            }

            var data = await response.Content.ReadAsStringAsync();

            // Giả sử bạn cần deserialize dữ liệu về WineRequest
            var wineRequest = JsonConvert.DeserializeObject<WineBatchDTO>(data);

            if (wineRequest == null)
            {
                return NotFound();
            }

            return View(wineRequest); // Truyền đối tượng đơn cho view
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _httpClient.DeleteAsync(_baseAddress + $"/winebatch/delete?id={id}"); // Gọi API

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index"); // Quay lại danh sách sau khi xóa thành công
            }

            // Xử lý lỗi nếu có
            return RedirectToAction("Index"); // Hoặc bạn có thể trả về một trang thông báo lỗi
        }


    }
}

