using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
namespace Licenses.ViewModels.StepViewModel
{
    public class StepAddViewModel
    {
        [RegularExpression(@"^(?=.{3,}$)[\u0621-\u064A]+(?:\s+[\u0621-\u064A]+)*$",
            ErrorMessage = " الاسم يجب أن يحتوي على أحرف عربية فقط، وبحد أدنى 3 أحرف لا تضف مسافه ف نهايه الاسم ")]
        public string Name { get; set; }
    }
}
