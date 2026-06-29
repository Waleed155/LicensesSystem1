namespace Licenses.Models
{
    public class ExcutivePosition:BaseModel
    {
        public string Name { get; set; }
        public ICollection<Lot>? Lots { get; set; } = new HashSet<Lot>();   
    }
}
