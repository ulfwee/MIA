using MyWebApi.Models;
using MyWebApi.Repositories.Interfaces;

namespace MyWebApi.Repositories.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly List<User> _users = new()
        {
            new() { Id = 1, Name = "Олена Ковальчук", Age = 28, Email = "olena@example.com" },
            new() { Id = 2, Name = "Сергій Іванов", Age = 35, Email = "sergiy@example.com" }
        };
        private int _nextId = 3;

        public Task<IEnumerable<User>> GetAllAsync() => Task.FromResult(_users.AsEnumerable());

        public Task<User?> GetByIdAsync(int id) =>
            Task.FromResult(_users.FirstOrDefault(u => u.Id == id));

        public Task AddAsync(User user)
        {
            user.Id = _nextId++;
            _users.Add(user);
            return Task.CompletedTask;
        }

        public Task UpdateAsync(User user)
        {
            var existing = _users.FirstOrDefault(u => u.Id == user.Id);
            if (existing != null)
            {
                existing.Name = user.Name;
                existing.Age = user.Age;
                existing.Email = user.Email;
            }
            return Task.CompletedTask;
        }

        public Task DeleteAsync(int id)
        {
            var user = _users.FirstOrDefault(u => u.Id == id);
            if (user != null) _users.Remove(user);
            return Task.CompletedTask;
        }
    }
}