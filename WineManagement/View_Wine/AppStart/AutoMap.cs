using AutoMapper;
using BusinessLayer.Modal.Request;
using BusinessLayer.Modal.Response;
using DataLayer.Models;
using View_Wine.Models;

namespace View_Wine.AppStart
{
    public class AutoMap : Profile
    {
        public AutoMap()
        {
            CreateMap<WineRequestDTO, WineRequestModal>().ReverseMap();
        }

           

    }
}
