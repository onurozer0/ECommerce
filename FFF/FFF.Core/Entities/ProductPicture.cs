using System.ComponentModel.DataAnnotations;

namespace FFF.Core.Entities
{
	public class ProductPicture
	{
		public int ID { get; set; }
		[Display(Name = "Resim Adı")]
		public string Name { get; set; }
		[Display(Name = "Resim")]
		public string Picture { get; set; }
		[Display(Name = "Görüntülenme Sırası")]
		public int DisplayIndex { get; set; }
		public int ProductID { get; set; }
		public Product Product { get; set; }
	}
}
