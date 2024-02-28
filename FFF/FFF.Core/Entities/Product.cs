using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FFF.Core.Entities
{
	public class Product
	{
		public int ID { get; set; }
		[Display(Name = "Ürün Adı")]
		public string Name { get; set; }
		[Display(Name = "Stok Miktarı")]
		public int UnitsInStock { get; set; }
		[Display(Name = "Ürün Açıklaması")]
		public string Description { get; set; }
		[Display(Name = "Ürün Fiyatı")]
		public decimal Price { get; set; }
		[Display(Name = "Ürün Detayları")]
		public string Details { get; set; }
		[Display(Name = "Kargo Detayları")]
		public string ShippingDetails { get; set; }
		[Display(Name = "Kategorisi")]
		public int? CategoryId { get; set; }
		public Category Category { get; set; }
		public ICollection<ProductPicture> ProductPicture { get; set; }
		[Display(Name = "Markası")]
		public int? BrandId { get; set; }
		public Brand Brand { get; set; }
		[NotMapped]
		public int CartQuantity { get; set; }
	}
}
