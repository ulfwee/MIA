using MyWebApi.Entities;
using MyWebApi.Dtos;
namespace MyWebApi.Services.Interfaces
{
    public interface IBookingService
    {
        Task CreateAsync(Booking booking);
        Task<List<Booking>> GetAsync();
        Task<Booking> GetAsync(string id);
        Task<List<Booking>> GetByUserIdAsync(string userId);
        Task<List<Booking>> GetByMasterIdAsync(string masterId);
        Task UpdateAsync(Booking booking);
        Task DeleteAsync(string id);
    }
}