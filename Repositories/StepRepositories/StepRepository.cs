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
        public IQueryable<Step> GetAll(int page = 1, int pageSize = 15)
        {
            page = page <= 0 ? 1 : page;
            pageSize = pageSize <= 0 ? 15 : pageSize;
            return _Db.
                Set<Step>().
                AsNoTracking().
               OrderBy(c => c.Name).
                Where(c => c.IsDeleted == false).
                Skip((page - 1) * pageSize).
                Take(pageSize);
        }
        public IQueryable<Step> GetAllDeleted(int page, int pageSize)
        {
            page = page <= 0 ? 1 : page;
            pageSize = pageSize <= 0 ? 15 : pageSize;
            return _Db.
                Set<Step>().
                AsNoTracking().
               OrderBy(c => c.Name).
                Where(c => c.IsDeleted == true).
                Skip((page - 1) * pageSize).
                Take(pageSize);
        }
        public async Task<Step?> GetByIdAsync(int id)
        {
            return await _Db.
                Set<Step>().
                AsTracking().
                SingleOrDefaultAsync(x => x.Id == id);
        }
        public async Task<Step?> GetByNameAsync(string name)
        {
            return await _Db.
                Set<Step>().
                AsNoTracking().
                FirstOrDefaultAsync(s => s.Name == name);

        }
        public IQueryable<Step?> SearchByName(string name, int page = 1, int pagesize = 15)
        {

            return _Db.
                Set<Step>().
                AsNoTracking().
                OrderBy(s => s.Name).
                Where(s=>s.Name.Contains(name)&&!s.IsDeleted).
                Skip((page - 1) * pagesize).
                Take(pagesize);

        }
        public IQueryable<Step?> SearchByNameDeleted(string name, int page = 1, int pagesize = 15)
        {

            return _Db.
                Set<Step>().
                AsNoTracking().
                OrderBy(s=>s.Name).
                Where(s=>s.Name.Contains(name)   && s.IsDeleted == true).
                Skip((page - 1) * pagesize).
                Take(pagesize);

        }
        public async Task<Step> AddAsync(Step step)
        {

            await _Db.
                  Set<Step>().
                  AddAsync(step);
            return step;


        }
        public Step Update(Step step)
        {

            _Db.Set<Step>().
                Update(step);
            return step;


        }
        public bool SoftDelete(Step step)
        {

            step.IsDeleted = true;

            return true;

        }
        public bool Revive(Step step)
        {

            step.IsDeleted = false;

            return true;

        }
        public async Task<int> CountAsync()
        {
            return await _Db.
                Set<Step>()
                .CountAsync(s => !s.IsDeleted);
        }
        public async Task<int> CountDeletedAsync()
        {
            return await _Db.
                Set<Step>()
                .CountAsync(s => s.IsDeleted);
        }
        public async Task<int> CountSearchAsync(string search)
        {
            return await _Db.
                Set<Step>().
                AsNoTracking().
                Where(s => s.Name.Contains(search)).
                CountAsync(s => !s.IsDeleted);
        }
        public async Task<int> CountSearchDeletedAsync(string search)
        {
            return await _Db.
                Set<Step>().
                AsNoTracking().
                Where(s => s.Name.Contains(search) && s.IsDeleted == true).
                CountAsync();
        }
        public async Task SaveChangesAsync()
        {
            await _Db.SaveChangesAsync();
        }
    }
}
