using Licenses.Models;
using Microsoft.EntityFrameworkCore;

namespace Licenses.Repositories.TransactionStagesRepository
{
    public class TransactionStagesRepository:ITransactionStagesRepository
    {
        DbContext _Db;
        public TransactionStagesRepository(DbContext db) { 
        _Db = db;
        }
        public IQueryable<TransactionStages> GetAll(int page = 1, int pageSize = 10)
        {
            page = page <= 0 ? 1 : page;
            pageSize = pageSize <= 0 ? 10 : pageSize;
            return _Db.
                Set<TransactionStages>().
                AsNoTracking().
                Where(c => c.IsDeleted == false).
                Skip((page - 1) * pageSize).
                Take(pageSize);
        }
        public async Task<TransactionStages?> GetByIdAsync(int id)
        {
            return await _Db.
                Set<TransactionStages>().
                AsTracking().
                SingleOrDefaultAsync(x => x.Id == id && x.IsDeleted == false);
        }
        public async Task<TransactionStages> AddAsync(TransactionStages transactionStages)
        {

            await _Db.
                  Set<TransactionStages>().
                  AddAsync(transactionStages);
            return transactionStages;


        }
        public TransactionStages Update(TransactionStages transactionStages)
        {

            _Db.Set<TransactionStages>().
                Update(transactionStages);
            return transactionStages;


        }
        public bool SoftDelete(TransactionStages transactionStages)
        {

            transactionStages.IsDeleted = true;

            return true;

        }
        public async Task SaveChanges()
        {
            await _Db.SaveChangesAsync();
        }
    }
}
