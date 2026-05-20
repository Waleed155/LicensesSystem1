using System.ComponentModel.DataAnnotations.Schema;

namespace Licenses.Models
{
    public class TransactionSteps:BaseModel
    {
        [ForeignKey("Transaction")]
        public int TransactionId { get; set; }
        public Transaction ? Transaction {  get; set; } 
    }
}
