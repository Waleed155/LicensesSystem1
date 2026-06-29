using Licenses.Dto;
using Licenses.Dto.ClientsDto;
using Licenses.Models;
using Licenses.ViewModels;
using Mapster;

namespace Licenses.Services.ClientServices
{
    public interface IClientService
    {

        public Task<ResultViewModel<PagedResult<ClientReadDto?>>> GetAllAsync(int page = 1, int pageSize = 15);
        public Task<ResultViewModel<PagedResult<ClientReadDto>>> GetByNameOrNationalId(string name, int page = 1, int pageSize = 15);
        public Task<ResultViewModel<ClientReadDto?>> GetByIdAsync(int id);
        public Task<ResultViewModel<ClientWithLotDto>> GetByIdWithLotsAsync(int id);
        public Task<ResultViewModel<ClientReadDto?>> GetByNationalIdAsync(string nationalid);
        public Task<ResultViewModel<ClientReadDto>> AddAsync(ClientAddDto clientAddDto);
        public  Task<ResultViewModel<ClientReadDto>> UpdateAsync(ClientReadDto clientReadDto);
        public  Task<ResultViewModel<bool>> SoftDeleteAsync(int id);
    }
}
