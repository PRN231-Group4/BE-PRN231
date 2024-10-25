using BusinessLayer.Modal.Request;
using BusinessLayer.Modal.Response;
using BusinessLayer.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WineManagement.Controllers
{
	[Route("odata/[controller]")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		private readonly IAuthServices _authService;

		public AuthController(IAuthServices authServices)
		{
			_authService = authServices;
		}

        [HttpPost]
        [Route("Login"),AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var sss = Request;
            if (model == null)
            {
                return BadRequest("Invalid login request");
            }

            var response = await _authService.AuthenticateAsync(model.Username, model.Password);
            if (response.Code == 200)
            {
                return Ok(response);
            }

            return Unauthorized(new { Message = response.Message });
        }

		[HttpPost]
		[Route("register")]
		public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new BaseResponse<TokenModel>
                {
                    Code = 400,
                    Message = "Invalid request data"
                });
            }

			var result = await _authService.RegisterAsync(model);

            // Trả về kết quả đăng ký
			if(result.Code == 201)
			{
                return Created("api/register", result);
            }
            else if (result.Code == 409)
            {
                return Conflict(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

    }
}
