using FFF.Core.Entities;
using FFF.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FFF.Web.Areas.Admin.Controllers
{
	[Area("admin")]
	[Authorize(Roles = "Yonetici")]
	public class BrandController : Controller
	{
		private readonly IGenericService<Brand> _brandService;

		public BrandController(IGenericService<Brand> brandService)
		{
			_brandService = brandService;
		}

		[Route("/admin/brands")]
		public async Task<IActionResult> Index()
		{
			var brands = await _brandService.GetAllAsync();
			if (brands.Count() == 0)
			{
				ModelState.AddModelError(string.Empty, "Marka Bulunamadı!");
				return View();
			}
			return View(brands);
		}
		[Route("/admin/brands/remove/{brandId}")]
		public async Task<IActionResult> Remove(int brandId)
		{
			var brand = await _brandService.GetByIdAsync(brandId);
			if (brand != null)
			{
				await _brandService.RemoveAsync(brand);
				return RedirectToAction(nameof(Index));
			}
			ModelState.AddModelError(string.Empty, "Seçilen Marka Bulunamadı!");
			return View(nameof(Index));
		}
		[Route("/admin/brands/new")]
		public IActionResult Insert()
		{
			return View();
		}
		[Route("/admin/brands/new"), HttpPost, ValidateAntiForgeryToken]
		public async Task<IActionResult> Insert(Brand model)
		{
			if (!ModelState.IsValid)
			{
				return View();
			}
			await _brandService.AddAsync(model);

			return RedirectToAction("Index");
		}
		[Route("/admin/brands/edit/"), HttpPost, ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(Brand model)
		{
			if (!ModelState.IsValid)
			{
				return View("Update", model);
			}
			await _brandService.UpdateAsync(model);
			return RedirectToAction(nameof(Index));
		}
		[Route("/admin/brands/edit/{brandId}")]
		public async Task<IActionResult> Update(int brandId)
		{
			var brand = await _brandService.GetByIdAsync(brandId);
			if (brand != null)
			{
				return View(brand);
			}
			return RedirectToAction(nameof(Index));
		}

	}
}
