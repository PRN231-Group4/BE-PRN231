using BusinessLayer.Modal.Request;
using BusinessLayer.Service.Interface;
using DataLayer.Repository.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace WineManagement.Controllers
{
    [Route("odata/[controller]")]
    [ApiController]
    public class WineRequestController : ODataController
    {
        private readonly IWineRequestService _WineRequestService;

        public WineRequestController(IWineRequestService WineRequestService)
        {
            _WineRequestService = WineRequestService;
        }


        [EnableQuery]
        //[Authorize(Roles = "Staff")]
        [HttpPut("update")]
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
        [HttpDelete("delete")]
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
        [HttpGet("get-by-id")]
        public async Task<IActionResult> GetWineRequestById(int id)
        {
            try
            {
                var result = await _WineRequestService.GetById(id);
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
    }
}
