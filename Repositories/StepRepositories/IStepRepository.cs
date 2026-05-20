using Licenses.Models;

namespace Licenses.Repositories.StepRepositories
{
    public interface IStepRepository
    {
        public IQueryable<Step> GetAll(int page, int pageSize);

        public Task<Step?> GetById(int id);

        public Task<Step> AddAsync(Step step);

        public Step Update(Step step);

        public bool SoftDelete(Step step);

        public  Task SaveChangesAsync();
    }
}
