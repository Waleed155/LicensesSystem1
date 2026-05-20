using Licenses.Models;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Licenses.Repositories.ExcutivePositionRepositories
{
    public class ExcutivePositionRepository:IExcutivePositionRepository
    {

        DbContext _Db;
        public ExcutivePositionRepository(DbContext db)
        {
            _Db = db;
        }

        public IQueryable<ExcutivePosition> GetAll(int page = 1, int pageSize = 20)
        {
            page = page <= 0 ? 1 : page;
            pageSize = pageSize <= 0 ? 10 : pageSize;
            return _Db.
                 Set<ExcutivePosition>().
                 AsNoTracking().
                Where(ex=>ex.IsDeleted==false).
                Skip((page-1)*pageSize).
                Take(pageSize);
        }
        public async Task<ExcutivePosition?> GetById(int id )
        {
            return
             await   _Db.
                Set<ExcutivePosition>().
                AsTracking().
                SingleOrDefaultAsync(ex=>ex.Id == id&&!ex.IsDeleted);
        }
        public async Task<ExcutivePosition> AddAsync (ExcutivePosition excutivePosition)
        {
            await
                _Db.
                Set<ExcutivePosition>().
                AddAsync(excutivePosition);
            return excutivePosition;
        }
        public ExcutivePosition Update(ExcutivePosition excutivePosition) 
        {
            _Db.
                Update(excutivePosition);
            return excutivePosition;
        }
        public  bool SoftDelete(ExcutivePosition excutivePosition)
        {
            try
            {
                excutivePosition.IsDeleted = true;
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
