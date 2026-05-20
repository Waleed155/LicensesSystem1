using Licenses.Models;

namespace Licenses.Repositories.LotOrderStepsRepositpories
{
    public interface ILotOrderStepsRepository
    {
        public IQueryable<LotOrderSteps> GetAll(int page = 1, int pageSize = 10);
      
        public  Task<LotOrderSteps?> GetByIdAsync(int id);
       
        public  Task<LotOrderSteps> AddAsync(LotOrderSteps lotOrderSteps);


        public LotOrderSteps Update(LotOrderSteps lotOrderSteps);


        public bool SoftDelete(LotOrderSteps lotOrderSteps);


        public  Task SaveChanges();
        
    }
}
