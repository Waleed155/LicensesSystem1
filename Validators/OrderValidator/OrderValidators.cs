using Licenses.Dto.ExcutivePositionDto;
using Licenses.Dto.OrderDto;
using Licenses.ViewModels;

namespace Licenses.Validators.OrderValidator
{
    public static class OrderValidators
    {
        public static ResultViewModel<bool> OrderValidator(OrderAddDto orderAddDto)
        {
            var orderNameValidator = Validators.NameValidator(orderAddDto.Name);
            if (!orderNameValidator.State)
                return ResultViewModel<bool>.Failure(orderNameValidator.Message);
            return ResultViewModel<bool>.Success(true);
        }
    } 
}
