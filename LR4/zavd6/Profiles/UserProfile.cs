using AutoMapper;
using MyWebApi.Entities;
using MyWebApi.Dtos;

namespace MyWebApi.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, RegisterDto>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName));
            CreateMap<RegisterDto, User>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName))
                .ForMember(dest => dest.Id, opt => opt.Ignore())  // Ignore Id for creation
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore());  // Password handled separately

            CreateMap<RegisterDto, User>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName))
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore());
        }
    }
}
