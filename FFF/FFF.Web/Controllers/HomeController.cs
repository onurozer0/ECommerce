using AutoMapper;
using FFF.Core.Entities;
using FFF.Core.Repositories;
using FFF.Core.Services;
using FFF.Core.ViewModels;
using FFF.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace FFF.Web.Controllers
{
	public class HomeController : Controller
	{
		private readonly IGenericService<ContactMessages> _contactService;
		private readonly IGenericRepository<Product> _productRepo;
		private readonly IGenericRepository<ProductPicture> _productPictureRepo;
		private readonly IGenericRepository<Category> _categoryRepo;
		private readonly IGenericRepository<Slide> _slideRepo;
		private readonly IMapper _mapper;
		public HomeController(IGenericService<ContactMessages> contactService, IMapper mapper, IGenericRepository<Product> productRepo, IGenericRepository<Category> categoryRepo, IGenericRepository<Slide> slideRepo, IGenericRepository<ProductPicture> productPictureRepo)
		{
			_contactService = contactService;
			_mapper = mapper;
			_productRepo = productRepo;
			_categoryRepo = categoryRepo;
			_slideRepo = slideRepo;
			_productPictureRepo = productPictureRepo;
		}
		[Route("/")]
		public async Task<IActionResult> Index()
		{
			var products = await _productRepo.GetAll().Where(x => x.UnitsInStock > 0).Include(x => x.ProductPicture).Include(x => x.Category).OrderBy(x => Guid.NewGuid()).Take(9).ToListAsync();
			var categories = await _categoryRepo.GetAll().ToListAsync();
			var slides = await _slideRepo.GetAll().OrderBy(x => x.DisplayIndex).ToListAsync();
			IndexViewModel indexVm = new()
			{
				Products = products,
				Categories = categories,
				Slides = slides
			};
			return View(indexVm);
		}
		[Route("/category/{catId}")]
		public async Task<IActionResult> CategoryProducts(int catId)
		{
			var category = await _categoryRepo.Where(x => x.ID == catId).Include(x => x.SubCategories).FirstAsync();
			var products = await _productRepo.GetAll().Where(x => x.CategoryId == catId).Where(x => x.UnitsInStock > 0).Include(x => x.ProductPicture).Include(x => x.Category).ToListAsync();
			if (category.SubCategories != null)
			{
				var subCategories = await _categoryRepo.Where(x => x.ParentID == category.ID).Include(x => x.Products).ToListAsync();
				foreach (var subCategory in subCategories)
				{
					if (subCategory.Products != null)
					{
						foreach (var product in subCategory.Products)
						{
							product.ProductPicture = await _productPictureRepo.Where(x => x.ProductID == product.ID).ToListAsync();
							products.Add(product);
						}
					}
				}
			}
			ViewBag.Title = category.Name;
			return View(products);
		}

		[Route("/hakkimizda")]
		public IActionResult AboutUs()
		{
			return View();
		}
		[Route("/contact")]
		public IActionResult ContactUs()
		{
			return View();
		}
		[Route("/contact"), HttpPost, ValidateAntiForgeryToken]
		public async Task<IActionResult> ContactUs(ContactMessagesViewModel model)
		{
			if (!ModelState.IsValid)
			{
				return View();
			}
			var cm = _mapper.Map<ContactMessages>(model);
			cm.CreatedDate = DateTime.Now;

			await _contactService.AddAsync(cm);
			TempData["SuccessMessage"] = "Mesajýnýz Baþarýyla Gönderildi";
			return RedirectToAction();
		}
	}
}
