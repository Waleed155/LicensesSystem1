namespace Licenses.Models
{
    public class ExcutivePosition:BaseModel
    {
        public string Name { get; set; }
        public IQueryable<Lot>? Lots { get; set; }   
    }
}
