using AutoMapper;
using MyWebApi.Models;

namespace MyWebApi.Profiles
{
    public class TodoItemProfile:Profile
    {
        public TodoItemProfile()
        {
            CreateMap<TodoItem, TodoItemDto>()
                .ForMember(d => d.Task, o => o.MapFrom(s => s.FullTask));

            CreateMap<TodoItemDto, TodoItem>()
                .ForMember(d => d.FullTask, o => o.MapFrom(s => s.Task));

        }
    }
}
