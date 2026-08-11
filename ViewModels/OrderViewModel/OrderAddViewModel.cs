using System.ComponentModel.DataAnnotations;

namespace Licenses.ViewModels.OrderViewModel
{
    public class OrderAddViewModel
    {
        [RegularExpression(@"^(?=.{3,}$)[\u0621-\u064A]+(?:\s+[\u0621-\u064A]+)*$",
            ErrorMessage = "يجب ادخال اسم عربي بحد ادني 3 حروف")]
        public string Name { get; set; }

    }
}
