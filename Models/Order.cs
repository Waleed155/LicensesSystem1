
namespace Licenses.Models
{
    public class Order:BaseModel
    {
        public string Name { get; set; }
        public ICollection<LotOrder>? LotOrders { get; set; } = new HashSet<LotOrder>();
        public ICollection<OrderSteps>? OrderSteps { get; set; } = new HashSet<OrderSteps>();
        public ICollection<Transaction>? Transactions { get; set; } = new HashSet<Transaction>();

    }
}
