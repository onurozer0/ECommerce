using FFF.Core.Models;

namespace FFF.Core.Entities
{
	public class UserAddresses
	{
		public int ID { get; set; }
		public string Title { get; set; }
		public string NameSurname { get; set; }
		public string Phone { get; set; }
		public string Address { get; set; }
		public City City { get; set; }
		public string Zipcode { get; set; }
		public string UserID { get; set; }

	}
}
