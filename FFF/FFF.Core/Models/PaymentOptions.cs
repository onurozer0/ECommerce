using System.ComponentModel.DataAnnotations;

namespace FFF.Core.Models
{
	public enum PaymentOptions : byte
	{
		[Display(Name = "Kredi Kartı ile Ödeme")]
		KrediKarti = 1,
		//[Display(Name = "Havale/EFT ile Ödeme")]
		//Havale,
		//[Display(Name = "Kapıda Nakit/Kredi Kartı ile Ödeme")]
		//KapidaOdeme
	}
}
