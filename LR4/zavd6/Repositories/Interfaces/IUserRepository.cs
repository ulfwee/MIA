using MongoDB.Driver;
using MyWebApi.Entities;
using MyWebApi.DTOs;

namespace MyWebApi.Repositories.Interfaces
{
    public interface IUserRepository
    {

        Task CreateAsync(User user);
        Task<List<User>> GetAsync();
        Task<User?> GetByEmailAsync(string email);
        Task<User> GetAsync(string id);
        Task UpdateAsync(User user);
        Task DeleteAsync(string id);
    }
}