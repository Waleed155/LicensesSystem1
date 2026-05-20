using Licenses.Models;
using Microsoft.EntityFrameworkCore;

namespace Licenses.Repositories.StepOrderRepositories
{
    public class StepOrderRepository:IStepOrderRepository
    {
        readonly DbContext _Db;
        public StepOrderRepository(DbContext db) { 
        _Db = db;
        }
        public IQueryable<OrderSteps> GetAll(int page = 1, int pageSize = 10)
        {
            page = page <= 0 ? 1 : page;
            pageSize = pageSize <= 0 ? 10 : pageSize;
            return _Db.
                Set<OrderSteps>().
                AsNoTracking().
                Where(c => c.IsDeleted == false).
                Skip((page - 1) * pageSize).
                Take(pageSize);
        }
        public async Task<OrderSteps?> GetByIdAsync(int id)
        {
            return await _Db.
                Set<OrderSteps>().
                AsTracking().
                SingleOrDefaultAsync(x => x.Id == id && x.IsDeleted == false);
        }
        public async Task<OrderSteps> AddAsync(OrderSteps orderSteps)
        {

            await _Db.
                  Set<OrderSteps>().
                  AddAsync(orderSteps);
            return orderSteps;


        }
        public OrderSteps Update(OrderSteps orderSteps)
        {

            _Db.Set<OrderSteps>().
                Update(orderSteps);
            return orderSteps;


        }
        public bool SoftDelete(OrderSteps orderSteps)
        {

            orderSteps.IsDeleted = true;

            return true;

        }
        public async Task SaveChanges()
        {
            await _Db.SaveChangesAsync();
        }
    }
}
