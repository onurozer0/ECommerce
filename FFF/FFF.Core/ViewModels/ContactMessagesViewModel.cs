using System.ComponentModel.DataAnnotations;

namespace FFF.Core.ViewModels
{
	public class ContactMessagesViewModel
	{
		public int ID { get; set; }
		[Display(Name = "Ad Soyad")]
		public string NameSurname { get; set; }
		[Display(Name = "E-Posta")]
		public string Email { get; set; }
		[Display(Name = "Mesaj")]
		public string Message { get; set; }
		public DateTime CreatedDate { get; set; }
		[Display(Name = "Yanıtınız")]
		public string ReplyMessage { get; set; }
		public DateTime RepliedDate { get; set; }
	}
}
