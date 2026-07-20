using System.ComponentModel.DataAnnotations;

namespace Licenses.ViewModels.ExcutivePositionViewModel
{
    public class ExcutivePositionAddViewModel
    {
        [RegularExpression(@"^[\u0621-\u064A]{2,}(?:[\s+]+[\u0621-\u064A]{2,}){2,}$",
    ErrorMessage = "+ الاسم يجب أن يحتوي على أحرف عربية فقط، , وبحد أدنى 3 أحرف ويجب الفصل بين كل كلمه وكلمه ب علامه")]
        public string Name { get; set; }
    }
}
