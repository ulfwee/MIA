using MyWebApi.Models;

namespace MyWebApi.Services.Interfaces
{
    public interface ITodoItemService
    {
        Task<IEnumerable<TodoItemDto>> GetAllAsync();
        Task<TodoItemDto?> GetByIdAsync(string id);
        Task CreateAsync(TodoItemDto todoDto);
        Task<bool> UpdateAsync(TodoItemDto todoDto);
        Task<bool> DeleteAsync(string id);
    }
}