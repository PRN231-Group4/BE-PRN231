using AutoMapper;
using BusinessLayer.Modal.Request;
using DataLayer.Enum;
using DataLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Text;
using View_Wine.Models;

namespace View_Wine.Controllers
{
    public class WineExportControllercs : Controller
    {
        Uri _baseAddress = new Uri("http://localhost:5067/odata");
        private readonly HttpClient _httpClient;
        private readonly IMapper _mapper;


        public WineExportControllercs(IHttpClientFactory httpClientFactory, IMapper mapper)
        {
            // Sử dụng IHttpClientFactory để lấy HttpClient
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri("http://localhost:5067/odata");
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var roleId = HttpContext.Session.GetInt32("roleId");
            var accountId = HttpContext.Session.GetInt32("accountId");

            // Prepare the data layer request list
            var dataLayerRequest = new List<WineRequestDTO>();

            if (roleId == 4) // role la manager
            {
                HttpResponseMessage httpResponseMessage = await _httpClient.GetAsync(_baseAddress + "/winerequest?$filter= Status ne 'FailedExport'");
                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    string data = await httpResponseMessage.Content.ReadAsStringAsync();
                    if (!data.StartsWith("["))
                    {
                        // Handle single object response
                        var wineRequest = JsonConvert.DeserializeObject<WineRequestModal>(data);
                        if (wineRequest != null)
                        {
                            dataLayerRequest.Add(new WineRequestDTO
                            {
                                RequestId = wineRequest.RequestId,
                                SupplierName = wineRequest.SupplierName,
                                ManagerName = wineRequest.ManagerName,
                                Description = wineRequest.Description,
                                Status = wineRequest.Status,
                            });
                        }
                    }
                    else
                    {
                        // Handle list response
                        var wineRequests = JsonConvert.DeserializeObject<List<WineRequestModal>>(data);
                        foreach (var wineRequest in wineRequests)
                        {
                            dataLayerRequest.Add(new WineRequestDTO
                            {
                                RequestId = wineRequest.RequestId,
                                SupplierName = wineRequest.SupplierName,
                                ManagerName = wineRequest.ManagerName,
                                Description = wineRequest.Description,
                                Status = wineRequest.Status,
                            });
                        }
                    }
                }
            }
            else if (roleId == 1) //role la staff
            {
                HttpResponseMessage httpResponseMessage = await _httpClient.GetAsync(_baseAddress + $"/wineexports/getwinerequestbyid/get-by-id/{accountId}?$filter=Status eq 'Pending'");
                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    string data = await httpResponseMessage.Content.ReadAsStringAsync();
                    Console.WriteLine(data); // Log the response for debugging

                    if (!data.StartsWith("["))
                    {
                        // Handle single object response
                        var wineRequest = JsonConvert.DeserializeObject<WineRequestModal>(data);
                        if (wineRequest != null)
                        {
                            dataLayerRequest.Add(new WineRequestDTO
                            {
                                RequestId = wineRequest.RequestId,
                                SupplierName = wineRequest.SupplierName,
                                ManagerName = wineRequest.ManagerName,
                                Description = wineRequest.Description,
                                Status = wineRequest.Status,
                            });
                        }
                    }
                    else
                    {
                        // Handle list response
                        var wineRequests = JsonConvert.DeserializeObject<List<WineRequestModal>>(data);
                        foreach (var wineRequest in wineRequests)
                        {
                            dataLayerRequest.Add(new WineRequestDTO
                            {
                                RequestId = wineRequest.RequestId,
                                SupplierName = wineRequest.SupplierName,
                                ManagerName = wineRequest.ManagerName,
                                Description = wineRequest.Description,
                                Status = wineRequest.Status,
                            });
                        }
                    }
                }
            }

            return View(dataLayerRequest);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            try
            {
                // Tạo các task cho việc gọi API
                var supplierTask = _httpClient.GetAsync(_baseAddress + "/supplier");
                var wineTask = _httpClient.GetAsync(_baseAddress + "/wine/getallwine");
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
        // POST: Create
        [HttpPost]
        public async Task<IActionResult> Create(WineRequestCRUDDTO wineRequestDto)
        {
            if (ModelState.IsValid)
            {
                var apiRequest = new WineRequestCRUDDTO
                {
                    SupplierId = wineRequestDto.SupplierId,
                    ManagerId = wineRequestDto.ManagerId,
                    RequestDate = wineRequestDto.RequestDate,
                    Description = wineRequestDto.Description,
                    Status = wineRequestDto.Status,
                    WineItems = wineRequestDto.WineItems.Select(item => new WineRequestItemDTO
                    {
                        WineId = item.WineId,
                        Quantity = item.Quantity
                    }).ToList() // Chuyển đổi danh sách WineItems sang danh sách DTO
                };
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
                    ManagerId = wineRequestDto.ManagerId,
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
            var wineRequestModal = _mapper.Map<WineRequestModal>(wineRequestDto);
            return View(wineRequestModal);
        }


        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync(_baseAddress + "/wineexports/getdetailwinerequestbyid/get-detail-by-req-id/" + id);
                if (!response.IsSuccessStatusCode)
                {
                    return NotFound();
                }

                var wineCheckData = await response.Content.ReadAsStringAsync();
                // Giả sử rằng dữ liệu trả về là một mảng
                var wineCheckDtos = JsonConvert.DeserializeObject<List<WineCheckDTO>>(wineCheckData);

                if (wineCheckDtos == null || !wineCheckDtos.Any())
                {
                    return NotFound();
                }

                // Chuyển đổi từ WineCheckDTO sang WineCheckModal
                var wineCheckEditViewModels = wineCheckDtos.Select(dto => new WineCheckModal
                {
                    InspectorId = dto.InspectorId,
                    CheckId = dto.CheckId,
                    RequestId = dto.RequestId,
                    WineId = dto.WineId,
                    Quantity = dto.Quantity,
                    Status = dto.Status,
                    CheckDate = dto.CheckDate,
                    ImageUrl = dto.ImageUrl,
                    wineName = dto.wineName // Thêm nếu cần
                }).ToList();

                return View(wineCheckEditViewModels); // Truyền danh sách cho view
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdateStatusSuccessExport(WineRequestModal modal)
        {
            try
            {
                string data = JsonConvert.SerializeObject(modal);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

                HttpResponseMessage responseMessage = await _httpClient
                    .PutAsync(_httpClient.BaseAddress + "/wineexports/updatewineexportstatus/update-status-export/" + modal.RequestId + "/success", content);

                if (responseMessage.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Export status updated successfully";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View(modal);
            }

            return View(modal);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateStatusCheckedExport(WineRequestModal modal)
        {
            try
            {
                string data = JsonConvert.SerializeObject(modal);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

                HttpResponseMessage responseMessage = await _httpClient
                    .PutAsync(_httpClient.BaseAddress + "/wineexports/updatewineexportstatus/update-status-export/" + modal.RequestId + "/checked", content);

                if (responseMessage.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Export status updated successfully";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View(modal);
            }

            return View(modal);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateStatusFailedExport(WineRequestModal modal)
        {
            try
            {
                string data = JsonConvert.SerializeObject(modal);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

                HttpResponseMessage responseMessage = await _httpClient
                    .PutAsync(_httpClient.BaseAddress + "/wineexports/updatewineexportstatus/update-status-export/" + modal.RequestId + "/failed", content);

                if (responseMessage.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Export status updated successfully";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View(modal);
            }

            return View(modal);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateStatusFailedDetailExport(WineCheckModal modal)
        {
            try
            {
                string data = JsonConvert.SerializeObject(modal);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

                HttpResponseMessage responseMessage = await _httpClient
                    .PutAsync(_httpClient.BaseAddress + "/wineexports/updatewinepickstatus/update-status-export/" + modal.CheckId + "/failed", content);

                if (responseMessage.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Export status updated successfully";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View(modal);
            }

            return View(modal);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateStatusSuccessDetailExport(WineCheckModal modal)
        {
            try
            {
                string data = JsonConvert.SerializeObject(modal);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

                HttpResponseMessage responseMessage = await _httpClient
                    .PutAsync(_httpClient.BaseAddress + "/wineexports/updatewinepickstatus/update-status-export/" + modal.CheckId + "/success", content);

                if (responseMessage.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Export status updated successfully";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View(modal);
            }

            return View(modal);
        }
    }
}
