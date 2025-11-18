using MyWebApi.Entities;
using MyWebApi.Dtos;
namespace MyWebApi.Services.Interfaces
{
    public interface IMasterService
    {
        Task CreateAsync(Master master);
        Task<List<Master>> GetAsync();
        Task<Master> GetAsync(string id);
        Task<List<Master>> GetByCategoryAsync(Category category);
        Task UpdateAsync(Master master);
        Task DeleteAsync(string id);
    }
}