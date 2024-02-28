using System.ComponentModel.DataAnnotations;

namespace FFF.Core.Entities
{
	public class Category
	{
		public int ID { get; set; }
		[Display(Name = "Üst Kategori Adı")]
		public int? ParentID { get; set; }
		public Category ParentCategory { get; set; }
		public ICollection<Category> SubCategories { get; set; }
		[Display(Name = "Kategori Adı")]
		public string Name { get; set; }
		[Display(Name = "Görüntüleme Sırası")]
		public int DisplayIndex { get; set; }

		public ICollection<Product> Products { get; set; }
	}
}
