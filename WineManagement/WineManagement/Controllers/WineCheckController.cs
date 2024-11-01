using BusinessLayer.Modal.Request;
using BusinessLayer.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace WineManagement.Controllers
{

    [Authorize(Roles = "Staff,Manager")]

    [Route("odata/[controller]")]
    [ApiController]
    public class WineCheckController : ODataController
    {
        private readonly IWineCheckService _WineCheckService;

        public WineCheckController(IWineCheckService WineCheckService)
        {
            _WineCheckService = WineCheckService;
        }


        [EnableQuery]
        //[Authorize(Roles = "Staff")]
        [HttpPut("update")]
        public async Task<IActionResult> UpdateWineCheck(int id, WineCheckDTO dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                bool isUpdated = await _WineCheckService.Update(id, dto);

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
        //[Authorize(Roles = "Staff")]
        [HttpPost("create")]
        public async Task<IActionResult> CreateWineCheck(WineCheckDTO dto)
        {
            try
            {
                var data = await _WineCheckService.Create(dto);
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
        public async Task<IActionResult> DeleteWineCheck(int id)
        {
            try
            {
                var result = await _WineCheckService.Delete(id);
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
        //[Authorize(Roles = "Staff")]
        [HttpGet]
        public async Task<IActionResult> GetAllWineCheck()
        {
            try
            {
                var result = await _WineCheckService.GetAll();
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
        public async Task<IActionResult> GetWineCheckById(int id)
        {
            try
            {
                var result = await _WineCheckService.GetById(id);
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
