using Licenses.Models;

namespace Licenses.Repositories.StepRepositories
{
    public interface IStepRepository
    {
        public IQueryable<Step> GetAll(int page , int pageSize );
        public IQueryable<Step> GetAllDeleted(int page, int pageSize);
        public Task<Step?> GetByIdAsync(int id);
        public Task<Step?> GetByNameAsync(string name);
        public IQueryable<Step?> SearchByName(string name, int page , int pagesize );
        public IQueryable<Step?> SearchByNameDeleted(string name, int page , int pagesize );
        public Task<Step> AddAsync(Step step);
        public Step Update(Step step);
        public bool SoftDelete(Step step);
        public bool Revive(Step step);
        public Task<int> CountAsync();
        public Task<int> CountDeletedAsync();
        public Task<int> CountSearchAsync(string search);
        public Task<int> CountSearchDeletedAsync(string search);
        public Task SaveChangesAsync();
        

    }
}
