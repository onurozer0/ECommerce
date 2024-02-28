using FFF.Core.Entities;

namespace FFF.Core.ViewModels
{
	public class CartViewModel
	{
		public List<Cart> Cart { get; set; }
		public decimal ShippingFee { get; set; }
	}
}
