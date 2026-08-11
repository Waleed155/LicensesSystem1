using Licenses.Dto.OrderDto;
using Licenses.Models;
using Licenses.ViewModels.OrderViewModel;
using Mapster;
namespace Licenses.Mapping
{
    public static class MapesterConfig
    {
        public static void RegiserMappings()
        {
            TypeAdapterConfig<OrderReadDto, OrderReadViewModel>
                .NewConfig();
         
        }
    }
}
