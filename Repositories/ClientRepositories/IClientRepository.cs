using Licenses.Models;

namespace Licenses.Repositories.ClientRepositories
{
    public interface IClientRepository
    {
        public IQueryable<Client> GetAll(int page = 1, int pageSize = 15);
        public  Task<Client?> GetByIdAsync(int id);
        public  Task<Client> GetByIdWithLotsAsync(int id);
        public Task<Client?> GetByNationalIdAsync(string nationalid);
        public IQueryable<Client> GetByNameOrNationalId(string name, int page = 1, int pageSize = 15);
        public Task< Client> AddAsync(Client client);
        public Client Update(Client client);
        public bool SoftDelete(Client client);
        public  Task<int> CountAsync();
        public Task<int> CountSearchAsync(string search);

        public Task SaveChangesAsync();
        
    }
}
