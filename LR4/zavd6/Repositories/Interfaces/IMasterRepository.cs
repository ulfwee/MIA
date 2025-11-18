using MongoDB.Driver;
using MyWebApi.Entities;
using MyWebApi.DTOs;

namespace MyWebApi.Repositories.Interfaces
{
    public interface IMasterRepository
    {
        Task CreateAsync(Master master);
        Task<List<Master>> GetAsync();
        Task<Master> GetAsync(string id);
        Task<List<Master>> GetByCategoryAsync(Category category);
        Task UpdateAsync(Master master);
        Task DeleteAsync(string id);
    }
}