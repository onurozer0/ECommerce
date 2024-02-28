using AutoMapper;
using FFF.Core.Entities;
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

	public class CategoryController : Controller
	{
		private readonly IGenericService<Category> _categoriesGeneric;
		private readonly IGenericRepository<Category> _categoriesRepository;
		private readonly IGenericService<Product> _productService;

		public CategoryController(IMapper mapper, IGenericService<Category> categoriesGeneric, IGenericRepository<Category> categoriesRepository, IGenericService<Product> productService)
		{
			_categoriesGeneric = categoriesGeneric;
			_categoriesRepository = categoriesRepository;
			_productService = productService;
		}

		[Route("/admin/categories")]
		public async Task<IActionResult> Index()
		{
			var categories = await _categoriesRepository.GetAll().Include(x => x.ParentCategory).ToListAsync();
			if (categories.Count() == 0)
			{
				ModelState.AddModelError(string.Empty, "Kategori Bulunamadı!");
				return View();
			}
			return View(categories);
		}
		[Route("/admin/categories/remove/{catId}")]
		public async Task<IActionResult> Remove(int catId)
		{
			var category = await _categoriesGeneric.GetByIdAsync(catId);
			var products = await _productService.Where(x => x.CategoryId == catId).ToListAsync();
			if (products.Count > 0)
			{
				await _productService.RemoveRangeAsync(products);
			}
			if (category != null)
			{
				if (category.ParentID == null)
				{
					var subCategories = await _categoriesGeneric.Where(x => x.ParentID == category.ID).ToListAsync();
					foreach (var subCategory in subCategories)
					{
						subCategory.ParentID = null;
					}
				}
				await _categoriesGeneric.RemoveAsync(category);
				return RedirectToAction(nameof(Index));
			}
			ModelState.AddModelError(string.Empty, "Seçilen Kategori Bulunamadı!");
			return View(nameof(Index));
		}
		[Route("/admin/categories/add")]
		public async Task<IActionResult> Insert()
		{
			CategoryViewModel categoryVM = new CategoryViewModel()
			{
				Category = new Category(),
				Categories = await _categoriesGeneric.Where(x => x.ParentID == null).ToListAsync(),
			};
			return View(categoryVM);
		}
		[HttpPost, ValidateAntiForgeryToken, Route("/admin/categories/add")]
		public async Task<IActionResult> Insert(CategoryViewModel model)
		{
			if (!ModelState.IsValid)
			{
				return RedirectToAction("Insert");
			}
			await _categoriesGeneric.AddAsync(model.Category);

			return RedirectToAction("Index");
		}
		[Route("/admin/categories/edit/{catId}")]
		public async Task<IActionResult> Update(int catId)
		{
			var category = await _categoriesGeneric.GetByIdAsync(catId);
			if (category != null)
			{
				CategoryViewModel categoryVM = new CategoryViewModel
				{
					Category = category,
					Categories = await _categoriesGeneric.Where(x => x.ParentID == null && x.ID != category.ID).ToListAsync()
				};
				return View(categoryVM);
			}
			return RedirectToAction(nameof(Index));
		}
		[Route("/admin/categories/edit"), HttpPost, ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(CategoryViewModel model)
		{
			if (!ModelState.IsValid)
			{
				model.Category = await _categoriesGeneric.GetByIdAsync(model.Category.ID);
				model.Categories = await _categoriesGeneric.Where(x => x.ParentID == null && x.ID != model.Category.ID).ToListAsync();
				return View("Update", model);
			}
			await _categoriesGeneric.UpdateAsync(model.Category);
			return RedirectToAction(nameof(Index));
		}
	}
}
