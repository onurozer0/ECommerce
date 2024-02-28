using FFF.Core.Entities;
using FFF.Core.Repositories;
using FFF.Core.Services;
using FFF.Core.ViewModels;
using FFF.Web.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FFF.Web.Controllers
{
	public class UserController : Controller
	{
		private readonly IUserRepository _userRepository;
		private readonly IUserService _userService;
		private readonly UserManager<AppUser> _userManager;
		private readonly IEmailService _emailService;

		public UserController(IUserRepository userRepository, IUserService userService, UserManager<AppUser> userManager, IEmailService emailService)
		{
			_userRepository = userRepository;
			_userService = userService;
			_userManager = userManager;
			_emailService = emailService;
		}

		[Route("signup")]
		public IActionResult SignUp()
		{
			if (User.Identity.IsAuthenticated)
			{
				return RedirectToAction(nameof(HomeController.Index), "Home");
			}
			return View();
		}

		[Route("signup"), HttpPost, ValidateAntiForgeryToken]
		public async Task<IActionResult> SignUp(SignUpViewModel model, string returnUrl = null)
		{
			returnUrl = returnUrl ?? Url.Action(nameof(HomeController.Index), "Home");
			if (!ModelState.IsValid)
			{
				return View();
			}
			var identityResult = await _userService.RegisterUserAsync(model);

			if (identityResult.Succeeded)
			{
				ViewBag.SuccessMessage = "Kayıt İşlemi Başarılı, Yönlendiriliyorsunuz";
				await _userService.SignInAsync(model, returnUrl);
				return Redirect(returnUrl);
			}
			ModelState.AddModelErrorList(identityResult.Errors.Select(x => x.Description).ToList());
			return View();
		}


		[Route("signin")]
		public IActionResult SignIn()
		{
			if (User.Identity.IsAuthenticated)
			{
				return RedirectToAction(nameof(HomeController.Index), "Home");
			}
			return View();
		}


		[Route("signin"), HttpPost, ValidateAntiForgeryToken]
		public async Task<IActionResult> SignIn(SignInViewModel model, string returnUrl = null)
		{
			if (!ModelState.IsValid)
			{
				return View();
			}
			var currentUser = await _userManager.FindByEmailAsync(model.Email);
			if(currentUser == null)
			{
				ModelState.AddModelError(string.Empty, "E-Posta Veya Parola Hatalı!");
				return View();
			}
			if (currentUser.isBanned)
			{
				ModelState.AddModelError(string.Empty, "Hesabınız Aktif Durumda Değildir, Yöneticiyle İletişime Geçiniz!");
				return View();
			}
			returnUrl = returnUrl ?? Url.Action(nameof(HomeController.Index), "Home");
			var result = await _userService.SignInAsync(model, returnUrl);


			if (result.Succeeded)
			{
				ViewBag.SuccessMessage = "Giriş İşlemi Başarılı, Yönlendiriliyorsunuz";
				return Redirect(returnUrl);
			}

			if (result.IsLockedOut)
			{
				ModelState.AddModelErrorList(new List<string>()
			{
				"Çok fazla başarısız giriş denemesi yapıldığı için 15 dakika boyunca hesabınız kilitlenmiştir.Giriş Yapılamaz!"
			});
				return View();
			}


			ModelState.AddModelErrorList(new List<string>()
			{
				"E-Posta Veya Parola Hatalı!"
			});
			return View();
		}
		[Route("forgotpassword")]
		public IActionResult ForgotPassword()
		{
			if (User.Identity.IsAuthenticated)
			{
				return RedirectToAction(nameof(HomeController.Index), "Home");
			}
			return View();
		}
		[Route("forgotpassword"), HttpPost, ValidateAntiForgeryToken]
		public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
		{
			if (!ModelState.IsValid)
			{
				return View();
			}
			var isUserCount = await _userManager.FindByEmailAsync(model.Email);

			if (isUserCount == null)
			{
				ModelState.AddModelError(string.Empty, "Bu E-Posta Adresine Sahip Kullanıcı Bulunamamıştır.");
				return View();
			}
			string resetToken = await _userManager.GeneratePasswordResetTokenAsync(isUserCount);

			var resetUrl = Url.Action("ResetPassword", "User", new { userId = isUserCount.Id, Token = resetToken }, HttpContext.Request.Scheme);

			await _emailService.SendPwdResetMail(resetUrl, isUserCount.Email);

			TempData["Success"] = "E-Posta Başarıyla Gönderildi!";
			return RedirectToAction(nameof(ForgotPassword));
		}
		[Route("resetpassword")]
		public IActionResult ResetPassword(string userId, string token)
		{
			if (User.Identity.IsAuthenticated)
			{
				return RedirectToAction(nameof(HomeController.Index), "Home");
			}
			TempData["UserId"] = userId;
			TempData["Token"] = token;

			return View();
		}
		[Route("resetpassword"), HttpPost, ValidateAntiForgeryToken]
		public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
		{
			if (!ModelState.IsValid)
			{
				return View();
			}

			var userId = TempData["UserId"];
			var token = TempData["Token"];
			if (userId == null || token == null)
			{
				throw new Exception("Bir Hata Meydana Geldi");
			}

			var resetPasswordResult = await _userService.ResetPassword(userId.ToString(), token.ToString(), model.ConfirmPassword);

			if (resetPasswordResult.Succeeded)
			{
				TempData["Success"] = "Şifreniz Başarıyla Sıfırlanmıştır!";
				return View();
			}
			else
			{
				ModelState.AddModelErrorList(resetPasswordResult.Errors.Select(x => x.Description).ToList());
				TempData["UserId"] = userId.ToString();
				TempData["Token"] = token.ToString();
				return View();
			}
		}
	}
}
