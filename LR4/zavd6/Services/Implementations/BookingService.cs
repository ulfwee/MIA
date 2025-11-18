using AutoMapper;
using MyWebApi.Entities;
using MyWebApi.Dtos;
using MyWebApi.Repositories.Interfaces;
using MyWebApi.Services.Interfaces;

namespace MyWebApi.Services.Implementations
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepository;
        public BookingService(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }
        public async Task CreateAsync(Booking booking) => await _bookingRepository.CreateAsync(booking);
        public async Task DeleteAsync(string id) => await _bookingRepository.DeleteAsync(id);
        public async Task<List<Booking>> GetAsync() => await _bookingRepository.GetAsync();
        public async Task<Booking> GetAsync(string id) => await _bookingRepository.GetAsync(id);
        public async Task<List<Booking>> GetByUserIdAsync(string userId) => await _bookingRepository.GetByUserIdAsync(userId);
        public async Task<List<Booking>> GetByMasterIdAsync(string masterId) => await _bookingRepository.GetByMasterIdAsync(masterId);
        public async Task UpdateAsync(Booking booking) => await _bookingRepository.UpdateAsync(booking);
    }
}