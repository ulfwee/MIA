using MongoDB.Driver;
using MyWebApi.Entities;
using MyWebApi.Repositories.Interfaces;
using SortTest.Test;

namespace MyWebApi.Repositories.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoCollection<User> _collection;

        public UserRepository()
        {
            _collection = MobgoDBClient.Instance.GetCollection<User>("Users");
        }

        // Read
        public async Task<User> GetAsync(string id) => await _collection.Find(x => x.Id == id).FirstOrDefaultAsync();
        public async Task<User?> GetByEmailAsync(string email)
            => await _collection.Find(u => u.Email == email).FirstOrDefaultAsync();
        public async Task<List<User>> GetAsync() => await _collection.Find(x => true).ToListAsync();

        // Create, Update, Delete
        public async Task CreateAsync(User user) => await _collection.InsertOneAsync(user);
        public async Task DeleteAsync(string id) => await _collection.DeleteOneAsync(x => x.Id == id);
        public async Task UpdateAsync(User user) => await _collection.ReplaceOneAsync(x => x.Id == user.Id, user);
    }
}