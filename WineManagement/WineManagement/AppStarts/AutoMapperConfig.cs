using AutoMapper;
using BusinessLayer.Modal.Request;
using BusinessLayer.Modal.Request.Account;
using DataLayer.Models;
using System.Reflection;

namespace WineManagement.AppStarts
{
	public class AutoMapperConfig : Profile
	{
        public AutoMapperConfig()
        {
            CreateMap<WineDTO, Wine>().ReverseMap();

            CreateMap<WineBatchDTO, WineBatch>().ReverseMap();

            CreateMap<CategoryDTO, Category>().ReverseMap();

            CreateMap<UpdateAccountDto, Account>().ReverseMap();

            CreateMap<Account, UpdateAccountDto>().ReverseMap();
        }
    }
}
