    using MongoDB.Driver;
using MyWebApi.Entities;
using MyWebApi.DTOs;
using MyWebApi.Repositories.Interfaces;
using SortTest.Test;

namespace MyWebApi.Repositories.Implementations
{
    public class TodoItemRepository : ITodoItemRepository
    {
        private IMongoCollection<TodoItem> _collection;
        public TodoItemRepository()
        {
            _collection = MobgoDBClient.Instance.GetCollection<TodoItem>("Todos");
        }
        // Read
        public async Task<TodoItem> GetAsync(string id) => await _collection.Find(x => x.Id == id).FirstOrDefaultAsync();
        public async Task<List<TodoItem>> GetByUserIdAsync(string userId)
            => await _collection.Find(t => t.UserId == userId).ToListAsync();
        public async Task<List<TodoItem>> GetAsync() => await _collection.Find(x => true).ToListAsync();
        // Create, Update, Delete
        public async Task CreateAsync(TodoItem todoItem) => await _collection.InsertOneAsync(todoItem);
        public async Task DeleteAsync(string id) => await _collection.DeleteOneAsync(x => x.Id == id);
        public async Task UpdateAsync(TodoItem todoItem) => await _collection.ReplaceOneAsync(x => x.Id == todoItem.Id, todoItem);
    }
}