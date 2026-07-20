using Licenses.Models;

namespace Licenses.Repositories.ExcutivePositionRepositories
{
    public interface IExcutivePositionRepository
    {
        public IQueryable<ExcutivePosition> GetAll();
      
        public Task<ExcutivePosition?> GetByIdAsync(int id);
        public  Task<ExcutivePosition?> GetByNameAsync(string name);

        public Task<ExcutivePosition> AddAsync(ExcutivePosition excutivePosition);


        public ExcutivePosition Update(ExcutivePosition excutivePosition);


        public bool SoftDelete(ExcutivePosition excutivePosition);


        public  Task SaveChangesAsync();
    }
}
