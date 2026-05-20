using System.ComponentModel.DataAnnotations.Schema;

namespace Licenses.Models
{
    public class Lot:BaseModel
    {
        public string LotNum { get; set; }
        public string  AreaName { get; set; } 
        public string  NeighborhoodName { get; set; }
        public string BuildingNumber { get; set; } = "0";

        public int FileNumber { get; set; }
        public  DateTime EntryDate { get; set; }=DateTime.Now;
        [ForeignKey("Client")]
        public int ClientId { get; set; }
        public Client?  Client { get; set; }
        [ForeignKey("ExcutivePosition")]
        public int ExcutivePositionId { get; set; }
        public ExcutivePosition? ExcutivePosition { get; set; }
        [ForeignKey("ActivityType")]
        public int ActivityTypeId { get; set; }
        public ActivityType? ActivityType { get; set; }
        public IQueryable<LotOrder>? LotOrders { get; set; }


    }
}
