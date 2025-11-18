using MongoDB.Driver;
using MyWebApi.Entities;
using MyWebApi.DTOs;

namespace MyWebApi.Repositories.Interfaces
{
    public interface ITodoItemRepository
    {
        Task CreateAsync(TodoItem todoItem);
        Task<List<TodoItem>> GetAsync();
        Task<TodoItem> GetAsync(string id);
        Task<List<TodoItem>> GetByUserIdAsync(string userId);
        Task UpdateAsync(TodoItem todoItem);
        Task DeleteAsync(string id);
    }
}