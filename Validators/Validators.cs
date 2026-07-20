using Licenses.ViewModels;
using System.Text.RegularExpressions;

namespace Licenses.Validators
{
    public  static class Validators
    {
        public static ResultViewModel<bool> NameValidator(string activityTypeName)
        {
            if (string.IsNullOrWhiteSpace(activityTypeName)) return ResultViewModel<bool>.Failure("لا يوجد اسم ");
            activityTypeName = activityTypeName.Trim();
            activityTypeName = Regex.Replace(activityTypeName, @"\s+", " ");
            if (!Regex.IsMatch(activityTypeName, @"^(?=.{3,}$)[\u0621-\u064A]+(?:\s+[\u0621-\u064A]+)*$"))
                return ResultViewModel<bool>.Failure("الاسم يجب أن يحتوي على أحرف عربية فقط، وبحد أدنى 3 أحرف");
            return ResultViewModel<bool>.Success(true);
        }
    }
}
