namespace Licenses.Models
{
    public class Step:BaseModel
    {
        public string Name { get; set; }   
        
        public ICollection<OrderSteps>? OrderSteps { get; set; } = new HashSet<OrderSteps>();


    }
}
