using System.ComponentModel.DataAnnotations;

namespace FFF.Core.ViewModels
{
	public class RoleUpdateViewModel
	{
		public string ID { get; set; }
		[Display(Name = "Rol Adı")]
		public string Name { get; set; }
	}
}
