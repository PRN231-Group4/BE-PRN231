using BusinessLayer.Modal.Request;
using BusinessLayer.Modal.Response;
using BusinessLayer.Service.Interface;
using DataLayer.UnitOfWork;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Service
{
	public class AuthServices : IAuthServices
	{
		private readonly IConfiguration _configuration;
		private readonly IAccountService _accountService;
		private readonly IUnitOfWork _unitOfWork;

		public AuthServices(IConfiguration configuration, IAccountService accountService, IUnitOfWork unitOfWork)
		{
			_configuration = configuration;
			_accountService = accountService;
			_unitOfWork = unitOfWork;
		}

		public async Task<<LoginResponseModel> AuthenticateAsync(string username, string password)
		{
			var account = await _accountService.GetAccountByUsernameAsync(username);

			if (account != null && VerifyPassword(password, account.Password))
			{
				if (account.Status == "inactive")
				{
					Code = 403,
					Message = "Account is inactive",
					return new BaseResponseForLogin<LoginResponseModel>()
					{
						
						Account = new AccountResponseModel
						{
							AccountId = account.AccountId,
							UserName = account.Username,
							RoleId = account.RoleId,
							Status = "Inactive"
						}
					};
				}

				return new LoginResponseModel
				{
					Code = 200,
					Message = "Login successful",
					User = new AccountResponseModel
					{
						AccountId = user.Id,
						UserName = user.UserName,
						Email = user.Email,
						RoleId = user.RoleId,
						Status = "Active"
					}
				};
			}

			return new LoginResponseModel
			{
				Code = 404,
				Message = "Username or Password incorrect",
				User = null
			};
		}

		public string HashPassword(string password)
		{
			byte[] salt = new byte[16];
			using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
			{
				rng.GetBytes(salt);
			}

			var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000);
			byte[] hash = pbkdf2.GetBytes(20);

			byte[] hashBytes = new byte[36];
			Array.Copy(salt, 0, hashBytes, 0, 16);
			Array.Copy(hash, 0, hashBytes, 16, 20);
			string hashedPassword = Convert.ToBase64String(hashBytes);

			return hashedPassword;
		}

		public bool VerifyPassword(string password, string hashedPassword)
		{
			byte[] hashBytes = Convert.FromBase64String(hashedPassword);
			byte[] salt = new byte[16];
			Array.Copy(hashBytes, 0, salt, 0, 16);
			byte[] hash = new byte[20];
			Array.Copy(hashBytes, 16, hash, 0, 20);

			var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000);
			byte[] computedHash = pbkdf2.GetBytes(20);

			for (int i = 0; i < 20; i++)
			{
				if (hash[i] != computedHash[i])
				{
					return false;
				}
			}
			return true;
		}


	}
}
