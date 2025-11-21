using MyWebApi.Models;
using MyWebApi.Repositories.Interfaces;

namespace MyWebApi.Repositories.Implementations
{
    public class BookingRepository : IBookingRepository
    {
        private readonly List<Booking> _bookings = new()
        {
            new() { Id = 1, MasterId = 1, Date = "2025-11-15", ServiceDetails = "Ремонт крану", Status = Status.Pending },
            new() { Id = 2, MasterId = 2, Date = "2025-11-16", ServiceDetails = "Заміна проводки", Status = Status.Confirmed }
        };
        private int _nextId = 3;

        public Task<IEnumerable<Booking>> GetAllAsync() => Task.FromResult(_bookings.AsEnumerable());

        public Task<Booking?> GetByIdAsync(int id) =>
            Task.FromResult(_bookings.FirstOrDefault(b => b.Id == id));

        public Task AddAsync(Booking booking)
        {
            booking.Id = _nextId++;
            _bookings.Add(booking);
            return Task.CompletedTask;
        }

        public Task UpdateAsync(Booking booking)
        {
            var existing = _bookings.FirstOrDefault(b => b.Id == booking.Id);
            if (existing != null)
            {
                existing.MasterId = booking.MasterId;
                existing.Date = booking.Date;
                existing.ServiceDetails = booking.ServiceDetails;
                existing.Status = booking.Status;
            }
            return Task.CompletedTask;
        }

        public Task DeleteAsync(int id)
        {
            var booking = _bookings.FirstOrDefault(b => b.Id == id);
            if (booking != null) _bookings.Remove(booking);
            return Task.CompletedTask;
        }
    }
}