using AutoMapper;
using FFF.Core.Entities;
using FFF.Core.Models;
using FFF.Core.Services;
using FFF.Core.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FFF.Web.Controllers
{
	[Authorize]
	public class OrdersController : Controller
	{
		private readonly SignInManager<AppUser> _signInManager;
		private readonly IUserService _userService;
		private readonly UserManager<AppUser> _userManager;
		private readonly IGenericService<UserAddresses> _userAddressesService;
		private readonly IMapper _mapper;
		private readonly IGenericService<Order> _orderService;

		public OrdersController(SignInManager<AppUser> signInManager, IUserService userService, UserManager<AppUser> userManager, IGenericService<UserAddresses> userAddressesService, IMapper mapper, IGenericService<Order> orderService)
		{
			_signInManager = signInManager;
			_userService = userService;
			_userManager = userManager;
			_userAddressesService = userAddressesService;
			_mapper = mapper;
			_orderService = orderService;
		}

		[Route("/user/orders")]
		public async Task<IActionResult> MyOrders()
		{
			var user = await _userManager.FindByNameAsync(User.Identity.Name);
			var orders = await _orderService.Where(x => x.UserId == user.Id).Include(x => x.OrderDetails).ToListAsync();
			if(orders.Count > 0)
			{
				foreach (var order in orders)
				{
					foreach(var od in order.OrderDetails)
					{
						order.TotalQuantity += od.Quantity;
						order.TotalFee += (od.Quantity * od.ProductPrice);
					}
				}
			}
			var myOrdersVm = new MyOrdersViewModel()
			{
				Orders = orders,
			};
			return View(myOrdersVm);
		}
		[Route("/user/orders/cancel/{orderId}")]
		public async Task<IActionResult> Cancel(int orderId)
		{
			var order = await _orderService.GetByIdAsync(orderId);
			if(order == null)
			{
				return RedirectToAction(nameof(MyOrders));
			}
			if(order.OrderStatus == OrderStatus.OnayVerildi || order.OrderStatus == OrderStatus.Hazirlaniyor)
			{
				order.OrderStatus = OrderStatus.IptalEdildi;
				await _orderService.UpdateAsync(order);
				return RedirectToAction(nameof(MyOrders));
			}
			else
			{
				ModelState.AddModelError(string.Empty, "Sadece Onaylanmış ve Hazırlanıyor Durumundaki Siparişler İptal Edilebilir!");
				return RedirectToAction(nameof(MyOrders));
			}
		}
	}
}
