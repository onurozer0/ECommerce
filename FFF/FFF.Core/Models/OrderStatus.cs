using System.ComponentModel.DataAnnotations;

namespace FFF.Core.Models
{
	public enum OrderStatus : byte
	{
		[Display(Name = "Hazırlanıyor")]
		Hazirlaniyor = 1,
		[Display(Name = "Onaylandı")]
		OnayVerildi,
		[Display(Name = "Kargoya Verildi")]
		KargoyaVerildi,
		[Display(Name = "Teslim Edildi")]
		TeslimEdildi,
		[Display(Name = "İptal Edildi")]
		IptalEdildi
	}
}
