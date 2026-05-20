using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Licenses.Enums
{
    public enum TransactionStages
    {
        [Display(Name ="تمرير عقاريه")]
        RealEstateTransfer,
        [Display(Name = " فتح إدارات")]
        OpenDepartments,
        [Display(Name = "تمرير ماليه")]
        FinancialTransfer,
        [Display(Name = "تمرير طرق")]
        WaysTransfer,
        [Display(Name = "تمرير مساحه")]
        AreaTransfer,
        [Display(Name = "تمرير امن")]
        SafetyTransfer,
        [Display(Name = "تمرير احياء")]
        NeighborhoodsTransfer,
        [Display(Name = "تمرير معاينات")]
        PreviewsTransfer,
        [Display(Name = "تمرير تراخيص تشغيل")]
        OperatingLicencesTransfer

    }
}
