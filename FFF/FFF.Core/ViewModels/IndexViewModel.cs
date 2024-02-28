using FFF.Core.Entities;

namespace FFF.Core.ViewModels
{
	public class IndexViewModel
	{
		public IEnumerable<Product> Products { get; set; }
		public IEnumerable<Category> Categories { get; set; }
		public IEnumerable<Slide>? Slides { get; set; }
	}
}
