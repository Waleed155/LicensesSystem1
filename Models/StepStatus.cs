using System.ComponentModel.DataAnnotations;

namespace Licenses.Models
{
    public enum StepStatus
    {
        [Display(Name = " تم البدء")]
        Start,
       
        [Display(Name = " تم الانتهاء")]
        End,
    }
}
