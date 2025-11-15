using AutoMapper;
using MyWebApi.Models;
using MyWebApi.Repositories.Interfaces;
using MyWebApi.Services.Interfaces;

namespace MyWebApi.Services.Implementations
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _repository;
        private readonly IMapper _mapper;


        public BookingService(IBookingRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BookingDto>> GetAllAsync()
        {
            var items = await _repository.GetAsync();
            return _mapper.Map<IEnumerable<BookingDto>>(items);
        }

        public async Task<BookingDto?> GetByIdAsync(string id)
        {
            var item = await _repository.GetAsync(id);
            return _mapper.Map<BookingDto>(item);
        }

        public async Task CreateAsync(BookingDto bookingDto)
        {
            var model = _mapper.Map<Booking>(bookingDto);
            await _repository.CreateAsync(model);
        }

        public async Task<bool> UpdateAsync(BookingDto bookingDto)
        {
            var existing = await _repository.GetAsync(bookingDto.Id);
            if (existing == null)
                return false;

            var model = _mapper.Map<Booking>(bookingDto);
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