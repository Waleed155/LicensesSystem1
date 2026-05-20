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
        public IQueryable<Stage> GetAll(int page = 1, int pageSize = 20)
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
        public async Task<Stage?> GetById(int id)
        {
            return
             await _Db.
                Set<Stage>().
                AsTracking().
                SingleOrDefaultAsync(ex => ex.Id == id && !ex.IsDeleted);
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

    }
}
