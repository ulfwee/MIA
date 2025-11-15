using AutoMapper;
using MyWebApi.Models;

namespace MyWebApi.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDto>()
                .ForMember(d => d.Name, o => o.MapFrom(s => s.FullName));

            CreateMap<UserDto, User>()
                .ForMember(d => d.FullName, o => o.MapFrom(s => s.Name));

        }
    }
}
