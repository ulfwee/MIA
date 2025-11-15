using AutoMapper;
using MongoDB.Driver;
using MyWebApi.Models;
using MyWebApi.Repositories.Interfaces;
using MyWebApi.Services.Interfaces;
using SortTest.Test;

namespace MyWebApi.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserDto>> GetAllAsync()
        {
            var users = await _repository.GetAsync();
            return _mapper.Map<IEnumerable<UserDto>>(users);
        }

        public async Task<UserDto?> GetByIdAsync(string id)
        {
            var user = await _repository.GetAsync(id);
            return _mapper.Map<UserDto?>(user);
        }

        public async Task CreateAsync(UserDto userDto)
        {
            var user = _mapper.Map<User>(userDto);
            await _repository.CreateAsync(user);
        }

        public async Task<bool> UpdateAsync(UserDto userDto)
        {
            var existing = await _repository.GetAsync(userDto.Id);
            if (existing == null)
                return false;

            var updated = _mapper.Map<User>(userDto);
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