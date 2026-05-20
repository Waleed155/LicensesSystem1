using Licenses.Models;

namespace Licenses.Repositories.TransactionLotOrderStagesRepositories
{
    public interface ITransactionLotOrderStagesRepository
    {
        public IQueryable<TransactionLotOrderStages> GetAll(int page = 1, int pageSize = 10);
      
        public  Task<TransactionLotOrderStages?> GetByIdAsync(int id);
      
        public Task<TransactionLotOrderStages> AddAsync(TransactionLotOrderStages transactionLotOrderStages);


        public TransactionLotOrderStages Update(TransactionLotOrderStages transactionLotOrderStages);


        public bool SoftDelete(TransactionLotOrderStages transactionLotOrderStages);


        public Task SaveChanges();
       
    }
}
