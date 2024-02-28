using FFF.Core.Models;
using System.ComponentModel.DataAnnotations;

namespace FFF.Core.ViewModels
{
	public class UserAccountDetailsViewModel
	{
		[Display(Name = "Ad")]
		public string FirstName { get; set; }
		[Display(Name = "Soyad")]
		public string LastName { get; set; }
		[Display(Name = "E-Posta")]
		public string Email { get; set; }
		[Display(Name = "Telefon Numarası")]
		public string PhoneNumber { get; set; }
		[Display(Name = "Şifre")]
		public string Password { get; set; }
		[Display(Name = "Şifre Onayı")]
		public string PasswordConfirm { get; set; }
		[Display(Name = "Doğum Tarihi"), DataType(DataType.Date)]
		public DateTime? DateOfBirth { get; set; }
		[Display(Name = "Cinsiyet")]
		public Gender? Gender { get; set; }
	}
}
