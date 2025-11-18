using AutoMapper;
using MyWebApi.Entities;
using MyWebApi.Dtos;

namespace MyWebApi.Profiles
{
    public class TodoItemProfile:Profile
    {
        public TodoItemProfile()
        {
            CreateMap<TodoItem, TodoItemDto>()
                .ForMember(dest => dest.IsComplete, opt => opt.MapFrom(src => src.IsComplete));
            CreateMap<TodoItemDto, TodoItem>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.IsComplete, opt => opt.MapFrom(src => src.IsComplete));
        }
    }
}
