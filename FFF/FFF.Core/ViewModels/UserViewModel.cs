using System.ComponentModel.DataAnnotations;

namespace FFF.Core.ViewModels
{
	public class UserViewModel
	{

		public string Id { get; set; }
		[Display(Name = "Ad")]
		public string Name { get; set; }
		[Display(Name = "Soyad")]
		public string Surname { get; set; }
		[Display(Name = "Kullanıcı Adı")]
		public string UserName { get; set; }
		[Display(Name = "E-Posta")]
		public string Email { get; set; }
		[Display(Name = "Telefon Numarası")]
		public string PhoneNumber { get; set; }
		public bool EmailConfirmed { get; set; }
		public DateTime? LastLoginDate { get; set; }
		[Display(Name = "Durum")]
		public bool isBanned { get; set; }
		public string? LastLoginIP { get; set; }
	}
}
