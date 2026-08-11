using Licenses.Models;

namespace Licenses.Repositories.OrderRepositpories
{
    public interface IOrderRepository
    {
        public IQueryable<Order> GetAll(int page, int pageSize);
        public IQueryable<Order> GetAllDeleted(int page, int pageSize);

        public Task<Order?> GetByIdAsync(int id);
        public Task<Order?> GetByNameAsync(string name);

        public Task<Order> AddAsync(Order order);

        public Order Update(Order order);
        public IQueryable<Order?> SearchByName(string name, int page = 1, int pagesize = 10);
        public IQueryable<Order?> SearchByNameDeleted(string name, int page = 1, int pagesize = 15);

        public bool SoftDelete(Order order);
        public bool Revive(Order order);

        public Task<int> CountAsync();
        public  Task<int> CountDeletedAsync();


        public Task<int> CountSearchAsync(string search);
        public Task<int> CountSearchDeletedAsync(string search);


        public Task SaveChangesAsync();
        
    }
}
