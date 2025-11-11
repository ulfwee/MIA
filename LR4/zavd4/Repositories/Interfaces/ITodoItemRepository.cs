using MongoDB.Driver;
using MyWebApi.Models;

namespace MyWebApi.Repositories.Interfaces
{
    public interface ITodoItemRepository
    {
        Task CreateAsync(TodoItem todo);
        Task<List<TodoItem>> GetAsync();
        Task<TodoItem> GetAsync(string id);
        Task UpdateAsync(TodoItem todo);
        Task DeleteAsync(string id);
    }
}