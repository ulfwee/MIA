

namespace Project.Repositories
{

    public class MasterRepository : IMasterRepository
    {
        private readonly List<Master> _masters = new()
        {
            new() { Id = 1, Name = "Іван Петренко", Category = Category.Plumbing, Ranking = 4.8 },
            new() { Id = 2, Name = "Олег Сидоренко", Category = Category.Electrical, Ranking = 4.9 },
            new() { Id = 3, Name = "Микола Коваль", Category = Category.Assembly, Ranking = 4.5 }
        };
        private int _nextId = 4;

        public Task<IEnumerable<Master>> GetAllAsync() => Task.FromResult(_masters.AsEnumerable());

        public Task<Master?> GetByIdAsync(int id) =>
            Task.FromResult(_masters.FirstOrDefault(m => m.Id == id));

        public Task AddAsync(Master master)
        {
            master.Id = _nextId++;
            _masters.Add(master);
            return Task.CompletedTask;
        }

        public Task UpdateAsync(Master master)
        {
            var existing = _masters.FirstOrDefault(m => m.Id == master.Id);
            if (existing != null)
            {
                existing.Name = master.Name;
                existing.Category = master.Category;
                existing.Ranking = master.Ranking;
            }
            return Task.CompletedTask;
        }

        public Task DeleteAsync(int id)
        {
            var master = _masters.FirstOrDefault(m => m.Id == id);
            if (master != null) _masters.Remove(master);
            return Task.CompletedTask;
        }
    }

}
