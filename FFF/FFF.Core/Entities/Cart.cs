namespace FFF.Core.Entities
{
	public class Cart
	{
		public int Id { get; set; }
		public Product Product { get; set; }
		public int ProductId { get; set; }
		public int Quantity { get; set; }
		public AppUser User { get; set; }
		public string UserId { get; set; }
	}
}
