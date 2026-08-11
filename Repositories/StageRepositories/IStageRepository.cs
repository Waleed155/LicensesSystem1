using Licenses.Models;

namespace Licenses.Repositories.StageRepositories
{
    public interface IStageRepository
    {
        public IQueryable<Stage> GetAll(int page = 1, int pageSize = 20);
        public IQueryable<Stage> GetAllDeleted(int page, int pageSize);
        public Task<Stage?> GetByIdAsync(int id);
        public  Task<Stage?> GetByNameAsync(string name);
        public IQueryable<Stage?> SearchByName(string name, int page = 1, int pagesize = 10);
        public IQueryable<Stage?> SearchByNameDeleted(string name, int page = 1, int pagesize = 10);

        public Task<Stage> AddAsync(Stage stage);
        public Stage Update(Stage stage);
        public bool SoftDelete(Stage stage);
        public bool Revive(Stage stage);
        public Task<int> CountAsync();
        public Task<int> CountDeletedAsync();
        public Task<int> CountSearchAsync(string search);
        public  Task<int> CountSearchDeletedAsync(string search);
        public  Task SaveChangesAsync();
    }
}
