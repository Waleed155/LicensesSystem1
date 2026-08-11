namespace Licenses.ViewModels.OrderViewModel
{
    public class OrderReadViewModel:OrderAddViewModel
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; }

    }
}
