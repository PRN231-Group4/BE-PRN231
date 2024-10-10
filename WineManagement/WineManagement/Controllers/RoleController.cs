﻿using BusinessLayer.Modal.Request;
using BusinessLayer.Service.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WineManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateRole(int id, RoleDTO dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                bool isUpdated = await _roleService.Update(id, dto);

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
        public async Task<IActionResult> CreateRole(RoleDTO dto)
        {
            try
            {
                var data = await _roleService.Create(dto);
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
        public async Task<IActionResult> DeleteRole(int id)
        {
            try
            {
                var result = await _roleService.Delete(id);
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
        public async Task<IActionResult> GetAllRole()
        {
            try
            {
                var result = await _roleService.GetAll();
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
        public async Task<IActionResult> GetRoleById(int id)
        {
            try
            {
                var result = await _roleService.GetById(id);
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
