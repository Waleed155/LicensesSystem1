namespace Licenses.Models
{
    public class ActivityType:BaseModel
    {
        public string Name { get; set; }
        public ICollection<Lot>? Lots { get; set; }=new HashSet<Lot>();
    }
}
