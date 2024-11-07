using AutoMapper;
using BusinessLayer.Modal.Request;
using BusinessLayer.Service.Interface;
using CloudinaryDotNet;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WineManagement.Controllers
{
    [Route("odata/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;

        public AccountController(IAccountService accountService, IMapper mapper)
        {
            _accountService = accountService;
            _mapper = mapper;
        }

        [HttpGet("GetAllAccount")]
        public async Task<ActionResult<IEnumerable<Account>>> GetAllAccount()
        {
            var accounts = await _accountService.GetAllAccounts();
            return Ok(accounts);
        }

        [HttpGet]
        [Route("GetAccountById/{accountId}")]
        public async Task<IActionResult> GetAccountById(int accountId)
        {
            var account = await _accountService.GetAccountByIdAsync(accountId);
            if (account == null)
            {
                return NotFound(new { Message = "Account not found" });
            }
            return Ok(account);
        }

        [HttpPut]
        [Route("UpdateAccount")]
        public async Task<IActionResult> UpdateAccount(int accountId, [FromBody] UpdateAccountDto updateAccountDto)
        {
            var account = await _accountService.GetAccountByIdAsync(accountId);
            if (account == null)
            {
                return NotFound(new { Message = $"Không tìm thấy người dùng có ID {accountId}." });
            }

            var accountDto = _mapper.Map(updateAccountDto, account);


            // Cập nhật thông tin tài khoản

            await _accountService.UpdateAccountAsync(accountDto);

            return NoContent();

        }


        [HttpDelete]
        [Route("DeleteUser/{accountId}")]
        public async Task<IActionResult> DeleteUser(int accountId)
        {
            var account = await _accountService.GetAccountByIdAsync(accountId);
            if (account == null)
            {
                return NotFound(new { Message = $"Không tìm thấy người dùng có ID {accountId}." });
            }

            await _accountService.DeleteAccountAsync(accountId);

            return NoContent();

        }

    }
}
