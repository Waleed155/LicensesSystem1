namespace Licenses.Models
{
    public class ActivityType:BaseModel
    {
        public string Name { get; set; }
        public IQueryable<Lot>? Lots { get; set; }
    }
}
