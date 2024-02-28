using FFF.Core.Entities;
using FFF.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FFF.Web.Areas.Admin.Controllers
{
	[Area("admin")]
	[Authorize(Roles = "Yonetici")]
	public class SlideController : Controller
	{
		private readonly IGenericService<Slide> _slideService;
		public SlideController(IGenericService<Slide> slideService)
		{
			_slideService = slideService;
		}
		[Route("/admin/slides")]
		public async Task<IActionResult> Index()
		{
			return View(await _slideService.GetAllAsync());
		}
		[Route("/admin/slides/new")]
		public IActionResult New()
		{
			return View();
		}
		[HttpPost, ValidateAntiForgeryToken, Route("/admin/slides/new")]
		public async Task<IActionResult> Insert(Slide model)
		{
			if (!ModelState.IsValid)
			{
				return RedirectToAction(nameof(New));
			}
			if (!Request.Form.Files.Any())
			{
				return RedirectToAction(nameof(New));
			}
			if (!Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", "slide")))
			{
				Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", "slide"));
			}
			string fileName = Request.Form.Files["Picture"].FileName;
			using (FileStream stream = new(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", "slide", fileName), FileMode.Create))
			{
				await Request.Form.Files["Picture"].CopyToAsync(stream);
			}
			model.Picture = "/img/slide/" + fileName;
			await _slideService.AddAsync(model);
			return RedirectToAction(nameof(Index));
		}
		[Route("/admin/slides/update/{slideId}")]
		public async Task<IActionResult> Update(int slideId)
		{

			var slide = await _slideService.GetByIdAsync(slideId);

			if (slide != null)
			{
				return View(slide);
			}
			return RedirectToAction(nameof(Index));
		}
		[HttpPost, ValidateAntiForgeryToken, Route("/admin/slides/update/{slideId}")]
		public async Task<IActionResult> Update(Slide model)
		{
			if (!ModelState.IsValid)
			{
				model = await _slideService.GetByIdAsync(model.ID);
				return View(model);
			}
			if (Request.Form.Files.Any())
			{
				if (!Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", "slide")))
				{
					Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", "slide"));
				}
				string fileName = Request.Form.Files["Picture"].FileName;
				using (FileStream stream = new(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", "slide", fileName), FileMode.Create))
				{
					await Request.Form.Files["Picture"].CopyToAsync(stream);
				}
				model.Picture = "/img/slide/" + fileName;

			}
			await _slideService.UpdateAsync(model);
			return RedirectToAction(nameof(Index));

		}


		[Route("/admin/slides/remove/{slideId}")]
		public async Task<IActionResult> Remove(int slideId)
		{
			var slide = await _slideService.GetByIdAsync(slideId);
			if (slide != null)
			{
				if (!string.IsNullOrEmpty(slide.Picture))
				{
					string filePath = Directory.GetCurrentDirectory() + string.Format(@"\wwwroot") + slide.Picture.Replace("/", "\\");
					var fileInfo = new FileInfo(filePath);
					if (fileInfo.Exists)
					{
						fileInfo.Delete();
					}
					await _slideService.RemoveAsync(slide);
				}
			}
			return RedirectToAction(nameof(Index));
		}
	}
}
