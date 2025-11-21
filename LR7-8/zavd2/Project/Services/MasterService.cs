
using Project.Repositories;

namespace Project.Services
{
    public interface IMasterService { 
        Task<IEnumerable<Master>> GetAllAsync(); 
        Task<Master?> GetByIdAsync(int id); 
        Task AddAsync(Master master); 
        Task UpdateAsync(Master master); 
        Task DeleteAsync(int id); 
    }
    public class MasterService : IMasterService
    {
        private readonly IMasterRepository _repository;

        public MasterService(IMasterRepository repository) => _repository = repository;

        public Task<IEnumerable<Master>> GetAllAsync() => _repository.GetAllAsync();

        public async Task<Master?> GetByIdAsync(int id)
        {
            var master = await _repository.GetByIdAsync(id);
            if (master == null)
                throw new KeyNotFoundException($"Master with id {id} not found");
            return master;
        }

        public async Task AddAsync(Master master)
        {
            if (string.IsNullOrWhiteSpace(master.Name))
                throw new ArgumentException("Name is required");
            if (master.Ranking < 0 || master.Ranking > 5)
                throw new ArgumentException("Ranking must be between 0 and 5");

            await _repository.AddAsync(master);
        }

        public Task UpdateAsync(Master master) => _repository.UpdateAsync(master);
        public Task DeleteAsync(int id) => _repository.DeleteAsync(id);

        // ---------------- Нові методи ----------------

        public async Task<IEnumerable<Master>> GetTopRankedMastersAsync()
        {
            var all = await _repository.GetAllAsync();
            var maxRanking = all.Max(m => m.Ranking);
            return all.Where(m => m.Ranking == maxRanking);
        }

        public async Task<double> GetAverageRankingAsync()
        {
            var all = await _repository.GetAllAsync();
            if (!all.Any())
                throw new InvalidOperationException("No masters available");
            return all.Average(m => m.Ranking);
        }

        public async Task<int> CountLowRankedMastersAsync(double threshold)
        {
            if (threshold < 0 || threshold > 5)
                throw new ArgumentOutOfRangeException(nameof(threshold), "Threshold must be between 0 and 5");

            var all = await _repository.GetAllAsync();
            return all.Count(m => m.Ranking < threshold);
        }
    }
}