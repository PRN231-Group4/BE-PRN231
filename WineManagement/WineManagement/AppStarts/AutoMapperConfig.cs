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
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name)); CreateMap<WineBatchDTO, WineBatch>().ReverseMap();
            CreateMap<Wine, WineDTORespond>().ReverseMap();


            CreateMap<CategoryDTO, Category>().ReverseMap();
            CreateMap<RoleDTO, Role>().ReverseMap();
            CreateMap<SupplierDTO, Supplier>().ReverseMap();
            CreateMap<WineRequest, WineRequestDTO>().ForMember(dest => dest.SupplierName, opt => opt.Ignore())
                                                    .ForMember(dest => dest.ManagerName, opt => opt.Ignore())
                                                    .ForMember(dest => dest.Wine, opt => opt.Ignore());

            CreateMap<WineRequest, WineRequestCRUDDTO>().ReverseMap();
            CreateMap<WineCheck, WineCheckDTO>().ReverseMap();




        }


    }
}
