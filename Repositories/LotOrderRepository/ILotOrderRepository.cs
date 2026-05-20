using Licenses.Models;

namespace Licenses.Repositories.LotOrderRepository
{
    public interface ILotOrderRepository
    {
        public IQueryable<LotOrder> GetAll(int page = 1, int pageSize = 10);
        public  Task<LotOrder?> GetByIdAsync(int id);
        public  Task<LotOrder> AddAsync(LotOrder lotOrder);
        public LotOrder Update(LotOrder lotorder);
        public bool SoftDelete(LotOrder lotOrder);
        public Task SaveChanges();
       
    }
}
