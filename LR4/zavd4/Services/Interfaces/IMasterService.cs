using MyWebApi.Models;

namespace MyWebApi.Services.Interfaces
{
    public interface IMasterService
    {
        Task<IEnumerable<Master>> GetAllAsync();
        Task<Master?> GetByIdAsync(string id);
        Task CreateAsync(Master master);
        Task<bool> UpdateAsync(Master master);
        Task<bool> DeleteAsync(string id);
    }
}