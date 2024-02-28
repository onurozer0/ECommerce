using Microsoft.AspNetCore.Mvc;

namespace FFF.Web.Controllers
{
	[Route("/[controller]/{statusCode}")]
	public class ErrorController : Controller
	{
		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error(int statusCode)
		{
			return View();
		}
	}
}
