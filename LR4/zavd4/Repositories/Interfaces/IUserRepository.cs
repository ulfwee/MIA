using MongoDB.Driver;
using MyWebApi.Models;

namespace MyWebApi.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task CreateAsync(User user);
        Task<List<User>> GetAsync();
        Task<User> GetAsync(string id);
        Task UpdateAsync(User user);
        Task DeleteAsync(string id);
    }
}