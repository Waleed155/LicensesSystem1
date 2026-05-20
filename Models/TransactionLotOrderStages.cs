using System.ComponentModel.DataAnnotations.Schema;

namespace Licenses.Models
{
    public class TransactionLotOrderStages:BaseModel
    {
        public StepStatus StepStatus { get; set; }
        public string? Notes { get; set; }
        public DateOnly StartingDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);
        public DateOnly EndingDate { get; set; }
        [ForeignKey("TransactionStages")]
        public int TransactionStagesId { get; set; }
        public TransactionStages ?TransactionStages { get; set; }

        [ForeignKey("TransactionLotOrder")]
        public int  TransactionLotOrderId { get; set; } 
        public TransactionLotOrder ?TransactionLotOrder { get; set; }
    }
}
