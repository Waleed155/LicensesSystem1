using Licenses.Dto.ClientsDto;

namespace Licenses.ViewModels.ClientViewModels
{
    public class ClientIndexViewModel
    {
        
            public IEnumerable<ClientReadDto> Clients { get; set; } = [];

            public int CurrentPage { get; set; }

            public int TotalPages { get; set; }

            public string? Search { get; set; }
        
    }
}
