using System.ComponentModel.DataAnnotations;

namespace Licenses.Models
{
    public class Step:BaseModel
    {
        [RegularExpression(@"^(?=.{3,}$)[\u0621-\u064A]+(?:\s+[\u0621-\u064A]+)*$",
            ErrorMessage ="plz enter only arabic word and ant word must be more 2 digit")]
        public string Name { get; set; }   
    
        public ICollection<OrderSteps>? OrderSteps { get; set; } = new HashSet<OrderSteps>();


    }
}
