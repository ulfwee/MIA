

namespace Project.Repositories
{
    public interface IMasterRepository
    {
        Task<IEnumerable<Master>> GetAllAsync();
        Task<Master?> GetByIdAsync(int id);
        Task AddAsync(Master master);
        Task UpdateAsync(Master master);
        Task DeleteAsync(int id);
    }
}
