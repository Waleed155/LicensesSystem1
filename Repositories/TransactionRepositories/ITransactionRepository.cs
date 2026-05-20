using Licenses.Models;

namespace Licenses.Repositories.TransactionRepositories
{
    public interface ITransactionRepository
    {
        public IQueryable<Transaction> GetAll(int page = 1, int pageSize = 10);
        public  Task<Transaction?> GetByIdAsync(int id);
        public Task<Transaction> AddAsync(Transaction transaction);
        public Transaction Update(Transaction transaction);
        public bool SoftDelete(Transaction transaction);
        public Task SaveChanges();
        
    }
}
