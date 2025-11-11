using MyWebApi.Models;
using MyWebApi.Repositories.Interfaces;
using MyWebApi.Services.Interfaces;

namespace MyWebApi.Services.Implementations
{
    public class TodoItemService : ITodoItemService
    {
        private readonly ITodoItemRepository _repository;

        public TodoItemService(ITodoItemRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<TodoItem>> GetAllAsync()
        {
            return await _repository.GetAsync();
        }

        public async Task<TodoItem?> GetByIdAsync(string id)
        {
            return await _repository.GetAsync(id);
        }

        public async Task CreateAsync(TodoItem todo)
        {
            await _repository.CreateAsync(todo);
        }

        public async Task<bool> UpdateAsync(TodoItem todo)
        {
            var existing = await _repository.GetAsync(todo.Id);
            if (existing == null)
                return false; 

            await _repository.UpdateAsync(todo);
            return true;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var existing = await _repository.GetAsync(id);
            if (existing == null)
                return false;

            await _repository.DeleteAsync(id);
            return true;
        }
    }
}