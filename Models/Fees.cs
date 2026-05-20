using Licenses.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlTypes;

namespace Licenses.Models
{
    public class Fees:BaseModel
    {
        public decimal Amount { get; set; }
        public DateTime CollectionDateTime { get; set; }= DateTime.Now; 
        public FeesTypes FeesTypes { get; set; }
         [ForeignKey("TransactionLotOrder")]
        public int TransactionLotOrderId { get; set; }
        public TransactionLotOrder? TransactionLotOrder { get; set; }    
    }
}
