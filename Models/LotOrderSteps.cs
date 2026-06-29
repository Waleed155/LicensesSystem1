using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Licenses.Models
{
    public class LotOrderSteps:BaseModel
    {
        
        public StepStatus StepStatus { get; set; }
        public String? Notes { get; set; }
        public DateOnly StartingDate { get; set; }= DateOnly.FromDateTime(DateTime.Now);
        public DateOnly EndingDate { get; set; }

        [ForeignKey("LotOrder")]
        public int LotOrderId { get; set; }

        [ForeignKey("OrderSteps")]
        public int StepOrderId { get; set; }
        public LotOrder? LotOrder { get; set; }
        public OrderSteps? OrderSteps { get; set; }
    }
}
