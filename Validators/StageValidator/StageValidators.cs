using Licenses.Dto.StageDto;
using Licenses.ViewModels;

namespace Licenses.Validators.StageValidator
{
    public  static class StageValidators
    {
        public static ResultViewModel<bool> StageValidator(StageAddDto stageAddDto)
        {
            var stageNameValidator = Validators.NameValidator(stageAddDto.Name);
            if (!stageNameValidator.State)
            {
                return ResultViewModel<bool>.Failure(stageNameValidator.Message);
            }
            return ResultViewModel<bool>.Success(true);
        }
    }
}
