using BusinessLayer.Modal.Request;
using FE_WineManagement.Services;
using Microsoft.AspNetCore.Mvc;

namespace FE_WineManagement.Controllers
{
    public class RegisterController : Controller
    {
        private readonly AuthServices _authServices;

        public RegisterController(AuthServices authServices)
        {
            _authServices = authServices;
        }
        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new
                {
                    Code = 400,
                    Message = "Invalid request data"
                });
            }

            var result = await _authServices.RegisterAsync(model);

            // Trả về kết quả đăng ký
            if (result)
            {
                return Ok();
            }
            else
            {
                return StatusCode(500, new { message = "Đăng ký tài khoản thất bại." });
            }
        }
    }
}
