using Licenses.Models;

namespace Licenses.Repositories.ClientRepositories
{
    public interface IClientRepository
    {
        public IQueryable<Client> GetAll(int page = 0, int pageSize = 10);
        public  Task<Client?> GetByIdAsync(int id);
        public Task< Client> AddAsync(Client client);
        public Client Update(Client client);
        public bool SoftDelete(Client client);
        public Task SaveChanges();
        
    }
}
