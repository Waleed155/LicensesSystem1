namespace Licenses.Models
{
    public class Client : BaseModel
    { 
        public string Name { get; set; }
        public string  NationalId { get; set; }
        public string FirstPhoneNumber { get; set; }
        public string ? SecondPhoneNumber { get; set; }
        public IQueryable<Lot> ?Lots { get; set; }


    }
}
