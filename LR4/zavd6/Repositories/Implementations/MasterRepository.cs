using MongoDB.Driver;
using MyWebApi.Entities;
using MyWebApi.DTOs;
using MyWebApi.Repositories.Interfaces;
using SortTest.Test;

namespace MyWebApi.Repositories.Implementations
{
    public class MasterRepository : IMasterRepository
    {
        private IMongoCollection<Master> _collection;
        public MasterRepository()
        {
            _collection = MobgoDBClient.Instance.GetCollection<Master>("Masters");
        }
        // Read
        public async Task<Master> GetAsync(string id) => await _collection.Find(x => x.Id == id).FirstOrDefaultAsync();
        public async Task<List<Master>> GetByCategoryAsync(Category category)
            => await _collection.Find(m => m.Category == category).ToListAsync();
        public async Task<List<Master>> GetAsync() => await _collection.Find(x => true).ToListAsync();
        // Create, Update, Delete
        public async Task CreateAsync(Master master) => await _collection.InsertOneAsync(master);
        public async Task DeleteAsync(string id) => await _collection.DeleteOneAsync(x => x.Id == id);
        public async Task UpdateAsync(Master master) => await _collection.ReplaceOneAsync(x => x.Id == master.Id, master);
    }
}