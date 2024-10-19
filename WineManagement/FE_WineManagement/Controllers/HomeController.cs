using BusinessLayer.Modal.Request.Account;
using FE_WineManagement.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FE_WineManagement.Controllers
{
    public class HomeController : Controller
    {
        private readonly AccountService _accountService;

        public HomeController(AccountService accountService)
        {
            _accountService = accountService;
        }

        public async Task<IActionResult> Index()
        {
            var accounts = await _accountService.GetAllAccountsAsync();
            return View(accounts);
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
