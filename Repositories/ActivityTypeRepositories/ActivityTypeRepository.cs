using Licenses.Models;
using Microsoft.EntityFrameworkCore;

namespace Licenses.Repositories.ActivityTypeRepositories
{
    public class ActivityTypeRepository:IActivityTypeRepository
    {
        DbContext _Db;
        public ActivityTypeRepository(DbContext db) { 
        _Db = db;
        }
        public   IQueryable<ActivityType> GetAll(int page =1 , int pageSize = 10)
        {
            page = page <= 0 ? 1 : page;
            pageSize = pageSize <= 0 ? 10 : pageSize;
            return  _Db.
                Set<ActivityType>().
                AsNoTracking().
                Where(c => c.IsDeleted == false).
                Skip((page-1)*pageSize).
                Take(pageSize );
        }
        public async Task< ActivityType?> GetByIdAsync(int id)
        {
            return await _Db.
                Set<ActivityType>().
                AsTracking().
                SingleOrDefaultAsync(x => x.Id == id && x.IsDeleted == false);
        }
        public async Task< ActivityType> AddAsync(ActivityType activityType)
        {
            
               await _Db.
                Set<ActivityType>().
                AddAsync(activityType);
                return activityType;
           

        }
        public ActivityType Update(ActivityType activityType)
        {

              _Db.
                Set<ActivityType>()
                .Update(activityType);
            return activityType;


        }
        public bool SoftDelete(ActivityType activityType)
        {
            try
            {

                activityType.IsDeleted = true;

                return true;
            }catch
            {
                return false;
            }

        }
        public async Task SaveChanges()
        {
            await _Db.SaveChangesAsync();
        }
    }
}
