using AutoMapper;
using Teeleh.Models;
using Teeleh.Models.Dtos;

namespace Teeleh.WApi.App_Start
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
           var AdConfig_1 = new MapperConfiguration(cfg=>
           {
               cfg.CreateMap<Advertisement, AdvertisementDto>();
               cfg.CreateMap<AdvertisementDto, Advertisement>();
           });
        }
    }
}