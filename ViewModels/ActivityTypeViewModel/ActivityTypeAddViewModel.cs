using System.ComponentModel.DataAnnotations;

namespace Licenses.ViewModels.ActivityTypeViewModel
{
    public class ActivityTypeAddViewModel
    {
        [Required]
        [RegularExpression(
    @"^(?=.{3,}$)[\u0621-\u064A]+(?:\s+[\u0621-\u064A]+)*$",
    ErrorMessage = "الاسم يجب أن يحتوي على أحرف عربية فقط، وبحد أدنى 3 أحرف")]
        public string Name { get; set; }
    }
}
