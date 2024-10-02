using BusinessLayer.Modal.Response;
using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Service.Interface
{
	public interface IAccountService
	{
		IEnumerable<Account> GetAccounts();
		Task<Account> GetAccountByIdAsync(int id);
		Task CreateAccountAsync(Account account);
		Task UpdateAccountAsync(Account account);
		Task DeleteAccountAsync(int id);
		Task<bool> AccountExistsAsync(int id);
		Task<Account> GetAccountByUsernameAsync(string username);
		Task<AccountResponseModel> GetAccountProfile(int id);
	}
}
