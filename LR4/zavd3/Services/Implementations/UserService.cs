using MyWebApi.Models;
using MyWebApi.Repositories.Interfaces;
using MyWebApi.Services.Interfaces;

namespace MyWebApi.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository) => _repository = repository;

        public Task<IEnumerable<User>> GetAllAsync() => _repository.GetAllAsync();

        public async Task<User?> GetByIdAsync(int id)
        {
            var user = await _repository.GetByIdAsync(id);
            if (user == null)
                throw new KeyNotFoundException($"User with id {id} not found");
            return user;
        }

        public async Task AddAsync(User user)
        {
               if (string.IsNullOrWhiteSpace(user.Name))
                throw new ArgumentException("Name is required");
            if (user.Age < 18)
                throw new ArgumentException("Age must be at least 18");
            if (string.IsNullOrWhiteSpace(user.Email) || !user.Email.Contains("@"))
                throw new ArgumentException("Valid Email is required");

            await _repository.AddAsync(user);
        }

        public Task UpdateAsync(User user) => _repository.UpdateAsync(user);
        public Task DeleteAsync(int id) => _repository.DeleteAsync(id);
    }
}