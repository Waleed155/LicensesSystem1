using Licenses.Models;
using Microsoft.EntityFrameworkCore;

namespace Licenses.Repositories.LotOrderStepsRepositpories
{
    public class LotOrderStepsRepository:ILotOrderStepsRepository
    {
        DbContext _Db;
        public LotOrderStepsRepository(DbContext dbContext)
        {
        _Db = dbContext;
        }
        public IQueryable<LotOrderSteps> GetAll(int page = 1, int pageSize = 10)
        {
            page = page <= 0 ? 1 : page;
            pageSize = pageSize <= 0 ? 10 : pageSize;
            return _Db.
                Set<LotOrderSteps>().
                AsNoTracking().
                Where(c => c.IsDeleted == false).
                Skip((page - 1) * pageSize).
                Take(pageSize);
        }
        public async Task<LotOrderSteps?> GetByIdAsync(int id)
        {
            return await _Db.
                Set<LotOrderSteps>().
                AsTracking().
                SingleOrDefaultAsync(x => x.Id == id && x.IsDeleted == false);
        }
        public async Task<LotOrderSteps> AddAsync(LotOrderSteps lotOrderSteps)
        {

            await _Db.
                  Set<LotOrderSteps>().
                  AddAsync(lotOrderSteps);
            return lotOrderSteps;


        }
        public LotOrderSteps Update(LotOrderSteps lotOrderSteps)
        {

            _Db.Set<LotOrderSteps>().
                Update(lotOrderSteps);
            return lotOrderSteps;


        }
        public bool SoftDelete(LotOrderSteps lotOrderSteps)
        {

            lotOrderSteps.IsDeleted = true;

            return true;

        }
        public async Task SaveChanges()
        {
            await _Db.SaveChangesAsync();
        }
    }
}
