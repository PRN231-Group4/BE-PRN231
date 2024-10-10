using BusinessLayer.Modal.Response;
using BusinessLayer.Service.Interface;
using DataLayer.GenericRepository;
using DataLayer.Models;
using DataLayer.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Service
{
	public class AccountServices : IAccountService
	{
		private readonly IUnitOfWork _unitOfWork;

		public AccountServices(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public IEnumerable<Account> GetAccounts()
		{
			return _unitOfWork.Repository<Account>().GetAll();

		}

		public async Task<Account> GetAccountByIdAsync(int id)
		{
			return await _unitOfWork.Repository<Account>().GetById(id);
		}

		public async Task CreateAccountAsync(Account account)
		{
			await _unitOfWork.Repository<Account>().InsertAsync(account);
			await _unitOfWork.CommitAsync();
		}

		public async Task UpdateAccountAsync(Account account)
		{
			await _unitOfWork.Repository<Account>().Update(account, account.AccountId);
			await _unitOfWork.CommitAsync();
		}

		public async Task DeleteAccountAsync(int id)
		{
			var account = await _unitOfWork.Repository<Account>().GetById(id);
			if (account != null)
			{
				_unitOfWork.Repository<Account>().Delete(account);
				await _unitOfWork.CommitAsync();
			}
		}

		public async Task<bool> AccountExistsAsync(int id)
		{
			var account = await _unitOfWork.Repository<Account>().GetById(id);
			return account != null;
		}

		public async Task<Account> GetAccountByUsernameAsync(string username)
		{
			// Truy vấn người dùng từ cơ sở dữ liệu theo tên người dùng
			var user = await _unitOfWork.Repository<Account>()
				.GetAll()
				.Include(u => u.Role)
				.FirstOrDefaultAsync(u => u.Username == username);

			return user;
		}

		



		public async Task<AccountResponseModel> GetAccountProfile(int id)
		{
			var account = await _unitOfWork.Repository<Account>().GetById(id);

			if (account == null)
			{
				throw new Exception($"Account with ID {id} not found.");
			}

			var responseModel = new AccountResponseModel
			{
				AccountId = account.AccountId,
				Username = account.Username,
				RoleId = (int)account.RoleId,
				Status = account.Status
			};

			return responseModel;
		}

	}
}
