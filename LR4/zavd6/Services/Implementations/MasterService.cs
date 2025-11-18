using AutoMapper;
using MyWebApi.Entities;
using MyWebApi.Dtos;
using MyWebApi.Repositories.Interfaces;
using MyWebApi.Services.Interfaces;

namespace MyWebApi.Services.Implementations
{
    public class MasterService : IMasterService
    {
        private readonly IMasterRepository _masterRepository;
        public MasterService(IMasterRepository masterRepository)
        {
            _masterRepository = masterRepository;
        }
        public async Task CreateAsync(Master master) => await _masterRepository.CreateAsync(master);
        public async Task DeleteAsync(string id) => await _masterRepository.DeleteAsync(id);
        public async Task<List<Master>> GetAsync() => await _masterRepository.GetAsync();
        public async Task<Master> GetAsync(string id) => await _masterRepository.GetAsync(id);
        public async Task<List<Master>> GetByCategoryAsync(Category category) => await _masterRepository.GetByCategoryAsync(category);
        public async Task UpdateAsync(Master master) => await _masterRepository.UpdateAsync(master);
    }
}