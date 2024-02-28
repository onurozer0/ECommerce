using FFF.Core.Entities;

namespace FFF.Core.ViewModels
{
	public class CreateOrderViewModel
	{
        public List<Cart> CartItems { get; set; }
        public Order OrderDt { get; set; }
        public decimal TotalFee { get; set; }
        public decimal ShippingFee { get; set; }
        public List<UserAddresses> Addresses { get; set; }
    }
}
