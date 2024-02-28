using FFF.Core.Entities;
using FFF.Core.Models;
using FFF.Core.Repositories;
using FFF.Core.Services;
using FFF.Core.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FFF.Web.Areas.Admin.Controllers
{
	[Area("admin")]
	[Authorize(Roles = "Yonetici")]
	public class OrdersController : Controller
	{
		private readonly IGenericRepository<Order> _orderRepository;
		private readonly IGenericRepository<OrderDetails> _orderDetailsRepository;
		private readonly IGenericService<Order> _orderService;

		public OrdersController(IGenericRepository<Order> orderRepository, IGenericRepository<OrderDetails> orderDetailsRepository, IGenericService<Order> orderService)
		{
			_orderRepository = orderRepository;
			_orderDetailsRepository = orderDetailsRepository;
			_orderService = orderService;
		}

		[Route("/admin/orders")]
		public async Task<IActionResult> Index()
		{
			var orders = await _orderRepository.GetAll().Include(x => x.User).Include(x => x.OrderDetails).ToListAsync();
			foreach (var order in orders)
			{
				var orderDetails = await _orderDetailsRepository.Where(x => x.OrderID == order.ID).ToListAsync();
				decimal totalFee = 0;
				foreach (var od in orderDetails)
				{
					totalFee += od.ProductPrice * od.Quantity;
				}
				order.TotalFee = totalFee;
			}
			var myOrdersVm = new MyOrdersViewModel()
			{
				Orders = orders,
			};
			return View(myOrdersVm);
		}
		[Route("/admin/orders/remove/{orderId}")]
		public async Task<IActionResult> Remove(int orderId)
		{
			var order = await _orderRepository.GetByIdAsync(orderId);
			if (order != null)
			{
				await _orderService.RemoveAsync(order);
			}
			return RedirectToAction(nameof(Index));
		}
		[Route("/admin/orders/cancel/{orderId}")]
		public async Task<IActionResult> Cancel(int orderId)
		{
			var order = await _orderRepository.GetByIdAsync(orderId);
			if (order != null)
			{
				order.OrderStatus = OrderStatus.IptalEdildi;
				await _orderService.UpdateAsync(order);
			}
			return RedirectToAction(nameof(Index));
		}
		[Route("/admin/orders/detail/{orderId}")]
		public async Task<IActionResult> Details(int orderId)
		{
			var order = await _orderRepository.Where(x => x.ID == orderId).Include(x => x.User).Include(x => x.OrderDetails).FirstAsync();
			if (order != null)
			{
				var orderDetails = order.OrderDetails.ToList();
				for (int i = 0; i < order.OrderDetails.Count; i++)
				{
					var orderDetail = orderDetails[i];
					orderDetail = await _orderDetailsRepository.Where(x => x.ID == orderDetail.ID).Include(x => x.Product).FirstAsync();
				}
				order.OrderDetails = orderDetails;
				return View(order);
			}
			return RedirectToAction(nameof(Index));

		}
		[Route("/admin/orders/detail/{orderId}"), HttpPost, ValidateAntiForgeryToken]
		public async Task<IActionResult> Details(Order model)
		{
			var order = await _orderService.GetByIdAsync(model.ID);
			order.OrderStatus = model.OrderStatus;
			await _orderService.UpdateAsync(order);
			return RedirectToAction(nameof(Index));
		}
	}
}
