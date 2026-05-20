using Licenses.Models;

namespace Licenses.Repositories.OrderRepositpories
{
    public interface IOrderRepository
    {
        public IQueryable<Order> GetAll(int page, int pageSize);

        public  Task<Order?> GetByIdAsync(int id);

        public Task<Order> AddAsync(Order order);

        public Order Update(Order order);

        public bool SoftDelete(Order order);

        public  Task SaveChanges();
        
    }
}
