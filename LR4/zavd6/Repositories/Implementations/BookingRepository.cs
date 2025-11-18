using MongoDB.Driver;
using MyWebApi.Entities;
using MyWebApi.DTOs;
using MyWebApi.Repositories.Interfaces;
using SortTest.Test;

namespace MyWebApi.Repositories.Implementations
{
    public class BookingRepository : IBookingRepository
    {
        private IMongoCollection<Booking> _collection;
        public BookingRepository()
        {
            _collection = MobgoDBClient.Instance.GetCollection<Booking>("Bookings");
        }
        // Read
        public async Task<Booking> GetAsync(string id) => await _collection.Find(x => x.Id == id).FirstOrDefaultAsync();
        public async Task<List<Booking>> GetByUserIdAsync(string userId)
            => await _collection.Find(b => b.UserId == userId).ToListAsync();
        public async Task<List<Booking>> GetByMasterIdAsync(string masterId)
            => await _collection.Find(b => b.MasterId == masterId).ToListAsync();
        public async Task<List<Booking>> GetAsync() => await _collection.Find(x => true).ToListAsync();
        // Create, Update, Delete
        public async Task CreateAsync(Booking booking) => await _collection.InsertOneAsync(booking);
        public async Task DeleteAsync(string id) => await _collection.DeleteOneAsync(x => x.Id == id);
        public async Task UpdateAsync(Booking booking) => await _collection.ReplaceOneAsync(x => x.Id == booking.Id, booking);
    }
}