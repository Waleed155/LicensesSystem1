using Licenses.Models;
using Microsoft.EntityFrameworkCore;

namespace Licenses.Repositories.TransactionRepositories
{
    public class TransactionRepository:ITransactionRepository
    {
        readonly DbContext _Db;
        public TransactionRepository(DbContext db) 
        {
            _Db = db;
        
        }
        public IQueryable<Transaction> GetAll(int page = 1, int pageSize = 10)
        {
            page = page <= 0 ? 1 : page;
            pageSize = pageSize <= 0 ? 10 : pageSize;
            return _Db.
                Set<Transaction>().
                AsNoTracking().
                Where(c => c.IsDeleted == false).
                Skip((page - 1) * pageSize).
                Take(pageSize);
        }
        public async Task<Transaction?> GetByIdAsync(int id)
        {
            return await _Db.
                Set<Transaction>().
                AsTracking().
                SingleOrDefaultAsync(x => x.Id == id && x.IsDeleted == false);
        }
        public async Task<Transaction> AddAsync(Transaction transaction)
        {

            await _Db.
                  Set<Transaction>().
                  AddAsync(transaction);
            return transaction;


        }
        public Transaction Update(Transaction transaction)
        {

            _Db.Set<Transaction>().
                Update(transaction);
            return transaction;


        }
        public bool SoftDelete(Transaction transaction)
        {

            transaction.IsDeleted = true;

            return true;

        }
        public async Task SaveChanges()
        {
            await _Db.SaveChangesAsync();
        }
    }
}
