using MyWebApi.Models;
using MyWebApi.Repositories.Interfaces;

namespace MyWebApi.Repositories.Implementations
{
    public class TodoItemRepository : ITodoItemRepository
    {
        private readonly List<TodoItem> _items = new()
        {
            new() { Id = 1, Name = "Купити продукти", IsComplete = false },
            new() { Id = 2, Name = "Зателефонувати клієнту", IsComplete = true }
        };
        private int _nextId = 3;

        public Task<IEnumerable<TodoItem>> GetAllAsync() => Task.FromResult(_items.AsEnumerable());

        public Task<TodoItem?> GetByIdAsync(int id) =>
            Task.FromResult(_items.FirstOrDefault(i => i.Id == id));

        public Task AddAsync(TodoItem item)
        {
            item.Id = _nextId++;
            _items.Add(item);
            return Task.CompletedTask;
        }

        public Task UpdateAsync(TodoItem item)
        {
            var existing = _items.FirstOrDefault(i => i.Id == item.Id);
            if (existing != null)
            {
                existing.Name = item.Name;
                existing.IsComplete = item.IsComplete;
            }
            return Task.CompletedTask;
        }

        public Task DeleteAsync(int id)
        {
            var item = _items.FirstOrDefault(i => i.Id == id);
            if (item != null) _items.Remove(item);
            return Task.CompletedTask;
        }
    }
}