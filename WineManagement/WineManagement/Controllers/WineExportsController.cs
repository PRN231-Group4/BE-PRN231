using BusinessLayer.Modal.Request;
using BusinessLayer.Service;
using BusinessLayer.Service.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace WineManagement.Controllers
{
    [Route("odata/[controller]/[action]")]
    [ApiController]
    public class WineExportsController : ODataController
    {
        private readonly IWineRequestService _WineRequestService;
        private readonly IWineCheckService _WineCheckService;
        private readonly IWineExportService _WineExportService;

        public WineExportsController(IWineRequestService wineRequestService, IWineCheckService wineCheckService, IWineExportService wineExportService)
        {
            _WineRequestService = wineRequestService;
            _WineCheckService = wineCheckService;
            _WineExportService = wineExportService;
        }

        [EnableQuery]
        //[Authorize(Roles = "Staff")]
        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateWineRequest(int id, WineRequestCRUDDTO dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                bool isUpdated = await _WineRequestService.Update(id, dto);

                if (isUpdated)
                {
                    // Return a success response
                    return Ok();
                }
                else
                {
                    // Return a not found response if the service was not updated successfully
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                // Return a bad request response for any other exceptions
                return BadRequest();
            }
        }
        [EnableQuery]
        //[Authorize(Roles = "Manager")]
        [HttpPost("create")]
        public async Task<IActionResult> CreateWineRequest(WineRequestCRUDDTO dto)
        {
            try
            {
                var data = await _WineRequestService.Create(dto);
                if (data == null)
                {
                    return BadRequest();
                }
                return Ok(data);
            }
            catch (Exception ex)
            {

                return BadRequest();
            }

        }

        [EnableQuery]
        //[Authorize(Roles = "Staff")]
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteWineRequest(int id)
        {
            try
            {
                var result = await _WineRequestService.Delete(id);
                if (result)
                {
                    return Ok();
                }
                return BadRequest();
            }
            catch (Exception ex)
            {

                return BadRequest();
            }
        }

        [EnableQuery]
        //[Authorize(Roles = "Staff,Manager")]
        [HttpGet]
        public async Task<IActionResult> GetAllWineRequest()
        {
            try
            {
                var result = await _WineRequestService.GetAll();
                if (result == null)
                {
                    return NotFound();
                }
                return Ok(result);

            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [EnableQuery]
        //[Authorize(Roles = "Staff,Manager")]
        [HttpGet("all-staff")]
        public async Task<IActionResult> GetAllStaff()
        {
            try
            {
                var result = await _WineRequestService.GetAllStaffAccounts();
                if (result == null)
                {
                    return NotFound();
                }
                return Ok(result);

            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [EnableQuery]
        //[Authorize(Roles = "Staff")]
        [HttpGet("get-by-id/{id}")]
        public async Task<IActionResult> GetWineRequestById(int id)
        {
            try
            {
                var result = await _WineRequestService.GetByUserId(id);
                if (result == null)
                {
                    return NotFound();
                }
                return Ok(result);

            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [EnableQuery]
        //[Authorize(Roles = "Staff")]
        [HttpGet("get-detail-by-req-id/{id}")]
        public async Task<IActionResult> GetDetailWineRequestById(int id)
        {
            try
            {
                var result = await _WineRequestService.GetDetailByReqId(id);
                if (result == null)
                {
                    return NotFound();
                }
                return Ok(result);

            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        //=============================Status=================================

        [HttpPut("update-status-export/{id}/{status}")]
        public async Task<IActionResult> UpdateWineExportStatus(int id, WineRequestCRUDDTO dto, string status)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                bool isUpdated = false;

                switch (status.ToLower())
                {
                    case "success":
                        isUpdated = await _WineExportService.UpdateStatusSuccessExport(id, dto);
                        break;

                    case "failed":
                        isUpdated = await _WineExportService.UpdateStatusFailedExport(id, dto);
                        break;
                    case "checked":
                        isUpdated = await _WineExportService.UpdateStatusCheckedExport(id, dto);
                        break;
                    default:
                        return BadRequest("Invalid status value.");
                }

                if (isUpdated)
                {
                    // Return a success response
                    return Ok();
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                // Log the exception (optional)
                Console.WriteLine($"Error updating status: {ex.Message}");
                return BadRequest();
            }
        }
        [HttpPut("update-status-export/{id}/{status}")]
        public async Task<IActionResult> UpdateWinePickStatus(int id, WineCheckDTO dto, string status)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                bool isUpdated = false;

                switch (status.ToLower())
                {
                    case "success":
                        isUpdated = await _WineCheckService.UpdateStatusPickSuccess(id, dto);
                        break;

                    case "failed":
                        isUpdated = await _WineCheckService.UpdateStatusPickFailed(id, dto);
                        break;

                    default:
                        return BadRequest("Invalid status value.");
                }

                if (isUpdated)
                {
                    return Ok();
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating status: {ex.Message}");
                return BadRequest();
            }
        }
    }
}
