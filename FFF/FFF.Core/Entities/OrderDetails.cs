using System.ComponentModel.DataAnnotations;

namespace FFF.Core.Entities
{
	public class OrderDetails
	{
		public int ID { get; set; }

		[Display(Name = "Sipariş Numarası")]
		public int OrderID { get; set; }
		public Product Product { get; set; }
		public Order Order { get; set; }

		[Display(Name = "Ürün ID")]
		public int ProductID { get; set; }

		[Display(Name = "Ürün Adı")]
		public string ProductName { get; set; }


		[Display(Name = "Ürün Resmi")]
		public string ProductPicture { get; set; }


		[Display(Name = "Ürün Fiyatı")]
		public decimal ProductPrice { get; set; }

		[Display(Name = "Miktar")]
		public int Quantity { get; set; }
	}
}
