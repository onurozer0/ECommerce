
using System.ComponentModel.DataAnnotations;

namespace FFF.Core.ViewModels
{
	public class ForgotPasswordViewModel
	{
		[Display(Name = "E-Posta")]
		public string Email { get; set; }
	}
}
