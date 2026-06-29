using System.ComponentModel.DataAnnotations.Schema;

namespace Licenses.Dto.LotDto
{
    public class LotAddDto
    {
        public string LotNum { get; set; }
        public string AreaName { get; set; }
        public string NeighborhoodName { get; set; }
        public string OwnerName { get; set; } 
        public string UnitNumber { get; set; } = "-";
        public int FileNumber { get; set; }
    }
}
