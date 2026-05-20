using Licenses.Models;
using Microsoft.EntityFrameworkCore;

namespace Licenses.Repositories.ClientRepositories
{
    public class ClientRepository:IClientRepository
    {
        DbContext _Db;
        public ClientRepository(DbContext Db)
        {
            _Db = Db;
        }
        public IQueryable<Client> GetAll(int page = 1, int pageSize = 10)
        {
            page = page <= 0 ? 1 : page;
            pageSize = pageSize <= 0 ? 10 : pageSize;
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
        public async Task SaveChanges()
        {
            await _Db.SaveChangesAsync();
        }

    }
}
