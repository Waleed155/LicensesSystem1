using Licenses.Models;
using Microsoft.EntityFrameworkCore;

namespace Licenses.Repositories.StepRepositories
{
    public class StepRepository:IStepRepository
    {
        readonly DbContext _Db;
        public StepRepository(DbContext db)
        {
            _Db = db;
        }
        public IQueryable<Step> GetAll(int page = 1, int pageSize = 20)
        {
            page = page <= 0 ? 1 : page;
            pageSize = pageSize <= 0 ? 10 : pageSize;
            return _Db.
                 Set<Step>().
                 AsNoTracking().
                Where(ex => ex.IsDeleted == false).
                Skip((page - 1) * pageSize).
                Take(pageSize);
        }
        public async Task<Step?> GetById(int id)
        {
            return
             await _Db.
                Set<Step>().
                AsTracking().
                SingleOrDefaultAsync(ex => ex.Id == id && !ex.IsDeleted);
        }
        public async Task<Step> AddAsync(Step step)
        {
            await
                _Db.
                Set<Step>().
                AddAsync(step);
            return step;
        }
        public Step Update(Step step)
        {
            _Db.
                Update(step);
            return step;
        }
        public bool SoftDelete(Step step)
        {
            try
            {
                step.IsDeleted = true;
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
