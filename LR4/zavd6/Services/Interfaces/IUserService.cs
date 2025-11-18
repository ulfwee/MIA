using MyWebApi.Entities;
using MyWebApi.DTOs;

namespace MyWebApi.Services.Interfaces
{
    public interface IUserService
    {
        Task CreateAsync(User user);
        Task<List<User>> GetAsync();
        Task<User?> GetByEmailAsync(string email);
        Task<User> GetAsync(string id);
        Task UpdateAsync(User user);
        Task DeleteAsync(string id);
    }
}