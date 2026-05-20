using Licenses.Models;
using Microsoft.EntityFrameworkCore;

namespace Licenses.Repositories.LotRepositories
{
    public class LotRepository:ILotRepository
    {
        readonly DbContext _Db;
        public LotRepository(DbContext db)
        {
        _Db = db;
        }
        public IQueryable<Lot> GetAll(int page = 1, int pageSize = 10)
        {
            page = page <= 0 ? 1 : page;
            pageSize = pageSize <= 0 ? 10 : pageSize;
            return _Db.
                Set<Lot>().
                AsNoTracking().
                Where(c => c.IsDeleted == false).
                Skip((page - 1) * pageSize).
                Take(pageSize);
        }
        public async Task<Lot?> GetByIdAsync(int id)
        {
            return await _Db.
                Set<Lot>().
                AsTracking().
                SingleOrDefaultAsync(x => x.Id == id && x.IsDeleted == false);
        }
        public async Task<Lot> AddAsync(Lot lot)
        {

            await _Db.
                  Set<Lot>().
                  AddAsync(lot);
            return lot;


        }
        public Lot Update(Lot lot)
        {

            _Db.Set<Lot>().
                Update(lot);
            return lot;


        }
        public bool SoftDelete(Lot lot)
        {

            lot.IsDeleted = true;

            return true;

        }
        public async Task SaveChanges()
        {
            await _Db.SaveChangesAsync();
        }
    }
}
