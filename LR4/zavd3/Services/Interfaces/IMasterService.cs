using MyWebApi.Models;

namespace MyWebApi.Services.Interfaces
{
    public interface IMasterService
    {
        Task<IEnumerable<Master>> GetAllAsync();
        Task<Master?> GetByIdAsync(int id);
        Task AddAsync(Master master);
        Task UpdateAsync(Master master);
        Task DeleteAsync(int id);
    }
}