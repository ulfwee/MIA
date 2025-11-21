using MyWebApi.Models;
using MyWebApi.Repositories.Interfaces;
using MyWebApi.Services.Interfaces;

namespace MyWebApi.Services.Implementations
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _repository;

        public BookingService(IBookingRepository repository) => _repository = repository;

        public Task<IEnumerable<Booking>> GetAllAsync() => _repository.GetAllAsync();

        public async Task<Booking?> GetByIdAsync(int id)
        {
            var booking = await _repository.GetByIdAsync(id);
            if (booking == null)
                throw new KeyNotFoundException($"Booking with id {id} not found");
            return booking;
        }

        public async Task AddAsync(Booking booking)
        {
            if (string.IsNullOrWhiteSpace(booking.Date))
                throw new ArgumentException("Date is required");
            if (string.IsNullOrWhiteSpace(booking.ServiceDetails))
                throw new ArgumentException("ServiceDetails is required");

            await _repository.AddAsync(booking);
        }

        public Task UpdateAsync(Booking booking) => _repository.UpdateAsync(booking);
        public Task DeleteAsync(int id) => _repository.DeleteAsync(id);
    }
}