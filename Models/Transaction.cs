 using Licenses.Enums;
using System.ComponentModel.DataAnnotations.Schema;
namespace Licenses.Models
{
    public class Transaction:BaseModel
    {
        public string Name { get; set; }

        [ForeignKey("Order")]
        public int OrderId { get; set; }
        public Order? Order { get; set; }
        public ICollection<TransactionLotOrder>? TransactionLotOrders { get; set; } = new HashSet<TransactionLotOrder>();  
        public ICollection<TransactionStages>? TransactionStages { get; set; } = new HashSet<TransactionStages>();

    }
}
