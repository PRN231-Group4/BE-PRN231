using AutoMapper;
using BusinessLayer.Modal.Request;
using BusinessLayer.Modal.Response;
using DataLayer.Models;
using System.Reflection;
using View_Wine.Models;

namespace WineManagement.AppStarts
{
	public class AutoMapperConfig : Profile
	{
        public AutoMapperConfig()
        {
            CreateMap<Wine, WineDTO>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name));
            CreateMap<Wine, WineDTORespond>().ReverseMap();

            CreateMap<WineBatchDTO, WineBatch>().ReverseMap();
            CreateMap<CategoryDTO, Category>().ReverseMap();
            CreateMap<RoleDTO, Role>().ReverseMap();
            CreateMap<SupplierDTO, Supplier>().ReverseMap();

                       

        }


    }
}
