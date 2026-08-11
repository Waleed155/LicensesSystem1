using System.ComponentModel.DataAnnotations;

namespace Licenses.Dto.OrderDto
{
    public class OrderAddDto
    {
        [RegularExpression(@"^(?=.{3,}$)[\u0621-\u064A]+(?:\s+[\u0621-\u064A]+)*$",
             ErrorMessage = "الاسم يجب أن يحتوي على أحرف عربية فقط، وبحد أدنى 3 أحرف")]
        public string Name { get; set; }
    }
}
