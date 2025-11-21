using MyWebApi.Models;
using MyWebApi.Repositories.Interfaces;
using MyWebApi.Services.Interfaces;

namespace MyWebApi.Services.Implementations
{
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
    }
}