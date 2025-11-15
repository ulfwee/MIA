using MyWebApi.Models;

namespace MyWebApi.Services.Interfaces
{
    public interface IMasterService
    {
        Task<IEnumerable<MasterDto>> GetAllAsync();
        Task<MasterDto?> GetByIdAsync(string id);
        Task CreateAsync(MasterDto master);
        Task<bool> UpdateAsync(MasterDto master);
        Task<bool> DeleteAsync(string id);
    }
}