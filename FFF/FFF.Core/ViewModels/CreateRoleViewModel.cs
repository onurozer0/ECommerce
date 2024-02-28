using System.ComponentModel.DataAnnotations;

namespace FFF.Core.ViewModels
{
	public class CreateRoleViewModel
	{
		[Display(Name = "Rol Adı")]
		public string Name { get; set; }
	}
}
