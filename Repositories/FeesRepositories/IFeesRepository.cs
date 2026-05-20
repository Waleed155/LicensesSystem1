using Licenses.Models;

namespace Licenses.Repositories.FeesRepositories
{
    public interface IFeesRepository
    {
        public IQueryable<Fees> GetAll(int page, int pageSize);
        public  Task<Fees?> GetById(int id);
        public Task<Fees> AddAsync(Fees fees);
        public Fees Update(Fees fees);
        public bool SoftDelete(Fees fees);
        public Task SaveChangesAsync();
        
    }
}
