
namespace Licenses.Models
{
    public class Order:BaseModel
    {
        public string Name { get; set; }
        public IQueryable<LotOrder>? LotOrders { get; set; }
        public IQueryable<OrderSteps>? OrderSteps { get; set; }
        public IQueryable<Transaction> ? Transactions { get; set; }

    }
}
