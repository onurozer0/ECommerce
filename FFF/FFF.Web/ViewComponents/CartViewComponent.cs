using FFF.Core.Entities;
using FFF.Core.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FFF.Web.ViewComponents

{
	public class CartViewComponent : ViewComponent
	{
		private readonly IGenericRepository<Cart> _cartRepo;
		private readonly IGenericRepository<AppUser> _userManager;

		public CartViewComponent(IGenericRepository<Cart> cartRepo, IGenericRepository<AppUser> userManager)
		{
			_cartRepo = cartRepo;
			_userManager = userManager;
		}

		public IViewComponentResult Invoke()
		{
			if (User.Identity.IsAuthenticated)
			{
				var user = _userManager.Where(x => x.UserName == User.Identity.Name).First();
				if (user != null)
				{
					return View(_cartRepo.GetAll(x => x.UserId == user.Id).Include(x => x.Product).Include(x => x.Product.ProductPicture));
				}

			}
			return View();
		}
	}
}
