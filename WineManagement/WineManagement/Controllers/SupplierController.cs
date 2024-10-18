using BusinessLayer.Modal.Request;
using BusinessLayer.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace WineManagement.Controllers
{
    [Route("odata/[controller]")]
    [ApiController]
    public class SupplierController : ODataController
    {
        private readonly ISupplierService _supplierService;

        public SupplierController(ISupplierService supplierService)
        {
            _supplierService = supplierService;
        }

        [EnableQuery]
        [Authorize(Roles = "Staff")]
        [HttpPut("update")]
        public async Task<IActionResult> UpdateSupplier(int id, SupplierDTO dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                bool isUpdated = await _supplierService.Update(id, dto);

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
        [Authorize(Roles = "Staff")]
        [HttpPost("create")]
        public async Task<IActionResult> CreateSupplier(SupplierDTO dto)
        {
            try
            {
                var data = await _supplierService.Create(dto);
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
        [Authorize(Roles = "Staff")]
        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteSupplier(int id)
        {
            try
            {
                var result = await _supplierService.Delete(id);
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
        public async Task<IActionResult> GetAllSupplier()
        {
            try
            {
                var result = await _supplierService.GetAll();
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
        [Authorize(Roles = "Staff")]
        [HttpGet("get-by-id")]
        public async Task<IActionResult> GetSupplierById(int id)
        {
            try
            {
                var result = await _supplierService.GetById(id);
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
