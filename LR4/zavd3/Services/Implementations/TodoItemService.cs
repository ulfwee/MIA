using MyWebApi.Models;
using MyWebApi.Repositories.Interfaces;
using MyWebApi.Services.Interfaces;

namespace MyWebApi.Services.Implementations
{
    public class TodoItemService : ITodoItemService
    {
        private readonly ITodoItemRepository _repository;

        public TodoItemService(ITodoItemRepository repository) => _repository = repository;

        public Task<IEnumerable<TodoItem>> GetAllAsync() => _repository.GetAllAsync();

        public async Task<TodoItem?> GetByIdAsync(int id)
        {
            var item = await _repository.GetByIdAsync(id);
            if (item == null)
                throw new KeyNotFoundException($"TodoItem with id {id} not found");
            return item;
        }

        public async Task AddAsync(TodoItem item)
        {
            if (string.IsNullOrWhiteSpace(item.Name))
                throw new ArgumentException("Name is required");

            await _repository.AddAsync(item);
        }

        public Task UpdateAsync(TodoItem item) => _repository.UpdateAsync(item);
        public Task DeleteAsync(int id) => _repository.DeleteAsync(id);
    }
}