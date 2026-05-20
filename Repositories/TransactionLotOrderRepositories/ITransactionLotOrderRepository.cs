using Licenses.Models;

namespace Licenses.Repositories.TransactionLotOrderRepositories
{
    public interface ITransactionLotOrderRepository
    {
        public IQueryable<TransactionLotOrder> GetAll(int page = 1, int pageSize = 10);
        public  Task<TransactionLotOrder?> GetByIdAsync(int id);
        public Task<TransactionLotOrder> AddAsync(TransactionLotOrder transactionLotOrder);
        public TransactionLotOrder Update(TransactionLotOrder transactionLotOrder);
        public bool SoftDelete(TransactionLotOrder transactionLotOrder);
        public  Task SaveChanges();
      
    }
}
