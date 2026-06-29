using Licenses.Dto.LotDto;
namespace Licenses.Dto.ClientsDto
{
    public class ClientWithLotDto:ClientReadDto
    {
      public  IEnumerable<LotReadDto> ?LotReadDtos { get; set; }
    }
}
