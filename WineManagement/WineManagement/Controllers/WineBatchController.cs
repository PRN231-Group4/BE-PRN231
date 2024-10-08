﻿using BusinessLayer.Modal.Request;
using BusinessLayer.Service.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WineManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WineBatchController : ControllerBase
    {
        private readonly IWineBatchService _wineBatchService;

        public WineBatchController(IWineBatchService wineBatchService)
        {
            _wineBatchService = wineBatchService;
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateWineBatch(int id, WineBatchDTO dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                bool isUpdated = await _wineBatchService.Update(id, dto);

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
        public async Task<IActionResult> CreateWineBatch(WineBatchDTO dto)
        {
            try
            {
                var data = await _wineBatchService.Create(dto);
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
        public async Task<IActionResult> DeleteWineBatch(int id)
        {
            try
            {
                var result = await _wineBatchService.Delete(id);
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
        public async Task<IActionResult> GetAllWineBatch()
        {
            try
            {
                var result = await _wineBatchService.GetAll();
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
        public async Task<IActionResult> GetWineBatchById(int id)
        {
            try
            {
                var result = await _wineBatchService.GetById(id);
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