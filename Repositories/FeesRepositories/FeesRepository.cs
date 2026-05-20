using Licenses.Models;
using Microsoft.EntityFrameworkCore;

namespace Licenses.Repositories.FeesRepositories
{
    public class FeesRepository:IFeesRepository
    {
         readonly DbContext _Db;
        public FeesRepository(DbContext db)
        {
            _Db = db;        
        }
        public IQueryable<Fees> GetAll(int page = 1, int pageSize = 20)
        {
            page = page <= 0 ? 1 : page;
            pageSize = pageSize <= 0 ? 10 : pageSize;
            return _Db.
                 Set<Fees>().
                 AsNoTracking().
                Where(ex => ex.IsDeleted == false).
                Skip((page - 1) * pageSize).
                Take(pageSize);
        }
        public async Task<Fees?> GetById(int id)
        {
            return
             await _Db.
                Set<Fees>().
                AsTracking().
                SingleOrDefaultAsync(ex => ex.Id == id && !ex.IsDeleted);
        }
        public async Task<Fees> AddAsync(Fees fees)
        {
            await
                _Db.
                Set<Fees>().
                AddAsync(fees);
            return fees;
        }
        public Fees Update(Fees fees)
        {
            _Db.
                Update(fees);
            return fees;
        }
        public bool SoftDelete(Fees fees)
        {
            try
            {
                fees.IsDeleted = true;
                return true;
            }
            catch
            {
                return false;
            }
        }
        public async Task SaveChangesAsync()
        {
            await _Db.SaveChangesAsync();
        }

    }
}
