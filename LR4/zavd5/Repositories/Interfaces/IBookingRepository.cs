using MongoDB.Driver;

using MyWebApi.Models;

namespace MyWebApi.Repositories.Interfaces
{
    public interface IBookingRepository
    {
        Task CreateAsync(Booking booking);
        Task<List<Booking>> GetAsync();
        Task<Booking> GetAsync(string id);
        Task UpdateAsync(Booking booking);
        Task DeleteAsync(string id);
    }
}