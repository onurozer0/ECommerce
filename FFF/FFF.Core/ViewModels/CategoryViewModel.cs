using FFF.Core.Entities;

namespace FFF.Core.ViewModels
{
	public class CategoryViewModel
	{
		public Category Category { get; set; }
		public IEnumerable<Category> Categories { get; set; }
	}
}
