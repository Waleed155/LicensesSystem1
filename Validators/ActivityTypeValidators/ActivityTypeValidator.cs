using Licenses.Dto.ActivityTypeDto;
using Licenses.ViewModels;
using System.Text.RegularExpressions;

namespace Licenses.Validators.ActivityTypeValidators
{
    public static class ActivityTypeValidator
    {
      
        public static ResultViewModel<bool>ActivityTypeValidate(ActivityTypeAddDto activityTypeAddDto)
        {
            var activityTypeNameValidator=Validators.NameValidator(activityTypeAddDto.Name);
            if(!activityTypeNameValidator.State) 
                return ResultViewModel<bool>.Failure(activityTypeNameValidator.Message);
            return ResultViewModel<bool>.Success(true) ;
        }
    }
}
