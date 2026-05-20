using Licenses.Models;

namespace Licenses.Repositories.ActivityTypeRepositories
{
    public interface IActivityTypeRepository
    {
        public IQueryable<ActivityType> GetAll(int page , int pageSize );
        public Task< ActivityType?> GetByIdAsync(int id);
        public Task< ActivityType> AddAsync(ActivityType activityType);
        public ActivityType Update(ActivityType activityType);
        public bool SoftDelete(ActivityType activityType);
        public Task SaveChanges();
       
    }
}

