using Licenses.Dto.ActivityTypeDto;
using Licenses.Dto.OrderDto;
using Licenses.Models;
using Licenses.Repositories.OrderRepositpories;
using Licenses.Validators.ActivityTypeValidators;
using Mapster;
using MapsterMapper;
using Licenses.ViewModels;
using Microsoft.EntityFrameworkCore;
using Licenses.Validators.OrderValidator;
using Licenses.Dto;
namespace Licenses.Services.OrderServices
{
    public class OrderService:IOrderService
    {
        IOrderRepository _orderRepository;
        public OrderService(IOrderRepository orderRepository) { 
        _orderRepository = orderRepository;
        }
        public  async Task<ResultViewModel<PagedResult<OrderReadDto?>>> GetAllAsync(int page=1 ,int pageSize=15)
        {
            try
            {
                var orders = _orderRepository.GetAll( page ,  pageSize );
                int allOdersCount = await _orderRepository.CountAsync();
                var ordersReadDto = await orders.ProjectToType<OrderReadDto>().ToListAsync();
                var result = PagedResult<OrderReadDto?>.PaginationData(ordersReadDto, allOdersCount, page, pageSize);
                if (result.Items.Any())
                {
                 
                    return ResultViewModel<PagedResult<OrderReadDto?>>.Success(result);
            }
                else
            {
                return ResultViewModel<PagedResult<OrderReadDto?>>.Success(result,"لا يوجد اسماء طلبات ");
            }
        }
            catch
            {
                return ResultViewModel<PagedResult<OrderReadDto?>>.Failure("there is aproblem in service");

            }
        }
        public async Task<ResultViewModel<PagedResult<OrderReadDto?>>> 
            GetAllDeletedAsync(int page = 1, int pageSize = 15)
        {
            try
            {
                var deletedOrders = _orderRepository.GetAllDeleted(page, pageSize);
                int allDeletedOdersCount = await _orderRepository.CountDeletedAsync();
                if (deletedOrders != null)
                {
                    var ordersReadDto = await deletedOrders.ProjectToType<OrderReadDto>().ToListAsync();
                    var result = PagedResult<OrderReadDto?>.PaginationData(ordersReadDto, allDeletedOdersCount, page, pageSize);

                    return ResultViewModel<PagedResult<OrderReadDto?>>.Success(result);
                }
                else
                {
                    return ResultViewModel<PagedResult<OrderReadDto?>>.Failure(" لا يوجد طلبات تم مسحه م قبل");
                }
            }
            catch
            {
                return ResultViewModel<PagedResult<OrderReadDto?>>.Failure("there is aproblem in service");

            }
        }

        public async Task<ResultViewModel<OrderReadDto?>> GetByIdAsync(int id)
        {
            try
            {
                var order = await _orderRepository.GetByIdAsync(id);

                if (order != null)
                {
                    var orderReadDto = order.
                        Adapt<OrderReadDto>();
                    return ResultViewModel<OrderReadDto?>.
                        Success(orderReadDto);
                }
                else
                {
                    return ResultViewModel<OrderReadDto?>.Failure("لايوجد اسم طلب بهذه الهويه" + id);

                }
            }
            catch
            {
                return ResultViewModel<OrderReadDto?>.
                    Failure("problem in service layer ");

            }
        }
        public async Task<ResultViewModel<PagedResult<OrderReadDto?>>>
            SearchByNameAsync(string name ,int page=1,int pageSize=15)
        {
            try
            {
                var validatorResult = Validators.Validators.NameValidator(name);
                if (!validatorResult.State)
                    return ResultViewModel<PagedResult<OrderReadDto?>>.Failure(validatorResult.Message);
                var orders =  _orderRepository.SearchByName(name, page, pageSize);
                if(orders==null)
                    return ResultViewModel<PagedResult<OrderReadDto?>>.Failure("لايوجد طلبات بهذا الاسم");
                   var ordersReadDto=  await  orders.ProjectToType<OrderReadDto>().ToListAsync();
                int ordersCount = await _orderRepository.CountSearchAsync(name);

                var result =PagedResult<OrderReadDto?>.PaginationData(ordersReadDto,ordersCount,page,pageSize);
                return ResultViewModel<PagedResult<OrderReadDto?>>.Success(result);
            }
            catch
            {
                return ResultViewModel<PagedResult<OrderReadDto?>>.Failure("problem in service");

            }
        }
        public async Task<ResultViewModel<PagedResult<OrderReadDto?>>> SearchByDeletedNameAsync(string name, int page = 1, int pageSize = 15)
        {
            try
            {
               
                var deletedOrders = _orderRepository.SearchByNameDeleted(name, page, pageSize);
                if (!deletedOrders.Any() )
                    return ResultViewModel<PagedResult<OrderReadDto?>>.Failure("لايوجد طلبات بهذا الاسم");
                var ordersReadDto = await deletedOrders.ProjectToType<OrderReadDto>().ToListAsync();
                int deletedOrdersCount =await _orderRepository.CountSearchDeletedAsync(name);
                var result = PagedResult<OrderReadDto?>.PaginationData(ordersReadDto, deletedOrdersCount, page, pageSize);
                return ResultViewModel<PagedResult<OrderReadDto?>>.Success(result);
            }
            catch
            {
                return ResultViewModel<PagedResult<OrderReadDto?>>.Failure("problem in service");

            }
        }
        public async Task<ResultViewModel<OrderReadDto>> AddAsync(OrderAddDto orderAddDto)
        {
            try
            {
                var validationResult = OrderValidators.
                    OrderValidator(orderAddDto);
                var orderExistResult = await _orderRepository.
                    GetByNameAsync(orderAddDto.Name);
                if (!validationResult.State)
                {
                    return ResultViewModel<OrderReadDto>.Failure(validationResult.Message);

                }
                if (orderExistResult != null)
                {
                    return ResultViewModel<OrderReadDto>.
                        Failure(" هذا الطلب موجود بالفعل او تم مسحه يمكنك ارجاعه أفضل");
                }

                var order = orderAddDto.Adapt<Order>();
                var addedOrder = await _orderRepository.AddAsync(order);
                await _orderRepository.SaveChangesAsync();

                var orderReadDto = addedOrder.Adapt<OrderReadDto>();
                return ResultViewModel<OrderReadDto>.
                    Success(orderReadDto);
            }
            catch
            {
                return ResultViewModel<OrderReadDto>.
                    Failure("there is problem in service layer  ");

            }
        }
        public async Task<ResultViewModel<OrderReadDto>> UpdateAsync(OrderReadDto orderReadDto)
        {
            try
            {
                var orderAddDto = orderReadDto.Adapt<OrderAddDto>();
                var validationResult = OrderValidators.OrderValidator(orderAddDto);
                var orderExistResult = await _orderRepository.GetByNameAsync(orderReadDto.Name);
                if (!validationResult.State)
                {
                    return ResultViewModel<OrderReadDto>.Failure(validationResult.Message);
                }
                if (orderExistResult != null && orderExistResult.Id != orderReadDto.Id)
                {
                    return ResultViewModel<OrderReadDto>.
                        Failure("هذا النشاط موجود بالفعل او تم مسحه من قبل ");
                }
              
                var order = orderReadDto.Adapt<Order>();
    
                var updatetedOrder = _orderRepository.Update(order);
                await _orderRepository.SaveChangesAsync();
                var updatedOrderReadDto = updatetedOrder.Adapt<OrderReadDto>();
                return ResultViewModel<OrderReadDto>.Success(updatedOrderReadDto);
            }
            catch
            {
                return ResultViewModel<OrderReadDto>.Failure("there is problem in service");
            }
        }
        public async Task<ResultViewModel<bool>> SoftDeleteAsync(int id)
        {
            try
            {
                var order = await _orderRepository.GetByIdAsync(id);
                if (order == null) return ResultViewModel<bool>.Failure("لا يوجد طلب بهذا الرقم");
                bool result = _orderRepository.SoftDelete(order);
                if (!result) return ResultViewModel<bool>.Failure("problem in repo ");
                await _orderRepository.SaveChangesAsync();
                return ResultViewModel<bool>.Success(result);
            }
            catch
            {
                return ResultViewModel<bool>.Failure("there is problem in service");
            }

        }
        public async Task<ResultViewModel<bool>> Revive(int id)
        {
            try
            {
                var order = await _orderRepository.GetByIdAsync(id);
                if (order == null) 
                    return ResultViewModel<bool>.Failure("لا يوجد طلب تم مسحه بهذا الرقم");
                bool result = _orderRepository.Revive(order);
                if (!result) return ResultViewModel<bool>.Failure("problem in repo");
                await _orderRepository.SaveChangesAsync();
                return ResultViewModel<bool>.Success(result);
            }
            catch
            {
                return ResultViewModel<bool>.Failure("there is problem in service");
            }

        }

    }
}
