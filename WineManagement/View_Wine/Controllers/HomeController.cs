using BusinessLayer.Modal.Request;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using View_Wine.Models;
using View_Wine.Services;


namespace View_Wine.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AccountService _accountService;

        public HomeController(ILogger<HomeController> logger, AccountService accountService)
        {
            _logger = logger;
            _accountService = accountService;
        }

        public async Task<IActionResult> Index()
        {
            var accounts = await _accountService.GetAllAccountsAsync();
            return View(accounts);

        }

        //public IActionResult Privacy()
        //{
        //    return View();
        //}


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            HttpContext.Session.Clear(); // Xóa session
            await HttpContext.SignOutAsync(); // Đăng xuất người dùng
            return RedirectToAction("Index", "Login"); // Chuyển hướng về trang đăng nhập
        }

        [HttpGet]
        public async Task<IActionResult> GetAccountDetails(int accountId)
        {
            var account = await _accountService.GetAccountByIdAsync(accountId);
            if (account == null)
            {
                return NotFound();
            }
            // Trả về JSON chứa thông tin tài khoản
            return Json(new
            {
                accountId = account.AccountId,
                username = account.Username,
                status = account.Status,
                roleId = account.RoleId
            });
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAccount(int accountId, [FromBody] UpdateAccountDto updateAccountDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var isSuccess = await _accountService.UpdateAccountAsync(accountId, updateAccountDto);

            if (isSuccess)
            {
                // Trả về 204 NoContent khi cập nhật thành công
                return NoContent();
            }
            else
            {
                return StatusCode(500, new { message = "Cập nhật tài khoản thất bại." });
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteUser(int accountId)
        {
            if (accountId <= 0)
            {
                return BadRequest(new { message = "ID tài khoản không hợp lệ." });
            }

            var isDeleted = await _accountService.DeleteAccountAsync(accountId);

            if (isDeleted)
            {
                return NoContent(); // Trả về 204 No Content nếu xóa thành công
            }
            else
            {
                return StatusCode(500, new { message = "Xóa tài khoản thất bại." });
            }
        }

    }
}
