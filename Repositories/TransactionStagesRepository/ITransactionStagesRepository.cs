using Licenses.Models;

namespace Licenses.Repositories.TransactionStagesRepository
{
    public interface ITransactionStagesRepository
    {
        public IQueryable<TransactionStages> GetAll(int page = 1, int pageSize = 10);
      
         
        public  Task<TransactionStages?> GetByIdAsync(int id);
      
        public Task<TransactionStages> AddAsync(TransactionStages transactionStages);


        public TransactionStages Update(TransactionStages transactionStages);


        public bool SoftDelete(TransactionStages transactionStages);


        public  Task SaveChanges();
        
    }
}
