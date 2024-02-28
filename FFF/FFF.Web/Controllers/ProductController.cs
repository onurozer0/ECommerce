using FFF.Core.Entities;
using FFF.Core.Repositories;
using FFF.Core.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FFF.Web.Controllers
{
	public class ProductController : Controller
	{
		private readonly IGenericRepository<Product> _genericRepo;
		private readonly IGenericService<Product> _productSerivce;
		private readonly IGenericService<Cart> _cartService;
		private readonly UserManager<AppUser> _userManager;
		public ProductController(IGenericRepository<Product> genericRepo, IGenericService<Product> productSerivce, IGenericService<Cart> cartService, UserManager<AppUser> userManager)
		{
			_genericRepo = genericRepo;
			_productSerivce = productSerivce;
			_cartService = cartService;
			_userManager = userManager;
		}
		[Route("/product/detail/{productId}")]
		public async Task<IActionResult> Index(int productId)
		{
			var product = await _genericRepo.GetAll(x => x.ID == productId).Include(x => x.Category).Include(x => x.ProductPicture).Include(x => x.Brand).Where(x => x.ID == productId).ToListAsync();
			if (product != null)
			{
				var pduct = product.First();
				return View(pduct);
			}
			return RedirectToAction("Index", "Home");
		}
		[Route("/cart/addcart/{productId}/{quantity}"), HttpPost]
		public async Task<IActionResult> AddCart(int productId, int quantity)
		{
			var product = await _genericRepo.GetAll(x => x.ID == productId).Include(x => x.Category).Include(x => x.ProductPicture).Include(x => x.Brand).FirstAsync();
			var user = await _userManager.FindByNameAsync(User.Identity.Name);
			bool isExist = false;
			if (product != null)
			{
				var userCartItems = await _cartService.Where(x => x.UserId == user.Id).ToListAsync();
				if (userCartItems.Any())
				{
					foreach (var item in userCartItems)
					{
						if (item.ProductId == productId)
						{
							var cast = item.Quantity + quantity;
							if (cast > product.UnitsInStock)
							{
								var t = product.UnitsInStock - item.Quantity;
								item.Quantity += t;
								await _cartService.UpdateAsync(item);
							}
							else
							{
								item.Quantity += quantity;
								await _cartService.UpdateAsync(item);
							}
							isExist = true;
							break;
						}
					}

				}
				if (!isExist)
				{
					if (quantity <= product.UnitsInStock)
					{
						var cart = new Cart()
						{
							ProductId = product.ID,
							Quantity = quantity,
							UserId = user.Id,
						};
						await _cartService.AddAsync(cart);
					}
				}
				return View("Index", product);

			}
			return RedirectToAction("Index", "HomeController");
		}
	}
}
