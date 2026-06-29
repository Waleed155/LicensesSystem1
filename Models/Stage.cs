namespace Licenses.Models
{
    public class Stage:BaseModel
    {
        public string Name { get; set; }
        public ICollection<TransactionStages>? TransactionStages { get; set; } = new HashSet<TransactionStages>();    
    }
}
