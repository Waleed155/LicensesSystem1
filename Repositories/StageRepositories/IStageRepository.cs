using Licenses.Models;

namespace Licenses.Repositories.StageRepositories
{
    public interface IStageRepository
    {
        public IQueryable<Stage> GetAll(int page = 1, int pageSize = 20);
        public  Task<Stage?> GetById(int id);
        public  Task<Stage> AddAsync(Stage stage);
        public Stage Update(Stage stage);
        public bool SoftDelete(Stage stage);
        public  Task SaveChangesAsync();
    }
}
