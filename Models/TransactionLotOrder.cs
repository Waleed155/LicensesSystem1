

using System.ComponentModel.DataAnnotations.Schema;

namespace Licenses.Models
{
    public class TransactionLotOrder : BaseModel
    {
        public string TransactionNumber { get; set; }
        public string? Notes { get; set; }

        [ForeignKey("LotOrder")]
        public int LotOrderId { get; set; }
        public LotOrder? LotOrder { get; set; }
        [ForeignKey("TransactionStages")]
        public int TransactionStagesId {get;set;}
        public TransactionStages ? TransactionStages { get; set; }
        public IQueryable<TransactionLotOrderStages> ? TransactionLotOrderStages { get; set; }
        public IQueryable<Fees>? Fees { get; set; }


    }
}
