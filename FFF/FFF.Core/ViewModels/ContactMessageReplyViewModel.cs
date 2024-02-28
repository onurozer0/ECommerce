using System.ComponentModel.DataAnnotations;

namespace FFF.Core.ViewModels
{
	public class ContactMessageReplyViewModel
	{
		public int ID { get; set; }
		[Display(Name = "Yanıtınız")]
		public string? ReplyMessage { get; set; }
		public string Email { get; set; }
		[Display(Name = "Mesaj")]
		public string Message { get; set; }
	}
}
