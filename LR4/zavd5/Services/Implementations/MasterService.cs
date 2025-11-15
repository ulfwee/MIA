using AutoMapper;
using MyWebApi.Models;
using MyWebApi.Repositories.Interfaces;
using MyWebApi.Services.Interfaces;

namespace MyWebApi.Services.Implementations
{
    public class MasterService : IMasterService
    {
        private readonly IMasterRepository _repository;
        private readonly IMapper _mapper;


        public MasterService(IMasterRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<MasterDto>> GetAllAsync()
        {
            var masters = await _repository.GetAsync();
            return _mapper.Map<IEnumerable<MasterDto>>(masters);
        }

        public async Task<MasterDto?> GetByIdAsync(string id)
        {
            var master = await _repository.GetAsync(id);
            return _mapper.Map<MasterDto>(master);
        }

        public async Task CreateAsync(MasterDto masterDto)
        {
            var model = _mapper.Map<Master>(masterDto);
            await _repository.CreateAsync(model);
        }

        public async Task<bool> UpdateAsync(MasterDto masterDto)
        {
            var existing = await _repository.GetAsync(masterDto.Id);
            if (existing == null)
                return false;

            var model = _mapper.Map<Master>(masterDto);
            await _repository.UpdateAsync(model);
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