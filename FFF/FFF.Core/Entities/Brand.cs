using System.ComponentModel.DataAnnotations;

namespace FFF.Core.Entities
{
	public class Brand
	{
		public int ID { get; set; }
		[Display(Name = "Marka Adı")]
		public string Name { get; set; }
		[Display(Name = "Görüntülenme Sırası")]
		public int DisplayIndex { get; set; }

		public ICollection<Product> Products { get; set; }
	}
}
