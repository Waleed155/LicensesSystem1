using Licenses.Dto;
using Licenses.Dto.OrderDto;
using Licenses.Models;
using Licenses.Validators.OrderValidator;
using Licenses.ViewModels;

namespace Licenses.Services.OrderServices
{
    public interface IOrderService
    {
        public Task<ResultViewModel<PagedResult<OrderReadDto?>>> GetAllAsync(int page , int pageSize);
        public Task<ResultViewModel<PagedResult<OrderReadDto?>>> GetAllDeletedAsync(int page, int pageSize);

        public Task<ResultViewModel<PagedResult<OrderReadDto?>>> SearchByNameAsync(string name,int page , int pageSize );
        public Task<ResultViewModel<PagedResult<OrderReadDto?>>> SearchByDeletedNameAsync(string name, int page , int pageSize);

        public Task<ResultViewModel<OrderReadDto?>> GetByIdAsync(int id);

        public Task<ResultViewModel<OrderReadDto>> AddAsync(OrderAddDto orderAddDto);

        public Task<ResultViewModel<OrderReadDto>> UpdateAsync(OrderReadDto orderReadDto);

        public Task<ResultViewModel<bool>> SoftDeleteAsync(int id);
        public Task<ResultViewModel<bool>> Revive(int id);

    }
}
