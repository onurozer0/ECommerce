using FFF.Core.Entities;
using FFF.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FFF.Web.Areas.Admin.Controllers
{
	[Area("admin")]
	[Authorize(Roles = "Yonetici")]
	public class ProductPictureController : Controller
	{
		private readonly IGenericService<ProductPicture> _productPictureService;
		private readonly IGenericService<Product> _productService;
		public ProductPictureController(IGenericService<ProductPicture> productPictureService, IGenericService<Product> productService)
		{
			_productPictureService = productPictureService;
			_productService = productService;
		}

		[Route("/admin/productpictures/{productId}")]
		public async Task<IActionResult> Index(int productId)
		{
			var product = await _productService.GetByIdAsync(productId);
			if (product == null)
			{
				ModelState.AddModelError(string.Empty, "Ürün Bulunamadı!");
				return View();
			}
			var productPictures = await _productPictureService.Where(x => x.ProductID == productId).ToListAsync();
			ViewBag.ProductId = productId;
			if (productPictures.Count > 0)
			{

				return View(productPictures);
			}

			ModelState.AddModelError(string.Empty, "Ürüne Ait Fotoğraf Bulunamadı!");
			return View();

		}
		[Route("/admin/productpictures/new/{productId}")]
		public IActionResult New(int productId)
		{
			ViewBag.ProductId = productId;
			return View();
		}
		[HttpPost, ValidateAntiForgeryToken, Route("/admin/productpictures/new/{productId}")]
		public async Task<IActionResult> New(ProductPicture model)
		{
			if (ModelState.IsValid)
			{
				if (Request.Form.Files.Any())
				{
					if (!Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", "ProductPicture")))
					{
						Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", "ProductPicture"));
					}
					string dosyaAdi = Request.Form.Files["Picture"].FileName;
					using (FileStream stream = new FileStream(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", "ProductPicture", dosyaAdi), FileMode.Create))
					{
						await Request.Form.Files["Picture"].CopyToAsync(stream);
					}
					model.Picture = "/img/ProductPicture/" + dosyaAdi;
				}
				await _productPictureService.AddAsync(model);

				return RedirectToAction("Index", new { productId = model.ProductID });
			}
			else return RedirectToAction("New");
		}
		[Route("/admin/productpictures/remove/{pictureId}")]
		public async Task<IActionResult> Remove(int pictureId)
		{
			var productPicture = await _productPictureService.GetByIdAsync(pictureId);
			if (productPicture != null)
			{
				if (!string.IsNullOrEmpty(productPicture.Picture))
				{
					string _pathFile = Directory.GetCurrentDirectory() + string.Format(@"\wwwroot") + productPicture.Picture.Replace("/", "\\");
					FileInfo fileInfo = new FileInfo(_pathFile);
					if (fileInfo.Exists) fileInfo.Delete();
					await _productPictureService.RemoveAsync(productPicture);
				}
				return RedirectToAction("Index", new { productid = productPicture.ProductID });
			}
			return RedirectToAction(nameof(Index), "Product");
		}
		[Route("/admin/productpictures/update/{pictureId}")]
		public async Task<IActionResult> Edit(int pictureId)
		{
			var productPicture = await _productPictureService.GetByIdAsync(pictureId);
			if (productPicture != null)
			{
				return View(productPicture);
			}
			ModelState.AddModelError(string.Empty, "Seçilen Resim Bulunamadı!");
			return RedirectToAction("Index", "Product");
		}
		[HttpPost, ValidateAntiForgeryToken]
		public async Task<IActionResult> Update(ProductPicture model)
		{
			if (ModelState.IsValid)
			{
				if (Request.Form.Files.Any())
				{
					if (!Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", "ProductPicture")))
					{
						Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", "ProductPicture"));
					}
					string dosyaAdi = Request.Form.Files["Picture"].FileName;
					using (FileStream stream = new FileStream(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", "ProductPicture", dosyaAdi), FileMode.Create))
					{
						await Request.Form.Files["Picture"].CopyToAsync(stream);
					}
					model.Picture = "/img/ProductPicture/" + dosyaAdi;
				}
				await _productPictureService.UpdateAsync(model);

				return RedirectToAction("Index", new { productId = model.ProductID });
			}
			else return RedirectToAction("Edit");
		}
	}
}
