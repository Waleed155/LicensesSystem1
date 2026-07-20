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
        public   IQueryable<ActivityType> GetAll()
        { 
            return  _Db.
                Set<ActivityType>().
                AsNoTracking().
                Where(c => !c.IsDeleted);
        }
        public async Task< ActivityType?> GetByIdAsync(int id)
        {
            return await _Db.
                Set<ActivityType>().
                AsTracking().
                SingleOrDefaultAsync(x => x.Id == id && x.IsDeleted == false);
        }
        public Task<ActivityType?> GetByNameAsync(string name) 
        {
            return _Db.
                Set<ActivityType>().
                AsNoTracking().
                FirstOrDefaultAsync(aT => aT.Name==name);
        
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
        public async Task SaveChangesAsync()
        {
            await _Db.SaveChangesAsync();
        }
    }
}
