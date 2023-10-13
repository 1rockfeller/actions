using AutoMapper;
using Stats.API.Dto;
using Stats.API.Models;

namespace Stats.API.Profiles
{
    public class SundayDataProfile : Profile
    {
        public SundayDataProfile()
        {
            CreateMap<SundayDataDto,SundayData>()
                .ReverseMap();

            CreateMap<PlatformDto, Platform>()
                .ReverseMap();

            //CreateMap<SundayDataDto, SundayData>()
            //    .ForMember(dest => dest.SundayDataId,
            //        opt => opt.MapFrom(src => src.SundayDataId));

        }
    }
}
