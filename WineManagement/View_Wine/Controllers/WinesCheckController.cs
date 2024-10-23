using BusinessLayer.Modal.Request;
using DataLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using View_Wine.Models;

namespace View_Wine.Controllers
{
    public class WinesCheckController : Controller
    {
        Uri _baseAddress = new Uri("http://localhost:5067/odata");
        private readonly HttpClient _httpClient;

        public WinesCheckController()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = _baseAddress;
        }


        [HttpGet]
        public IActionResult Index()
        {
            List<WineCheckModal> wineList = new List<WineCheckModal>();
            HttpResponseMessage httpResponseMessage = _httpClient.GetAsync(_baseAddress + "/winecheck").Result;
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                string data = httpResponseMessage.Content.ReadAsStringAsync().Result;
                wineList = JsonConvert.DeserializeObject<List<WineCheckModal>>(data);
            }

            var dataLayerWines = wineList.Select(w => new WineCheckDTO
            {
                CheckId = w.CheckId,
                RequestId = w.RequestId,
                InspectorId = w.InspectorId,
                WineId = w.WineId,
                Quantity = w.Quantity,
                Status = w.Status,
                Description = w.Description,
                CheckDate = w.CheckDate,
                ImageUrl = w.ImageUrl
            }).ToList(); 

            return View(dataLayerWines); 
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                // Lấy chi tiết của WineRequest theo id
                var wineRequestResponse = await _httpClient.GetAsync($"{_baseAddress}/winecheck/get-by-id?id={id}");
                if (!wineRequestResponse.IsSuccessStatusCode)
                {
                    return NotFound();
                }

                var wineCheckData = await wineRequestResponse.Content.ReadAsStringAsync();
                var wineCheckDto = JsonConvert.DeserializeObject<WineCheckDTO>(wineCheckData);

                if (wineCheckDto == null)
                {
                    return NotFound();
                }

                // Chuyển đổi từ WineRequestDTO sang WineRequestEditViewModal
                var wineCheckEditViewModel = new WineCheckModal
                {
                    InspectorId =wineCheckDto.InspectorId,
                    CheckId = wineCheckDto.CheckId,
                    RequestId = wineCheckDto.RequestId,
                    WineId= wineCheckDto.WineId,
                    Quantity= wineCheckDto.Quantity,
                    Status= wineCheckDto.Status,
                    CheckDate= wineCheckDto.CheckDate,
                    ImageUrl= wineCheckDto.ImageUrl
                    
                    
                };

          
                return View(wineCheckEditViewModel); // Truyền đối tượng wineRequestEditViewModel cho view
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(WineCheckDTO wineCheckDTO)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Tạo URL cho API update
                    var updateUrl = $"http://localhost:5067/odata/winecheck/update?id={wineCheckDTO.CheckId}";

                    // Gửi yêu cầu PUT để cập nhật WineRequest
                    var response = await _httpClient.PutAsJsonAsync(updateUrl, wineCheckDTO);

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
            return View(wineCheckDTO);
        }
    }
}
