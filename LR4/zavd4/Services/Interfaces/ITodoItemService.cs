using MyWebApi.Models;

namespace MyWebApi.Services.Interfaces
{
    public interface ITodoItemService
    {
        Task<IEnumerable<TodoItem>> GetAllAsync();
        Task<TodoItem?> GetByIdAsync(string id);
        Task CreateAsync(TodoItem item);
        Task<bool> UpdateAsync(TodoItem item);
        Task<bool> DeleteAsync(string id);

    }
}