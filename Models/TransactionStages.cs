using System.ComponentModel.DataAnnotations.Schema;

namespace Licenses.Models
{
    public class TransactionStages:BaseModel
    {
        public int StageNumber { get; set; }
        [ForeignKey("Transaction")]
        public int TransactionId { get; set; }
        public Transaction? Transaction { get; set; }
        [ForeignKey("Stage")]
        public int StageId { get; set; }
        public Stage? Stage { get; set; }
        public ICollection<TransactionLotOrderStages>? TransactionLotOrderStages { get; set; } = new HashSet<TransactionLotOrderStages>();


    }
}
