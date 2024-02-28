using System.ComponentModel.DataAnnotations;

namespace FFF.Core.ViewModels
{
	public class ResetPasswordViewModel
	{
		[Display(Name = "Yeni Şifre")]
		public string? NewPassword { get; set; }
		[Display(Name = "Şifre Onayı")]
		public string? ConfirmPassword { get; set; }
	}
}
