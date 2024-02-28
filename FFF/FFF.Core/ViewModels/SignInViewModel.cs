using System.ComponentModel.DataAnnotations;

namespace FFF.Core.ViewModels
{
	public class SignInViewModel
	{
		[Display(Name = "E-Posta")]
		public string Email { get; set; }
		[Display(Name = "Şifre")]
		public string Password { get; set; }
		[Display(Name = "Beni Hatırla")]
		public bool RememberMe { get; set; }
	}
}
