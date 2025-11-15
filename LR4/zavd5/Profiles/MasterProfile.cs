using AutoMapper;
using MyWebApi.Models;

namespace MyWebApi.Profiles
{
    public class MasterProfile:Profile
    {
        public MasterProfile()
        {
            CreateMap<Master, MasterDto>()
                .ForMember(d => d.Name, o => o.MapFrom(s => s.FullName));

            CreateMap<MasterDto, Master>()
                .ForMember(d => d.FullName, o => o.MapFrom(s => s.Name));

        }
    }
}
