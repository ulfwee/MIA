using AutoMapper;
using MyWebApi.Models;
using MyWebApi.Repositories.Interfaces;
using MyWebApi.Services.Interfaces;

namespace MyWebApi.Services.Implementations
{
    public class TodoItemService : ITodoItemService
    {
        private readonly ITodoItemRepository _repository;
        private readonly IMapper _mapper;

        public TodoItemService(ITodoItemRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TodoItemDto>> GetAllAsync()
        {
            var todos = await _repository.GetAsync();
            return _mapper.Map<IEnumerable<TodoItemDto>>(todos);
        }

        public async Task<TodoItemDto?> GetByIdAsync(string id)
        {
            var todo = await _repository.GetAsync(id);
            return _mapper.Map<TodoItemDto?>(todo);
        }

        public async Task CreateAsync(TodoItemDto todoDto)
        {
            var todo = _mapper.Map<TodoItem>(todoDto);
            await _repository.CreateAsync(todo);
        }

        public async Task<bool> UpdateAsync(TodoItemDto todoDto)
        {
            var existing = await _repository.GetAsync(todoDto.Id);
            if (existing == null)
                return false;

            var updated = _mapper.Map<TodoItem>(todoDto);
            await _repository.UpdateAsync(updated);
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