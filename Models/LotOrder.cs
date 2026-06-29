using Azure.Core.Pipeline;
using System.ComponentModel.DataAnnotations.Schema;

namespace Licenses.Models
{
    public class LotOrder:BaseModel
    {
        public DateOnly ContractDate { get; set; }
        public DateOnly StartWorkingDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);

        [ForeignKey("Order")]
        public int OrderId { get; set; }
        public Order ? Order { get; set; }
        [ForeignKey("Lot")]
        public int LotId { get; set; }
        public Lot ? Lot  { get; set; }
        public ICollection<TransactionLotOrder>? TransactionLotOrders { get; set; } = new HashSet<TransactionLotOrder>();
    }
}
