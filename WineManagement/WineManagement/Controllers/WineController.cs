using BusinessLayer.Modal;
using BusinessLayer.Modal.Request;
using BusinessLayer.Modal.Response;
using BusinessLayer.Service.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace WineManagement.Controllers
{
    [Route("odata/[controller]/[action]")]
    [ApiController]
    public class WineController : ODataController
    {
        private readonly IWineService _wineService;

        public WineController(IWineService wineService)
        {
            _wineService = wineService;
        }
        [HttpPut("update-status/{id}")]
        public async Task<IActionResult> UpdateWineStatusFailed(int id, WineDTOStatus dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                bool isUpdated = await _wineService.UpdateStatusFailed(id, dto);

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

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateWine(int id, WineDTORespond dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                bool isUpdated = await _wineService.Update(id, dto);

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
        [HttpPost("create")]
        public async Task<IActionResult> CreateWine(WineDTORespond dto)
        {
            try
            {
                var data = await _wineService.Create(dto);
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

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteWine(int id)
        {
            try
            {
                var result = await _wineService.Delete(id);
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
        [HttpGet]
        public async Task<IActionResult> GetAllWine()
        {
            try
            {
                var result = await _wineService.GetAll();
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
        [HttpGet("get-by-id/{id}")]
        public async Task<IActionResult> GetWineById(int id)
        {
            try
            {
                var result = await _wineService.GetById(id);
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
        [HttpGet("get-by-name")]
        public async Task<IActionResult> GetWineByName(string name)
        {
            try
            {
                var result = await _wineService.GetByName(name);
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
