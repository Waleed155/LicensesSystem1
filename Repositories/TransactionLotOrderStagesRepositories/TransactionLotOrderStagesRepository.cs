using Licenses.Models;
using Microsoft.EntityFrameworkCore;

namespace Licenses.Repositories.TransactionLotOrderStagesRepositories
{
    public class TransactionLotOrderStagesRepository:ITransactionLotOrderStagesRepository
    {
        readonly DbContext _Db;
        public TransactionLotOrderStagesRepository (DbContext db)
        {
            _Db = db;
        }
        public IQueryable<TransactionLotOrderStages> GetAll(int page = 1, int pageSize = 10)
        {
            page = page <= 0 ? 1 : page;
            pageSize = pageSize <= 0 ? 10 : pageSize;
            return _Db.
                Set<TransactionLotOrderStages>().
                AsNoTracking().
                Where(c => c.IsDeleted == false).
                Skip((page - 1) * pageSize).
                Take(pageSize);
        }
        public async Task<TransactionLotOrderStages?> GetByIdAsync(int id)
        {
            return await _Db.
                Set<TransactionLotOrderStages>().
                AsTracking().
                SingleOrDefaultAsync(x => x.Id == id && x.IsDeleted == false);
        }
        public async Task<TransactionLotOrderStages> AddAsync(TransactionLotOrderStages transactionLotOrderStages)
        {

            await _Db.
                  Set<TransactionLotOrderStages>().
                  AddAsync(transactionLotOrderStages);
            return transactionLotOrderStages;


        }
        public TransactionLotOrderStages Update(TransactionLotOrderStages transactionLotOrderStages)
        {

            _Db.Set<TransactionLotOrderStages>().
                Update(transactionLotOrderStages);
            return transactionLotOrderStages;


        }
        public bool SoftDelete(TransactionLotOrderStages transactionLotOrderStages)
        {

            transactionLotOrderStages.IsDeleted = true;

            return true;

        }
        public async Task SaveChanges()
        {
            await _Db.SaveChangesAsync();
        }
    }
}
