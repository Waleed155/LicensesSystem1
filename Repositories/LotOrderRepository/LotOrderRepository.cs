using Licenses.Models;
using Microsoft.EntityFrameworkCore;

namespace Licenses.Repositories.LotOrderRepository
{
    public class LotOrderRepository:ILotOrderRepository
    {
     readonly   DbContext _Db;   
    public LotOrderRepository(DbContext db)
     {
            _Db = db;
     }
        public IQueryable<LotOrder> GetAll(int page = 1, int pageSize = 10)
        {
            page = page <= 0 ? 1 : page;
            pageSize = pageSize <= 0 ? 10 : pageSize;
            return _Db.
                Set<LotOrder>().
                AsNoTracking().
                Where(c => c.IsDeleted == false).
                Skip((page - 1) * pageSize).
                Take(pageSize);
        }
        public async Task<LotOrder?> GetByIdAsync(int id)
        {
            return await _Db.
                Set<LotOrder>().
                AsTracking().
                SingleOrDefaultAsync(x => x.Id == id && x.IsDeleted == false);
        }
        public async Task<LotOrder> AddAsync(LotOrder lotOrder)
        {

            await _Db.
                  Set<LotOrder>().
                  AddAsync(lotOrder);
            return lotOrder;


        }
        public LotOrder Update(LotOrder lotorder)
        {

            _Db.Set<LotOrder>().
                Update(lotorder);
            return lotorder;


        }
        public bool SoftDelete(LotOrder lotOrder)
        {

            lotOrder.IsDeleted = true;

            return true;

        }
        public async Task SaveChanges()
        {
            await _Db.SaveChangesAsync();
        }
    }
}
