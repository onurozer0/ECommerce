using AutoMapper;
using FFF.Core.Entities;
using FFF.Core.Models;
using FFF.Core.Services;
using FFF.Core.ViewModels;
using FFF.Web.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace FFF.Web.Controllers
{
	[Authorize]

	public class MemberController : Controller
	{
		private readonly SignInManager<AppUser> _signInManager;
		private readonly IUserService _userService;
		private readonly UserManager<AppUser> _userManager;
		private readonly IGenericService<UserAddresses> _userAddressesService;
		private readonly IMapper _mapper;

		public MemberController(SignInManager<AppUser> signInManager, IUserService userService, UserManager<AppUser> userManager, IGenericService<UserAddresses> userAddressesService, IMapper mapper)
		{
			_signInManager = signInManager;
			_userService = userService;
			_userManager = userManager;
			_userAddressesService = userAddressesService;
			_mapper = mapper;
		}
		[Route("/logout")]
		public async Task Logout()
		{
			await _signInManager.SignOutAsync();
		}
		[Route("user/dashboard")]
		public IActionResult Dashboard()
		{
			return View();
		}
		[Route("user/details")]
		public async Task<IActionResult> AccountDetails()
		{
			var model = await _userService.GetMemberAccountDetailsAsync(User.Identity.Name);

			ViewBag.GenderList = new SelectList(Enum.GetNames(typeof(Gender)));
			return View(model);
		}

		[Route("user/details"), HttpPost, ValidateAntiForgeryToken]
		public async Task<IActionResult> AccountDetails(UserAccountDetailsViewModel model)
		{
			if (!ModelState.IsValid)
			{
				return View();
			}
			ViewBag.GenderList = new SelectList(Enum.GetNames(typeof(Gender)));
			var updateResult = await _userService.UpdateAccountDetailsAsync(model, User.Identity.Name);

			if (updateResult.Succeeded)
			{
				TempData["SuccessMessage"] = "Bilgileriniz Başarıyla Güncellendi!";
				return RedirectToAction();
			}
			else
			{
				ModelState.AddModelErrorList(updateResult.Errors.Select(x => x.Description).ToList());
				return View();
			}
		}

		[Route("/user/changepassword")]
		public IActionResult ChangePassword()
		{
			return View();
		}
		[Route("/user/changepassword"), ValidateAntiForgeryToken, HttpPost]
		public async Task<IActionResult> ChangePassword(PasswordChangeViewModel model)
		{
			if (!ModelState.IsValid)
			{
				return View();
			}

			var changePasswordResult = await _userService.ChangeUserPasswordAsync(User.Identity.Name, model.CurrentPassword, model.ConfirmPassword);

			if (changePasswordResult.Succeeded)
			{
				ViewBag.SuccessMessage = "Şifreniz Başarıyla Değiştirildi!";
				return View();
			}
			else
			{
				ModelState.AddModelErrorList(changePasswordResult.Errors.Select(x => x.Description).ToList());
				return View();
			}
		}
		[Route("/user/addresses")]
		public async Task<IActionResult> MyAddresses()
		{

			var currentUser = await _userManager.FindByNameAsync(User.Identity.Name);
			if (currentUser != null)
			{
				string userId = currentUser.Id;
				var addressesVM = await _userService.GetUserAddressesAsync(userId);

				if (addressesVM == null)
				{
					ModelState.AddModelError(string.Empty, "Kayıtlı Adres Bulunamadı!");
				}

				return View(addressesVM);
			}
			ModelState.AddModelError(string.Empty, "Kullanıcı Bulunamadı!");
			return View();
		}
		[Route("/user/addresses/delete")]
		public async Task<IActionResult> RemoveAddress(int addressId)
		{
			var address = await _userAddressesService.GetByIdAsync(addressId);
			if (address != null)
			{
				await _userAddressesService.RemoveAsync(address);
				return RedirectToAction(nameof(MyAddresses));
			}
			ModelState.AddModelError(string.Empty, "Seçilen Adres Bulunamadı!");
			return View(nameof(MyAddresses));
		}
		[Route("/user/addresses/add")]
		public IActionResult AddNewAddress()
		{
			ViewBag.CityList = new SelectList(Enum.GetNames(typeof(City)));
			return View();
		}
		[Route("/user/addresses/add"), HttpPost, ValidateAntiForgeryToken]
		public async Task<IActionResult> AddNewAddress(UserAddressesViewModel model)
		{
			ViewBag.CityList = new SelectList(Enum.GetNames(typeof(City)));
			if (!ModelState.IsValid)
			{
				return View();
			}
			var currentUser = await _userManager.FindByNameAsync(User.Identity.Name);
			if (currentUser != null)
			{
				var addresses = await _userAddressesService.Where(x => x.UserID == currentUser.Id).ToListAsync();
				if (addresses.Count >= 5)
				{
					ModelState.AddModelError(string.Empty, "Maksimum 5 adet adres eklenebilir!");
					return View();
				}
				var userAddress = _mapper.Map<UserAddresses>(model);
				userAddress.UserID = currentUser.Id;
				var result = await _userAddressesService.AddAsync(userAddress);
				if (result != null)
				{
					return RedirectToAction(nameof(MyAddresses));
				}
				ModelState.AddModelError(string.Empty, "Ekleme İşlemi Sırasında Bir Hata Oluştu!");
				return View();
			}
			ModelState.AddModelError(string.Empty, "Kullanıcı Bulunamadı!");
			return View();
		}
		[Route("/user/editaddres/{addressId}")]
		public async Task<IActionResult> EditAddress(int addressId)
		{
			ViewBag.CityList = new SelectList(Enum.GetNames(typeof(City)));
			var selectedAddr = await _userAddressesService.GetByIdAsync(addressId);
			if (selectedAddr != null)
			{
				var AddressVm = _mapper.Map<UserAddressesViewModel>(selectedAddr);
				return View(AddressVm);
			}
			ModelState.AddModelError(string.Empty, "Adres Bulunamadı!");
			return View();
		}
		[Route("/user/editaddres/{addressId}"), HttpPost, ValidateAntiForgeryToken]
		public async Task<IActionResult> EditAddress(UserAddressesViewModel model, int addressId)
		{
			ViewBag.CityList = new SelectList(Enum.GetNames(typeof(City)));
			if (!ModelState.IsValid)
			{
				return View();
			}
			var currentUser = await _userManager.FindByNameAsync(User.Identity.Name);
			if (currentUser != null)
			{
				var userAddress = _mapper.Map<UserAddresses>(model);
				userAddress.ID = addressId;
				userAddress.UserID = currentUser.Id;
				await _userAddressesService.UpdateAsync(userAddress);
				return RedirectToAction(nameof(MyAddresses));
			}
			ModelState.AddModelError(string.Empty, "Kullanıcı Bulunamadı!");
			return View();
		}
		public IActionResult AccessDenied()
		{
			return View();
		}
	}
}
