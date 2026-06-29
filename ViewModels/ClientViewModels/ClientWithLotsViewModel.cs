using Licenses.ViewModels.LotViewModel;

namespace Licenses.ViewModels.ClientViewModels
{
    public class ClientWithLotsViewModel:ClientReadViewModel
    {
        public IEnumerable<LotReadViewModel>? LotsViewModel { get; set; }
    }
}
