using Licenses.Models;
using Microsoft.EntityFrameworkCore;

namespace Licenses.Repositories.OrderRepositpories
{
    public class OrderRepository:IOrderRepository
    {
        readonly DbContext _Db;
        public OrderRepository(DbContext db)
        {
        _Db=db;
        }
        public IQueryable<Order> GetAll(int page = 1, int pageSize = 10)
        {
            page = page <= 0 ? 1 : page;
            pageSize = pageSize <= 0 ? 10 : pageSize;
            return _Db.
                Set<Order>().
                AsNoTracking().
                Where(c => c.IsDeleted == false).
                Skip((page - 1) * pageSize).
                Take(pageSize);
        }
        public async Task<Order?> GetByIdAsync(int id)
        {
            return await _Db.
                Set<Order>().
                AsTracking().
                SingleOrDefaultAsync(x => x.Id == id && x.IsDeleted == false);
        }
        public async Task<Order> AddAsync(Order order)
        {

            await _Db.
                  Set<Order>().
                  AddAsync(order);
            return order;


        }
        public Order Update(Order order)
        {

            _Db.Set<Order>().
                Update(order);
            return order;


        }
        public bool SoftDelete(Order order )
        {

            order.IsDeleted = true;

            return true;

        }
        public async Task SaveChanges()
        {
            await _Db.SaveChangesAsync();
        }
    }
}
