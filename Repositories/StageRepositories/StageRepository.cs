using Licenses.Models;
using Microsoft.EntityFrameworkCore;

namespace Licenses.Repositories.StageRepositories
{
    public class StageRepository:IStageRepository
    {
        readonly DbContext _Db;
        public StageRepository(DbContext db)
        {
            _Db = db;
        }
        public IQueryable<Stage> GetAll(int page = 1, int pageSize = 10)
        {
            page = page <= 0 ? 1 : page;
            pageSize = pageSize <= 0 ? 10 : pageSize;
            return _Db.
                 Set<Stage>().
                 AsNoTracking().
                Where(ex => ex.IsDeleted == false).
                Skip((page - 1) * pageSize).
                Take(pageSize);
        }
        public IQueryable<Stage> GetAllDeleted(int page, int pageSize)
        {
            page = page <= 0 ? 1 : page;
            pageSize = pageSize <= 0 ? 10 : pageSize;
            return _Db.
                Set<Stage>().
                AsNoTracking().
               OrderBy(s => s.Name).
                Where(s => s.IsDeleted == true).
                Skip((page - 1) * pageSize).
                Take(pageSize);
        }

        public async Task<Stage?> GetByIdAsync(int id)
        {
            return
             await _Db.
                Set<Stage>().
                AsTracking().
                SingleOrDefaultAsync(ex => ex.Id == id && !ex.IsDeleted);
        }
        public async Task<Stage?> GetByNameAsync(string name)
        {
            return await _Db.
                Set<Stage>().
                AsNoTracking().
                FirstOrDefaultAsync(s => s.Name == name);

        }
        public IQueryable<Stage?> SearchByName(string name, int page = 1, int pagesize = 10)
        {

            return _Db.
                Set<Stage>().
                AsNoTracking().
                OrderBy(s => s.Name).
                Where(s => s.Name.Contains(name) && !s.IsDeleted).
                Skip((page - 1) * pagesize).
                Take(pagesize);

        }
        public IQueryable<Stage?> SearchByNameDeleted(string name, int page = 1, int pagesize = 10)
        {

            return _Db.
                Set<Stage>().
                AsNoTracking().
                OrderBy(s => s.Name).
                Where(s => s.Name.Contains(name) && s.IsDeleted == true).
                Skip((page - 1) * pagesize).
                Take(pagesize);

        }

        public async Task<Stage> AddAsync(Stage stage)
        {
            await
                _Db.
                Set<Stage>().
                AddAsync(stage);
            return stage;
        }
        public Stage Update(Stage stage)
        {
            _Db.
                Update(stage);
            return stage;
        }
        public bool SoftDelete(Stage stage)
        {
            try
            {
                stage.IsDeleted = true;
                return true;
            }
            catch
            {
                return false;
            }
        }
        public async Task SaveChangesAsync()
        {
          await  _Db.SaveChangesAsync();
        }
        public bool Revive(Stage stage)
        {

            stage.IsDeleted = false;

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
    }
}
