namespace Licenses.Models
{
    public class Step:BaseModel
    {
        public string Name { get; set; }   
        
        public IQueryable<OrderSteps>?OrderSteps { get; set; }


    }
}
