using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Licenses.Enums
{
    public enum FeesTypes
    {
        [Display(Name ="رسوم أوليه")]
        InitialFees,
        [Display(Name = "رسوم فعليه")]
        ActualFees,
        [Display(Name =" إكراميه")]
        Tips,
    }
}
