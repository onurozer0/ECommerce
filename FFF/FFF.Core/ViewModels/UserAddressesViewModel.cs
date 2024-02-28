using FFF.Core.Models;
using System.ComponentModel.DataAnnotations;

namespace FFF.Core.ViewModels
{
	public class UserAddressesViewModel
	{
		public int ID { get; set; }
		[Display(Name = "Adres Başlığı")]
		public string Title { get; set; }
		[Display(Name = "Ad Soyad")]
		public string NameSurname { get; set; }
		[Display(Name = "Telefon Numarası")]
		public string Phone { get; set; }
		[Display(Name = "Adres")]
		public string Address { get; set; }
		[Display(Name = "Şehir")]
		public City City { get; set; }
		[Display(Name = "Posta Kodu")]
		public string Zipcode { get; set; }
	}
}
