using Licenses.Dto.OrderDto;
using Licenses.Models;
using Licenses.Validators.OrderValidator;
using Licenses.ViewModels;

namespace Licenses.Services.OrderServices
{
    public interface IOrderService
    {
        public Task<ResultViewModel<IEnumerable<OrderReadDto?>>> GetAllAsync(int page = 1, int pageSize = 10);
        public Task<ResultViewModel<OrderReadDto?>> GetByIdAsync(int id);

        public Task<ResultViewModel<OrderReadDto>> AddAsync(OrderAddDto orderAddDto);

        public Task<ResultViewModel<OrderReadDto>> UpdateAsync(OrderReadDto orderReadDto);

        public Task<ResultViewModel<bool>> SoftDeleteAsync(int id);
    }
}
