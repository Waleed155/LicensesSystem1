using Licenses.ViewModels;
using System.Text.RegularExpressions;

namespace Licenses.Validators
{
    public  static class Validators
    {
        public static ResultViewModel<bool> NameValidator(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return ResultViewModel<bool>.Failure("لا يوجد اسم ");
            name = Regex.Replace(name, @"\s+", " ");
            name = name.Trim();
            if (!Regex.IsMatch(name, @"^(?=.{3,}$)[\u0621-\u064A]+(?:\s+[\u0621-\u064A]+)*$"))
                return ResultViewModel<bool>.Failure("الاسم يجب أن يحتوي على أحرف عربية فقط، وبحد أدنى 3 أحرف");
            return ResultViewModel<bool>.Success(true);
        }
    }
}
