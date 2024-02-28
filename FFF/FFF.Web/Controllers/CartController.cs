using FFF.Core.Entities;
using FFF.Core.Services;
using FFF.Core.ViewModels;
using FFF.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Numerics;
using AutoMapper;

namespace FFF.Web.Controllers
{
	[Authorize]
	public class CartController : Controller
	{
		private readonly IGenericService<Product> _productSerivce;
		private readonly IGenericService<Cart> _cartService;
		private readonly UserManager<AppUser> _userManager;
		private readonly IUserService _userService;
		private readonly IMapper _mapper;
		private readonly IGenericService<Order> _orderService;
		private readonly IGenericService<OrderDetails> _orderDetailsService;
		private readonly IEmailService _emailService;

		public CartController(IGenericService<Cart> cartService, IGenericService<Product> productSerivce, UserManager<AppUser> userManager, IUserService userService, IMapper mapper, IGenericService<Order> orderService, IGenericService<OrderDetails> orderDetailsService, IEmailService emailService)
		{
			_cartService = cartService;
			_productSerivce = productSerivce;
			_userManager = userManager;
			_userService = userService;
			_mapper = mapper;
			_orderService = orderService;
			_orderDetailsService = orderDetailsService;
			_emailService = emailService;
		}
		[Route("/cart")]
		public async Task<IActionResult> Index()
		{
			decimal shippingFee = 0;
			var user = await _userManager.FindByNameAsync(User.Identity.Name);

			var cartItems = await _cartService.Where(x => x.UserId == user.Id).Include(x => x.Product).Include(x => x.Product.ProductPicture).ToListAsync();
			var count = cartItems.Count;
			if (count > 0)
			{
				foreach (var item in cartItems)
				{
					if (item.Quantity > 5)
					{
						var mod = item.Quantity % 5;
						var y = mod * 20;
						var x = item.Quantity / 5;
						var z = x * 80;
						var cast = z + y;
						shippingFee += cast;
					}
					else
					{
						shippingFee += 40;
					}
				}
			}
			var cartVm = new CartViewModel()
			{
				Cart = cartItems,
				ShippingFee = shippingFee,
			};
			return View(cartVm);
		}
		[Route("/cart/remove/{cartId}")]
		public async Task<IActionResult> Remove(int cartId)
		{
			var cart = await _cartService.GetByIdAsync(cartId);
			if (cart != null)
			{
				await _cartService.RemoveAsync(cart);
			}
			return RedirectToAction(nameof(Index));
		}
		[Route("/cart/removeall")]
		public async Task<IActionResult> RemoveAll()
		{
			var user = await _userManager.FindByNameAsync(User.Identity.Name);
			if (user != null)
			{
				var cartList = await _cartService.Where(x => x.UserId == user.Id).ToListAsync();
				await _cartService.RemoveRangeAsync(cartList);
			}
			return RedirectToAction(nameof(Index));
		}
		[Route("/cart/checkout")]
		public async Task<IActionResult> Checkout()
		{
			var user = await _userManager.FindByNameAsync(User.Identity.Name);
			var addressesVm = await _userService.GetUserAddressesAsync(user.Id);
			var addresses = _mapper.Map<List<UserAddresses>>(addressesVm);

			var cartItems = await _cartService.Where(x => x.UserId == user.Id).Include(x => x.Product).Include(x => x.Product.ProductPicture).ToListAsync();
			var count = cartItems.Count;
			decimal shippingFee = 0;
			decimal totalFee = 0;
			if (count > 0)
			{
				foreach (var item in cartItems)
				{
					if (item.Quantity > 5)
					{
						var mod = item.Quantity % 5;
						var y = mod * 20;
						var x = item.Quantity / 5;
						var z = x * 80;
						var cast = z + y;
						shippingFee += cast;
					}
					else
					{
						shippingFee += 40;
					}
					totalFee += (item.Quantity * item.Product.Price);
				}
			}
			var OrderVm = new CreateOrderViewModel()
			{
				ShippingFee = shippingFee,
				TotalFee = totalFee,
				CartItems = cartItems,
				Addresses = addresses,
			};
			return View(OrderVm);
		}
		[Route("/cart/checkout"), HttpPost, ValidateAntiForgeryToken]
		public async Task<IActionResult> Checkout(CreateOrderViewModel model)
		{
			var user = await _userManager.FindByNameAsync(User.Identity.Name);
			var cartItems = await _cartService.Where(x => x.UserId == user.Id).Include(x => x.Product).Include(x => x.Product.ProductPicture).ToListAsync();
			var count = cartItems.Count;
			
			if (!ModelState.IsValid)
			{
				var addressesVm = await _userService.GetUserAddressesAsync(user.Id);
				var addresses = _mapper.Map<List<UserAddresses>>(addressesVm);
				model.CartItems = cartItems;
				model.Addresses = addresses;
		        return View(model);
			}
			model.OrderDt.CreatedDate = DateTime.Now;
			string orderNumber = DateTime.Now.Microsecond.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Microsecond.ToString() +
				DateTime.Now.Year.ToString();
			if (orderNumber.Length > 20)
			{
				orderNumber = orderNumber.Substring(0, 20);
			}
			model.OrderDt.OrderNumber = orderNumber;
			model.OrderDt.PaymentOption = PaymentOptions.KrediKarti;
			model.OrderDt.OrderStatus = OrderStatus.OnayVerildi;
			model.OrderDt.UserId = user.Id;
			model.OrderDt.ShippingFee = model.ShippingFee;
			await _orderService.AddAsync(model.OrderDt);
			
			if(cartItems.Count > 0)
			{
				foreach (var item in cartItems)
				{
					string pp = "/img/hazirlaniyor.jpg";
					if (item.Product.ProductPicture.Count > 0)
					{
						pp = item.Product.ProductPicture.First().Picture;
					}

					OrderDetails details = new OrderDetails()
					{
						OrderID = model.OrderDt.ID,
						ProductID = item.ProductId,
						ProductName = item.Product.Name,
						ProductPicture = pp,
						Quantity = item.Quantity,
						ProductPrice = item.Product.Price,
					};
					await _orderDetailsService.AddAsync(details);
				}
				await _cartService.RemoveRangeAsync(cartItems);
				await _emailService.SendOrderCompleteMsg($"Siparişiniz Alındı, Sipariş Numaranız:{orderNumber}", user.Email);
				return RedirectToAction(nameof(CheckoutComplete));
			}
			return RedirectToAction(nameof(Index));
		}
		[Route("/cart/complete/")]
		public IActionResult CheckoutComplete()
		{
			return View();
		}
	}
}
