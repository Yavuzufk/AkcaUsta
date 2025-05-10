using AkcaUsta.Dtos.AboutDtos;
using AkcaUsta.Dtos.BuisnessDataDtos;
using AkcaUsta.Dtos.ServiceDtos;
using AkcaUsta.Dtos.ServiceFeatureDtos;
using AkcaUsta.Dtos.ServiceRandevuDtos;
using AkcaUsta.Entity;
using AutoMapper;

namespace AkcaUsta.Mapping
{
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<About, ResultAboutDto>().ReverseMap();

            CreateMap<BuisnessData, ResultBuisnessDataDto>().ReverseMap();

            CreateMap<Service, ResultServiceDto>().ReverseMap();
            CreateMap<Service, CreateServiceDto>().ReverseMap();

            CreateMap<ServiceFeature, ResultServiceFeatureDto>().ReverseMap();
            CreateMap<ServiceFeature, CreateServiceFeatureDto>().ReverseMap();

            CreateMap<ServiceRandevu, ResultServiceRandevuDto>().ReverseMap();
            CreateMap<ServiceRandevu, CreateServiceRandevuDto>().ReverseMap();
        }
    }
}
