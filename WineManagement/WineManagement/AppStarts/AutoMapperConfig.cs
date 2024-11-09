using AutoMapper;
using BusinessLayer.Modal;
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
            // Ánh xạ cho WineRequest
            CreateMap<WineRequest, WineRequestDTO>()
                .ForMember(dest => dest.SupplierName, opt => opt.Ignore())
                .ForMember(dest => dest.ManagerName, opt => opt.Ignore());

            // Ánh xạ cho WineRequestCRUDDTO
            CreateMap<WineRequest, WineRequestCRUDDTO>().ReverseMap();

            // Ánh xạ cho WineCheck
            CreateMap<WineCheckDTO, WineCheck>();
            CreateMap<WineCheck, WineCheckDTO>();
            CreateMap<WineCheck, WineCheckDTO>()
            .ForMember(dest => dest.wineName, opt => opt.Ignore())    // Bỏ qua để lấy sau
            .ForMember(dest => dest.InspectorName, opt => opt.Ignore()); // Bỏ qua để lấy sau

            // Đảm bảo chỉ lấy `WineId` cho WineCheckDTO, không ánh xạ toàn bộ `Wine`
            CreateMap<Wine, WineCheckDTO>()
                .ForMember(dest => dest.wineName, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.WineId, opt => opt.MapFrom(src => src.WineId));
            CreateMap<Wine, WineDTOStatus>().ReverseMap();


            CreateMap<ReportRequest, Report>().ReverseMap();

        }


    }
}
