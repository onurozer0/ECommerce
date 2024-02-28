using FFF.Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FFF.Web.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize(Roles = "Yonetici")]

	public class HomeController : Controller
	{
		private readonly SignInManager<AppUser> _signInManager;

		public HomeController(SignInManager<AppUser> signInManager)
		{
			_signInManager = signInManager;
		}

		[Route("/admin")]
		public IActionResult Index()
		{
			return View();
		}
		[Route("/admin/logout")]
		public async Task<IActionResult> Logout()
		{
			await _signInManager.SignOutAsync();
			return Redirect("/");
		}
	}
}
