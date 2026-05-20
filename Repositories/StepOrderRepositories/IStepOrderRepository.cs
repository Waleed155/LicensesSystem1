using Licenses.Models;

namespace Licenses.Repositories.StepOrderRepositories
{
    public interface IStepOrderRepository
    {
        public IQueryable<OrderSteps> GetAll(int page = 1, int pageSize = 10);
        public  Task<OrderSteps?> GetByIdAsync(int id);
        public  Task<OrderSteps> AddAsync(OrderSteps orderSteps);
        public OrderSteps Update(OrderSteps orderSteps);
        public bool SoftDelete(OrderSteps orderSteps);
        public  Task SaveChanges();
        
    }
}
