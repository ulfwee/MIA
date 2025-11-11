    using MongoDB.Driver;
using MyWebApi.Models;
using MyWebApi.Repositories.Interfaces;
using SortTest.Test;

namespace MyWebApi.Repositories.Implementations
{
    public class TodoItemRepository : ITodoItemRepository
    {
        readonly private IMongoCollection<TodoItem> _collection;

        public TodoItemRepository()
        {
            _collection = MobgoDBClient.Instance.GetCollection<TodoItem>("Todos");
        }

        public async Task CreateAsync(TodoItem todo) => await _collection.InsertOneAsync(todo);
        public async Task DeleteAsync(string id) => await _collection.DeleteOneAsync(x => x.Id == id);
        public async Task<List<TodoItem>> GetAsync() => await _collection.Find(x => true).ToListAsync();
        public async Task<TodoItem> GetAsync(string id) => await _collection.Find(x => x.Id == id).FirstOrDefaultAsync();
        public async Task UpdateAsync(TodoItem todo) => await _collection.ReplaceOneAsync(x => x.Id == todo.Id, todo);
    }
}