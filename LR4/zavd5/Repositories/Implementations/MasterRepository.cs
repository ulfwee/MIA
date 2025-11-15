using MongoDB.Driver;
using MyWebApi.Models;
using MyWebApi.Repositories.Interfaces;
using SortTest.Test;

namespace MyWebApi.Repositories.Implementations
{
    public class MasterRepository : IMasterRepository
    {

        readonly private IMongoCollection<Master> _collection;

        public MasterRepository()
        {
            _collection = MobgoDBClient.Instance.GetCollection<Master>("Masters");
        }

        public async Task CreateAsync(Master master) => await _collection.InsertOneAsync(master);
        public async Task DeleteAsync(string id) => await _collection.DeleteOneAsync(x => x.Id == id);
        public async Task<List<Master>> GetAsync() => await _collection.Find(x => true).ToListAsync();
        public async Task<Master> GetAsync(string id) => await _collection.Find(x => x.Id == id).FirstOrDefaultAsync();
        public async Task UpdateAsync(Master master) => await _collection.ReplaceOneAsync(x => x.Id == master.Id, master);
    }
}