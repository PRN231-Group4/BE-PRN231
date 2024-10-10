﻿using BusinessLayer.Modal.Request;
using BusinessLayer.Modal.Response;
using BusinessLayer.Service.Interface;
using DataLayer.Models;
using DataLayer.UnitOfWork;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
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

		public async Task<BaseResponseForLogin<LoginResponseModel>> AuthenticateAsync(string username, string password)
		{
			var account = await _accountService.GetAccountByUsernameAsync(username);

			if (account != null && VerifyPassword(password, account.Password))
			{
				var userWithRole = await _accountService.GetAccountByUsernameAsync(account.Username);
				string token = GenerateJwtToken(account.Username, userWithRole.Role.Name, account.AccountId);
				//var bannedAccount = await _unitOfWork.Repository<BannedAccount>().FindAsync(ba => ba.UserId == user.Id && ba.Status == true);

				if (account.Status == "inactive")
				{
					return new BaseResponseForLogin<LoginResponseModel>()
					{
						Code = 404,
						Message = "Your Account has been banned. Check email for reason",
						Data = new LoginResponseModel()
						{
							Account = new AccountResponseModel()
							{
								AccountId= account.AccountId,
								Username = account.Username,
								RoleId= (int)account.RoleId,
								Status= account.Status
							},
						},
						IsBanned = true,
					};
				}

				return new BaseResponseForLogin<LoginResponseModel>()
				{
					Code = 200,
					Message = "",
					Data = new LoginResponseModel()
					{
						Token = token,
						Account = new AccountResponseModel()
						{
							AccountId = account.AccountId,
							Username = account.Username,
							RoleId = (int)account.RoleId,
							Status = account.Status
						},
					},
					IsBanned = false
				};
			}
			return new BaseResponseForLogin<LoginResponseModel>()
			{
				Code = 404,
				Message = "Username or Password incorrect",
				Data = null,
				IsBanned = false
			};
		}


		public async Task<BaseResponse<TokenModel>> RegisterAsync(RegisterModel registerModel)
		{
			var existingUser = await _unitOfWork.Repository<Account>().FindAsync(u => u.Username == registerModel.Username);

			if (existingUser != null)
			{
				return new BaseResponse<TokenModel>
				{
					Code = 409,
					Message = "Username already exists",
				};
			}

			var account = new Account()
			{
				RoleId = 2,
				Username = registerModel.Username,
				Password = HashPassword(registerModel.Password),
				Status = "active",
			};

			await _unitOfWork.Repository<Account>().InsertAsync(account);
			await _unitOfWork.CommitAsync();

			var userWithRole = await _accountService.GetAccountByUsernameAsync(account.Username);
			string token = GenerateJwtToken(account.Username, userWithRole.Role.Name, account.AccountId);

			return new BaseResponse<TokenModel>
			{
				Code = 201,
				Message = "Register successfully",
				Data = new TokenModel
				{
					Token = token
				}
			};
		}

		public string GenerateJwtToken(string username, string roleName, int userId)
		{
			var tokenHandler = new JwtSecurityTokenHandler();
			var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);

			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(new[]
				{
					new Claim(ClaimTypes.Name, username),
					new Claim(ClaimTypes.Role, roleName),
					new Claim(ClaimTypes.NameIdentifier, userId.ToString())
				}),
				Expires = DateTime.UtcNow.AddHours(24),
				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
			};

			var token = tokenHandler.CreateToken(tokenDescriptor);
			return tokenHandler.WriteToken(token);
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
