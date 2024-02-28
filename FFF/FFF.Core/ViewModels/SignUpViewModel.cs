using System.ComponentModel.DataAnnotations;

namespace FFF.Core.ViewModels
{
	public class SignUpViewModel
	{
		[Display(Name = "Kullanıcı Adı")]
		public string UserName { get; set; }
		[Display(Name = "Ad")]
		public string Name { get; set; }
		[Display(Name = "Soyad")]
		public string Surname { get; set; }
		[Display(Name = "E-Posta")]
		public string Email { get; set; }
		[Display(Name = "Telefon Numarası")]
		public string PhoneNumber { get; set; }
		[Display(Name = "Şifre")]
		public string Password { get; set; }
		[Display(Name = "Şifre Onayı")]
		public string ConfirmPassword { get; set; }
	}
}
