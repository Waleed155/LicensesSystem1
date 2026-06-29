using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Licenses.Models
{
    public class OrderSteps:BaseModel
    {
        [Required]
        public int StepNumber { get; set; }
        [ForeignKey("Order")]
        public int OrderId { get; set; }
        public Order? Order { get; set; }

        [ForeignKey("Step")]
        public int StepId { get; set; }
        public Step? Step { get; set; }
        public ICollection<LotOrderSteps>?LotOrderSteps { get; set; }=new  HashSet<LotOrderSteps>();

    }
}
