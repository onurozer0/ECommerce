using FFF.Core.Entities;

namespace FFF.Core.ViewModels
{
	public class ProductViewModel
	{
		public Product Product { get; set; }
		public IEnumerable<Category> Categories { get; set; }
		public IEnumerable<Brand> Brands { get; set; }
	}
}
