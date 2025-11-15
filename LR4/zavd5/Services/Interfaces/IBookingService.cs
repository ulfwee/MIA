using MyWebApi.Models;

namespace MyWebApi.Services.Interfaces
{
    public interface IBookingService
    {
        Task<IEnumerable<BookingDto>> GetAllAsync();
        Task<BookingDto?> GetByIdAsync(string id);
        Task CreateAsync(BookingDto booking);
        Task<bool> UpdateAsync(BookingDto booking);
        Task<bool> DeleteAsync(string id);
    }
}