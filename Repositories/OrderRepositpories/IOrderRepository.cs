using Licenses.Models;

namespace Licenses.Repositories.OrderRepositpories
{
    public interface IOrderRepository
    {
        public IQueryable<Order> GetAll(int page, int pageSize);

        public  Task<Order?> GetByIdAsync(int id);
        public Task<Order?> GetByNameAsync(string name);

        public Task<Order> AddAsync(Order order);

        public Order Update(Order order);
        public IQueryable<Order?> SearchByNameAsync(string name, int page = 1, int pagesize = 10);
        public bool SoftDelete(Order order);


        public  Task SaveChangesAsync();
        
    }
}
