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

        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _repository.GetAsync();
        }

        public async Task<User?> GetByIdAsync(string id)
        {
            return await _repository.GetAsync(id);
        }

        public async Task CreateAsync(User user)
        {
            await _repository.CreateAsync(user);
        }

        public async Task<bool> UpdateAsync(User user)
        {
            var existing = await _repository.GetAsync(user.Id);
            if (existing == null)
                return false; 

            await _repository.UpdateAsync(user);
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