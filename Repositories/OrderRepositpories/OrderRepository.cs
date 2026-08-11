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
        public IQueryable<Order> GetAll(int page = 1, int pageSize = 15)
        {
            page = page <= 0 ? 1 : page;
            pageSize = pageSize <= 0 ? 15 : pageSize;
            return _Db.
                Set<Order>().
                AsNoTracking().
               OrderBy(c => c.Name ).
                Where(c => c.IsDeleted == false).
                Skip((page - 1) * pageSize).
                Take(pageSize);
        }
        public IQueryable<Order>GetAllDeleted(int page,int pageSize)
        {
            page = page <= 0 ? 1 : page;
            pageSize = pageSize <= 0 ? 15 : pageSize;
            return _Db.
                Set<Order>().
                AsNoTracking().
               OrderBy(c => c.Name ).
                Where(c => c.IsDeleted == true).
                Skip((page - 1) * pageSize).
                Take(pageSize);
        }
        public async Task<Order?> GetByIdAsync(int id)
        {
            return await _Db.
                Set<Order>().
                AsTracking().
                SingleOrDefaultAsync(x => x.Id == id );
        }
        public async Task<Order?> GetByNameAsync(string name)
        {
            return await _Db.
                Set<Order>().
                AsNoTracking().
                FirstOrDefaultAsync(o => o.Name == name);
                
        }
        public  IQueryable<Order?> SearchByName(string name,int page=1,int pagesize=15)
        {

            return  _Db.
                Set<Order>().
                AsNoTracking().
                OrderBy(o => o.Name .Contains( name) && !o.IsDeleted).
                Skip((page-1)*pagesize).
                Take(pagesize);

        }
        public IQueryable<Order?> SearchByNameDeleted(string name, int page = 1, int pagesize = 15)
        {

            return _Db.
                Set<Order>().
                AsNoTracking().
                Where(o => o.Name.Contains(name) && o.IsDeleted==true).
                Skip((page - 1) * pagesize).
                Take(pagesize);

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
        public bool Revive(Order order)
        {

            order.IsDeleted = false;

            return true;

        }
        public async Task<int> CountAsync()
        {
            return await _Db.
                Set<Order>()
                .CountAsync(c => !c.IsDeleted);
        }
        public async Task<int> CountDeletedAsync()
        {
            return await _Db.
                Set<Order>()
                .CountAsync(c => c.IsDeleted);
        }
        public async Task<int> CountSearchAsync(string search)
        {
            return await _Db.
                Set<Order>().
                AsNoTracking().
                Where(o=> o.Name.Contains(search)).
                CountAsync(o=>!o.IsDeleted);
        }
        public async Task<int> CountSearchDeletedAsync(string search)
        {
            return await _Db.
                Set<Order>().
                AsNoTracking().
                Where(o => o.Name.Contains(search)&& o.IsDeleted==true).
                CountAsync();
        }
        public async Task SaveChangesAsync()
        {
            await _Db.SaveChangesAsync();
        }
    }
}
