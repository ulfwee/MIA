using MyWebApi.Entities;
using MyWebApi.Dtos;
namespace MyWebApi.Services.Interfaces
{
    public interface ITodoItemService
    {
        Task CreateAsync(TodoItem todoItem);
        Task<List<TodoItem>> GetAsync();
        Task<TodoItem> GetAsync(string id);
        Task<List<TodoItem>> GetByUserIdAsync(string userId);
        Task UpdateAsync(TodoItem todoItem);
        Task DeleteAsync(string id);
    }
}