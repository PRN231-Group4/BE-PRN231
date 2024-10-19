using BusinessLayer.Modal.Request;
using FE_WineManagement.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FE_WineManagement.Controllers
{
    public class AccountController : Controller
    {
        private readonly AuthServices _authServices;

        public AccountController(AuthServices authServices)
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

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {

            if (model == null || string.IsNullOrEmpty(model.Username) || string.IsNullOrEmpty(model.Password))
            {
                return BadRequest("Invalid login request");
            }
            var response = await _authServices.LoginAsync(model);
            if (response != null && response.Code == 200)
            {
                return Ok(response);
            }

            return Unauthorized(new { Message = response?.Message ?? "Đăng nhập thất bại." });
        }
    }
}
