using MyWebApi.Models;
using MyWebApi.Repositories.Interfaces;
using MyWebApi.Services.Interfaces;

namespace MyWebApi.Services.Implementations
{
    public class MasterService : IMasterService
    {
        private readonly IMasterRepository _repository;

        public MasterService(IMasterRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Master>> GetAllAsync()
        {
            return await _repository.GetAsync();
        }

        public async Task<Master?> GetByIdAsync(string id)
        {
            return await _repository.GetAsync(id);
        }

        public async Task CreateAsync(Master master)
        {
            await _repository.CreateAsync(master);
        }

        public async Task<bool> UpdateAsync(Master master)
        {
            var existing = await _repository.GetAsync(master.Id);
            if (existing == null)
                return false;

            await _repository.UpdateAsync(master);
            return true;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var existing = await _repository.GetAsync(id);
            if (existing == null)
                return false;

            await _repository.DeleteAsync(id);
            return true;
        }
    }
}