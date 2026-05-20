
using Licenses.Models;

namespace Licenses.Repositories.LotRepositories
{
    public interface ILotRepository
    {
        public IQueryable<Lot> GetAll(int page, int pageSize );

        public  Task<Lot?> GetByIdAsync(int id);

        public Task<Lot> AddAsync(Lot lot);

        public Lot Update(Lot lot);

        public bool SoftDelete(Lot lot);

        public  Task SaveChanges();
       
    }
}