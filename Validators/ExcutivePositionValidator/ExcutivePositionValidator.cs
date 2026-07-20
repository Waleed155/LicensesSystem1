using Licenses.Dto.ActivityTypeDto;
using Licenses.Dto.ExcutivePositionDto;
using Licenses.ViewModels;
using System.Text.RegularExpressions;

namespace Licenses.Validators.ExcutivePositionValidator
{
    public static class ExcutivePositionValidator
    {
        public static ResultViewModel<bool> NameValidator(string activityTypeName)
        {
            if (string.IsNullOrWhiteSpace(activityTypeName)) return ResultViewModel<bool>.Failure("لا يوجد اسم ");
            activityTypeName = activityTypeName.Trim();
            activityTypeName = Regex.Replace(activityTypeName, @"\s+", " ");
            if (!Regex.IsMatch(activityTypeName, @"^[\u0621-\u064A]{2,}(?:[\s+]+[\u0621-\u064A]{2,}){2,}$"))
                return ResultViewModel<bool>.Failure(" + الاسم يجب أن يحتوي على أحرف عربية فقط، , وبحد أدنى 3 أحرف ويجب الفصل بين كل كلمه وكلمه ب علامه ");
            return ResultViewModel<bool>.Success(true);
        }
        public static ResultViewModel<bool> ExcutiveValidator(ExcutivePositionAddDto excutivePositionAddDto)
        {
            var excutivePositionNameValidator = NameValidator(excutivePositionAddDto.Name);
            if (!excutivePositionNameValidator.State)
                return ResultViewModel<bool>.Failure(excutivePositionNameValidator.Message);
            return ResultViewModel<bool>.Success(true);
        }
    }
}
