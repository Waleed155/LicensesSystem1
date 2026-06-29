using Licenses.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Eventing.Reader;

namespace Licenses.Repositories.ClientRepositories
{
    public class ClientRepository:IClientRepository
    {
        DbContext _Db;
        public ClientRepository(DbContext Db)
        {
            _Db = Db;
        }
        public  IQueryable<Client> GetAll(int page = 1, int pageSize = 15)
        {
            page = page <= 0 ? 1 : page;
            pageSize = pageSize <= 0 ? 15 : pageSize;
            return _Db.
                Set<Client>().
                AsNoTracking().
                Where(c => c.IsDeleted == false).
                Skip((page-1) * pageSize).
                Take(pageSize );
        }
        public async Task< Client?> GetByIdAsync(int id)
        {
            return await _Db.
                Set<Client>().
                AsTracking().
                SingleOrDefaultAsync(x => x.Id == id && x.IsDeleted == false);
        }
        public async Task<Client?> GetByNationalIdAsync(string nationalid)
        {
            
                return await _Db.
                   Set<Client>().
                   AsNoTracking().
                   FirstOrDefaultAsync(c => c.NationalId == nationalid && !c.IsDeleted);
            
        }
        public IQueryable<Client?> GetByNameOrNationalId(string search, int page = 1, int pageSize = 15)
        {
            page = page <= 0 ? 1 : page;
            pageSize = pageSize <= 0 ? 15 : pageSize;
            return  _Db.
                Set<Client>().
                Where(c=>(c.Name.Contains(search)||c.NationalId.Contains(search))  && !c.IsDeleted)     
                .AsNoTracking().

                Skip((page - 1) * pageSize).
                Take(pageSize);

        }
        public  async Task<Client?> GetByIdWithLotsAsync (int id )
        {
            var entity= await _Db.
                Set<Client>().Include(x => x.Lots).
                AsNoTracking().
                SingleOrDefaultAsync(x => x.Id == id && x.IsDeleted == false);
            return entity;
           
                
        }
        public  async Task< Client> AddAsync(Client client)
        {
           
              await  _Db.
                    Set<Client>().
                    AddAsync(client);
                return client;
           

        }
        public Client Update(Client client)
        {

            _Db.Set<Client>().Update(client);
            return client;


        }
        public  bool SoftDelete(Client client)
        {

            client.IsDeleted = true;

            return true;

        }
        public async Task<int> CountAsync()
        {
            return await _Db.
                Set<Client>()
                .CountAsync(c => !c.IsDeleted);
        }
        public async Task<int> CountSearchAsync(string search)
        {
            return await _Db.
                Set<Client>().
                Where(c => (c.Name.Contains(search) || c.NationalId.Contains(search))&& !c.IsDeleted)
                .CountAsync();
        }
        public async Task SaveChangesAsync()
        {
            await _Db.SaveChangesAsync();
        }

    }
}
