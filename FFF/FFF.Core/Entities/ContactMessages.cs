namespace FFF.Core.Entities
{
	public class ContactMessages
	{
		public int ID { get; set; }
		public string NameSurname { get; set; }
		public string Email { get; set; }
		public string Message { get; set; }
		public string? ReplyMessage { get; set; }
		public bool isReplied { get; set; } = false;
		public DateTime CreatedDate { get; set; }
		public DateTime? RepliedDate { get; set; } = null;
	}
}
