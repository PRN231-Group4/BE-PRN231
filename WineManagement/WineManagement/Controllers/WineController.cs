using BusinessLayer.Modal.Request;
using BusinessLayer.Service.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WineManagement.Controllers
{
    [Route("odata/[controller]/[action]")]
    [ApiController]
    public class WineController : ControllerBase
    {
        private readonly IWineService _wineService;

        public WineController(IWineService wineService)
        {
            _wineService = wineService;
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateWine(int id, WineDTO dto)
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
        public async Task<IActionResult> CreateWine(WineDTO dto)
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

        [HttpDelete("delete")]
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
        [HttpGet("get-by-id")]
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
