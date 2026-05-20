using Licenses.Models;
using Microsoft.EntityFrameworkCore;

namespace Licenses.Repositories.TransactionLotOrderRepositories
{
    public class TransactionLotOrderRepository:ITransactionLotOrderRepository
    {
        readonly DbContext _Db;
        public TransactionLotOrderRepository(DbContext db)
        {
            _Db = db;
        }
        public IQueryable<TransactionLotOrder> GetAll(int page = 1, int pageSize = 10)
        {
            page = page <= 0 ? 1 : page;
            pageSize = pageSize <= 0 ? 10 : pageSize;
            return _Db.
                Set<TransactionLotOrder>().
                AsNoTracking().
                Where(c => c.IsDeleted == false).
                Skip((page - 1) * pageSize).
                Take(pageSize);
        }
        public async Task<TransactionLotOrder?> GetByIdAsync(int id)
        {
            return await _Db.
                Set<TransactionLotOrder>().
                AsTracking().
                SingleOrDefaultAsync(x => x.Id == id && x.IsDeleted == false);
        }
        public async Task<TransactionLotOrder> AddAsync(TransactionLotOrder transactionLotOrder)
        {

            await _Db.
                  Set<TransactionLotOrder>().
                  AddAsync(transactionLotOrder);
            return transactionLotOrder;


        }
        public TransactionLotOrder Update(TransactionLotOrder transactionLotOrder)
        {

            _Db.Set<TransactionLotOrder>().
                Update(transactionLotOrder);
            return transactionLotOrder;


        }
        public bool SoftDelete(TransactionLotOrder transactionLotOrder)
        {

            transactionLotOrder.IsDeleted = true;

            return true;

        }
        public async Task SaveChanges()
        {
            await _Db.SaveChangesAsync();
        }
    }
}
