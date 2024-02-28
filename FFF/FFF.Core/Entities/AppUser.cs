using FFF.Core.Models;
using Microsoft.AspNetCore.Identity;

namespace FFF.Core.Entities
{
	public class AppUser : IdentityUser
	{
		public string Name { get; set; }

		public string Surname { get; set; }
		public DateTime? DateOfBirth { get; set; }
		public Gender? Gender { get; set; }
		public DateTime? LastLoginDate { get; set; }
		public string? LastLoginIP { get; set; }
		public bool isBanned { get; set; }
		public ICollection<UserAddresses> Addresses { get; set; }
		public ICollection<Cart> CartItems { get; set; }
		public ICollection<Order> Orders { get; set; }

	}
}
