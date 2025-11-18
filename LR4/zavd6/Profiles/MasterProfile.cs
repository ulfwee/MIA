using AutoMapper;
using MyWebApi.Entities;
using MyWebApi.Dtos;

namespace MyWebApi.Profiles
{
    public class MasterProfile:Profile
    {
        public MasterProfile()
        {
            CreateMap<Master, MasterDto>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName))
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category.ToString()));
            CreateMap<MasterDto, Master>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName))
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => Enum.Parse<Category>(src.Category)));
        }
    }
}
