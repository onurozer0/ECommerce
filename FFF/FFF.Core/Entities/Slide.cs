using System.ComponentModel.DataAnnotations;

namespace FFF.Core.Entities
{
	public class Slide
	{
		public int ID { get; set; }

		[Display(Name = "Slogan")]
		public string? Slogan { get; set; }

		[Display(Name = "Slayt Başlığı")]
		public string Title { get; set; }

		[Display(Name = "Slayt Açıklaması")]
		public string Description { get; set; }

		[Display(Name = "Resim Dosyası")]
		public string Picture { get; set; }

		[Display(Name = "Link Adresi")]
		public string Link { get; set; }
		[Display(Name = "Link Başlığı")]
		public string LinkTitle { get; set; }

		[Display(Name = "Görüntüleme Sırası")]
		public int DisplayIndex { get; set; }
	}
}
