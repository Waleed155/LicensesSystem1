using Licenses.Models;

namespace Licenses.Repositories.ExcutivePositionRepositories
{
    public interface IExcutivePositionRepository
    {
        public IQueryable<ExcutivePosition> GetAll(int page , int pageSize );
      
        public Task<ExcutivePosition?> GetById(int id);
      
        public  Task<ExcutivePosition> AddAsync(ExcutivePosition excutivePosition);


        public ExcutivePosition Update(ExcutivePosition excutivePosition);


        public bool SoftDelete(ExcutivePosition excutivePosition);


        public  Task SaveChangesAsync();
    }
}
