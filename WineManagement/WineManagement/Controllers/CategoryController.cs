﻿using BusinessLayer.Modal.Request;
using BusinessLayer.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;

namespace WineManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [Authorize(Roles = "1")]

        [HttpPut("update")]
        public async Task<IActionResult> UpdateCategory(int id, CategoryDTO dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                bool isUpdated = await _categoryService.Update(id, dto);

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
        [Authorize(Roles = "1")]

        [HttpPost("create")]
        public async Task<IActionResult> CreateCategory(CategoryDTO dto)
        {
            try
            {
                var data = await _categoryService.Create(dto);
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
        [Authorize(Roles = "1")]

        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            try
            {
                var result = await _categoryService.Delete(id);
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
        [Authorize(Roles = "Member")]

        [HttpGet]
        public async Task<IActionResult> GetAllCategory()
        {
            try
            {
                var result = await _categoryService.GetAll();
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
        [Authorize(Roles = "1")]

        [HttpGet("get-by-id")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            try
            {
                var result = await _categoryService.GetById(id);
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
