using MongoDB.Driver;

using MyWebApi.Models;

namespace MyWebApi.Repositories.Interfaces
{
    public interface IMasterRepository
    {
        Task CreateAsync(Master master);
        Task<List<Master>> GetAsync();
        Task<Master> GetAsync(string id);
        Task UpdateAsync(Master master);
        Task DeleteAsync(string id);
    }
}