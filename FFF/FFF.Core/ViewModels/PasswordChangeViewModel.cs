using System.ComponentModel.DataAnnotations;

namespace FFF.Core.ViewModels
{
	public class PasswordChangeViewModel
	{
		[Display(Name = "Eski Şifre")]
		public string CurrentPassword { get; set; }
		[Display(Name = "Yeni Şifre")]
		public string NewPassword { get; set; }
		[Display(Name = "Şifreyi Onayla")]
		public string ConfirmPassword { get; set; }
	}
}
