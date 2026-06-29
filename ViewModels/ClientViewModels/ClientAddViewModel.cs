using System.ComponentModel.DataAnnotations;

namespace Licenses.ViewModels.ClientViewModels
{
    public class ClientAddViewModel
    {
        [Required(ErrorMessage = "الاسم مطلوب")]

        [RegularExpression(@"^[\u0621-\u064A]{2,}(?:\s+[\u0621-\u064A]{2,}){2,}$", 
            ErrorMessage = "يجب إدخال اسم عربي ثلاثي على الأقل، وأن يكون كل جزء مكونًا من حرفين أو أكثر.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "الرقم القومي مطلوب")]
        [RegularExpression(
    @"^[23][0-9]{13}$",
    ErrorMessage = "يجب أن يتكون الرقم القومي من 14 رقمًا باللغة الإنجليزية ويبدأ بـ 2 أو 3.")]
        public string NationalId { get; set; }
        [Required(ErrorMessage = " رقم الهاتف مطلوب")]

        [RegularExpression(
    @"^(010|011|012|015)[0-9]{8}$",
    ErrorMessage = "يجب إدخال رقم هاتف مصري صحيح مكون من 11 رقم ويبدأ بـ 010 أو 011 أو 012 أو 015.")]
        public string FirstPhoneNumber { get; set; }

        [RegularExpression(
   @"^(010|011|012|015)[0-9]{8}$",
   ErrorMessage = "يجب إدخال رقم هاتف مصري صحيح مكون من 11 رقم ويبدأ بـ 010 أو 011 أو 012 أو 015.")]
        public string? SecondPhoneNumber { get; set; }
    }
}
