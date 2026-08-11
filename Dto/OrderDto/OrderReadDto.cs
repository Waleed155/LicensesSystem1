using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System.ComponentModel.DataAnnotations;

namespace Licenses.Dto.OrderDto
{
    public class OrderReadDto:OrderAddDto
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; }

    }
}
