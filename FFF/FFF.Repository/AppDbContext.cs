using FFF.Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FFF.Repository
{
	public class AppDbContext : IdentityDbContext<AppUser, AppRole, string>
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
		{

		}
		protected override void OnModelCreating(ModelBuilder builder)
		{
			builder.Entity<AppUser>().Property(x => x.Name).IsRequired().HasColumnType("nvarchar(50)").HasMaxLength(50);
			builder.Entity<AppUser>().Property(x => x.Surname).IsRequired().HasColumnType("nvarchar(50)").HasMaxLength(50);
			builder.Entity<AppUser>().Property(x => x.LastLoginIP).HasColumnType("varchar(20)");
			builder.Entity<AppUser>().HasMany(x => x.Orders).WithOne(x => x.User).HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.NoAction);

			//------------------------------------------------------------------------------------------------------------------
			builder.Entity<UserAddresses>().Property(x => x.Title).IsRequired().HasColumnType("nvarchar(20)").HasMaxLength(20);
			builder.Entity<UserAddresses>().Property(x => x.NameSurname).IsRequired().HasColumnType("nvarchar(60)").HasMaxLength(60);
			builder.Entity<UserAddresses>().Property(x => x.Phone).IsRequired().HasColumnType("varchar(20)").HasMaxLength(20);
			builder.Entity<UserAddresses>().Property(x => x.Address).IsRequired().HasColumnType("nvarchar(400)").HasMaxLength(400);
			builder.Entity<UserAddresses>().Property(x => x.Zipcode).IsRequired().HasColumnType("varchar(10)").HasMaxLength(10);
			builder.Entity<UserAddresses>().HasOne<AppUser>().WithMany(x => x.Addresses).HasForeignKey(x => x.UserID).OnDelete(DeleteBehavior.Cascade);
			//------------------------------------------------------------------------------------------------------------------
			builder.Entity<OrderDetails>().Property(x => x.ProductName).HasColumnType("nvarchar(150)").HasMaxLength(150);
			builder.Entity<OrderDetails>().Property(x => x.ProductPicture).HasColumnType("nvarchar(200)").HasMaxLength(200);
			builder.Entity<OrderDetails>().Property(x => x.ProductPrice).HasColumnType("decimal(18,2)");
			////------------------------------------------------------------------------------------------------------------------
			builder.Entity<Order>().Property(x => x.OrderNumber).HasColumnType("varchar(20)").HasMaxLength(20);
			//------------------------------------------------------------------------------------------------------------------
			builder.Entity<ContactMessages>().Property(x => x.Message).HasColumnType("text").HasMaxLength(2000);
			builder.Entity<ContactMessages>().Property(x => x.ReplyMessage).HasColumnType("text").HasMaxLength(2000);
			builder.Entity<ContactMessages>().Property(x => x.NameSurname).HasColumnType("nvarchar(50)").HasMaxLength(50);
			builder.Entity<ContactMessages>().Property(x => x.Email).HasColumnType("nvarchar(50)").HasMaxLength(50);
			//------------------------------------------------------------------------------------------------------------------
			builder.Entity<Brand>().Property(x => x.Name).IsRequired().HasColumnType("nvarchar(100)").HasMaxLength(100);
			//------------------------------------------------------------------------------------------------------------------
			//------------------------------------------------------------------------------------------------------------------
			builder.Entity<Category>().Property(x => x.Name).IsRequired().HasColumnType("nvarchar(50)").HasMaxLength(50);
			builder.Entity<Category>().HasOne(x => x.ParentCategory).WithMany(x => x.SubCategories).HasForeignKey(x => x.ParentID);

			//------------------------------------------------------------------------------------------------------------------
			//------------------------------------------------------------------------------------------------------------------ 
			builder.Entity<Product>().Property(x => x.Name).IsRequired().HasColumnType("nvarchar(100)").HasMaxLength(100);
			builder.Entity<Product>().Property(x => x.Description).IsRequired().HasColumnType("nvarchar(250)").HasMaxLength(250);
			builder.Entity<Product>().Property(x => x.Details).IsRequired().HasColumnType("text").HasMaxLength(2000);
			builder.Entity<Product>().Property(x => x.ShippingDetails).IsRequired().HasColumnType("nvarchar(250)").HasMaxLength(250);
			builder.Entity<Product>().Property(x => x.UnitsInStock).IsRequired();
			builder.Entity<Product>().Property(x => x.Price).IsRequired().HasColumnType("decimal(18,2)");

			//------------------------------------------------------------------------------------------------------------------
			builder.Entity<Slide>().Property(x => x.Slogan).HasColumnType("nvarchar(50)").HasMaxLength(50);
			builder.Entity<Slide>().Property(x => x.Title).HasColumnType("nvarchar(250)").HasMaxLength(250).IsRequired();
			builder.Entity<Slide>().Property(x => x.Description).HasColumnType("nvarchar(150)").HasMaxLength(150).IsRequired();
			builder.Entity<Slide>().Property(x => x.Picture).HasColumnType("varchar(250)").HasMaxLength(250).IsRequired();
			builder.Entity<Slide>().Property(x => x.Link).HasColumnType("varchar(150)").HasMaxLength(150);
			builder.Entity<Slide>().Property(x => x.LinkTitle).HasColumnType("nvarchar(50)").HasMaxLength(50);
			builder.Entity<Slide>().Property(x => x.DisplayIndex).IsRequired();
			//------------------------------------------------------------------------------------------------------------------
			builder.Entity<Cart>().HasOne(x => x.User).WithMany(x => x.CartItems).HasForeignKey(x => x.UserId);
			base.OnModelCreating(builder);
		}
		public DbSet<UserAddresses> UserAddress { get; set; }
		public DbSet<Order> Orders { get; set; }
		public DbSet<OrderDetails> OrderDetails { get; set; }
		public DbSet<ContactMessages> ContactMessages { get; set; }
		public DbSet<Slide> Slides { get; set; }
		public DbSet<Cart> Carts { get; set; }
	}
}
