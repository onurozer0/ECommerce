using System.ComponentModel.DataAnnotations;

namespace FFF.Core.ViewModels
{
	public class AssignRoleToUserViewModel
	{
		public string RoleId { get; set; }
		[Display(Name = "Rol Adı")]
		public string Name { get; set; }
		public bool isExist { get; set; }
	}
}
