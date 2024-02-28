using FFF.Core.Entities;
using FFF.Core.Services;
using FFF.Core.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FFF.Web.Areas.Admin.Controllers
{
	[Area("admin")]
	[Authorize(Roles = "Yonetici")]

	public class ProductController : Controller
	{
		private readonly IGenericService<Product> _productService;
		private readonly IGenericService<Category> _categoriesService;
		private readonly IGenericService<Brand> _brandsService;

		public ProductController(IGenericService<Product> productService, IGenericService<Category> categoriesService, IGenericService<Brand> brandsService)
		{
			_productService = productService;
			_categoriesService = categoriesService;
			_brandsService = brandsService;
		}
		[Route("/admin/products")]
		public async Task<IActionResult> Index()
		{
			var products = await _productService.GetAllAsync();
			if (products.Count() == 0)
			{
				ModelState.AddModelError(string.Empty, "Ürün Bulunamadı!");
				return View();
			}
			return View(products);
		}
		[Route("/admin/products/remove/{productId}")]
		public async Task<IActionResult> Remove(int productId)
		{
			var product = await _productService.GetByIdAsync(productId);
			if (product != null)
			{
				await _productService.RemoveAsync(product);
				return RedirectToAction(nameof(Index));
			}
			ModelState.AddModelError(string.Empty, "Seçilen Ürün Bulunamadı!");
			return View(nameof(Index));
		}
		[Route("/admin/products/add")]
		public async Task<IActionResult> New()
		{
			ProductViewModel productVm = new ProductViewModel
			{
				Product = new Product(),
				Brands = await _brandsService.GetAllAsync(),
				Categories = await _categoriesService.GetAllAsync(),
			};
			return View(productVm);
		}
		[HttpPost, ValidateAntiForgeryToken, Route("/admin/products/add")]
		public async Task<IActionResult> Insert(ProductViewModel model)
		{
			if (!ModelState.IsValid)
			{
				return RedirectToAction(nameof(New));
			}
			await _productService.AddAsync(model.Product);

			return RedirectToAction("Index");
		}
		[Route("/admin/products/edit/{productId}")]
		public async Task<IActionResult> Update(int productId)
		{
			var product = await _productService.GetByIdAsync(productId);
			if (product != null)
			{
				ProductViewModel productVm = new ProductViewModel
				{
					Product = product,
					Brands = await _brandsService.GetAllAsync(),
					Categories = await _categoriesService.GetAllAsync(),
				};
				return View(productVm);
			}
			return RedirectToAction(nameof(Index));
		}
		[Route("/admin/products/edit"), HttpPost, ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(ProductViewModel model)
		{
			if (!ModelState.IsValid)
			{
				model.Product = await _productService.GetByIdAsync(model.Product.ID);
				model.Categories = await _categoriesService.GetAllAsync();
				model.Brands = await _brandsService.GetAllAsync();
				return View("Update", model);
			}
			await _productService.UpdateAsync(model.Product);
			return RedirectToAction(nameof(Index));
		}
	}
}
