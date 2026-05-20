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
        public IQueryable<TransactionLotOrder> ?TransactionLotOrders {  get; set; }  
        public IQueryable<TransactionStages> ?TransactionStages { get; set; }

    }
}
