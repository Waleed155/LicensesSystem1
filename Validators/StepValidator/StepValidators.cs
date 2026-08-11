using Licenses.Dto.StepDto;
using Licenses.ViewModels;

namespace Licenses.Validators.StepValidator
{
    public static class StepValidators
    {
        public static ResultViewModel<bool> StepValidator(StepAddDto stepAddDto)
        {
            var stepNameValidator=Validators.NameValidator(stepAddDto.Name);
            if (!stepNameValidator.State) { 
            return ResultViewModel<bool>.Failure(stepNameValidator.Message);
            }
            return ResultViewModel<bool>.Success(true);
        }
    }
}
