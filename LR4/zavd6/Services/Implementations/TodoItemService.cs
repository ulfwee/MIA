using AutoMapper;
using MyWebApi.Entities;
using MyWebApi.Dtos;
using MyWebApi.Repositories.Interfaces;
using MyWebApi.Services.Interfaces;

namespace MyWebApi.Services.Implementations
{
    public class TodoItemService : ITodoItemService
    {
        private readonly ITodoItemRepository _todoItemRepository;
        public TodoItemService(ITodoItemRepository todoItemRepository)
        {
            _todoItemRepository = todoItemRepository;
        }
        public async Task CreateAsync(TodoItem todoItem) => await _todoItemRepository.CreateAsync(todoItem);
        public async Task DeleteAsync(string id) => await _todoItemRepository.DeleteAsync(id);
        public async Task<List<TodoItem>> GetAsync() => await _todoItemRepository.GetAsync();
        public async Task<TodoItem> GetAsync(string id) => await _todoItemRepository.GetAsync(id);
        public async Task<List<TodoItem>> GetByUserIdAsync(string userId) => await _todoItemRepository.GetByUserIdAsync(userId);
        public async Task UpdateAsync(TodoItem todoItem) => await _todoItemRepository.UpdateAsync(todoItem);
    }
}