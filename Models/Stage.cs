namespace Licenses.Models
{
    public class Stage:BaseModel
    {
        public string Name { get; set; }
        public IQueryable<TransactionStages> ? TransactionStages { get; set; }    
    }
}
