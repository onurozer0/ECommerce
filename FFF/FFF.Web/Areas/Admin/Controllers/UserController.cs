using AutoMapper;
using FFF.Core.Entities;
using FFF.Core.Services;
using FFF.Core.UnitOfWorks;
using FFF.Core.ViewModels;
using FFF.Web.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FFF.Web.Areas.Admin.Controllers
{
	[Area("admin")]
	[Authorize(Roles = "Yonetici")]
	public class UserController : Controller
	{
		private readonly IUserService _userService;
		private readonly UserManager<AppUser> _userManager;
		private readonly IMapper _mapper;
		private readonly IUnitOfWork _unitOfWork;
		public UserController(IUserService userService, IMapper mapper, UserManager<AppUser> userManager, IUnitOfWork unitOfWork)
		{
			_userService = userService;
			_mapper = mapper;
			_userManager = userManager;
			_unitOfWork = unitOfWork;
		}
		[Route("/admin/users")]
		public async Task<IActionResult> Index()
		{
			var userVmList = await _userService.GetUserListAsync();
			if (userVmList.Count != 0)
			{
				return View(userVmList);
			}

			ModelState.AddModelError(string.Empty, "Kullanıcı Bulunamadı!");
			return View();

		}
		[Route("/admin/users/edit/{userId}")]

		public async Task<IActionResult> Edit(string userId)
		{
			var user = await _userManager.FindByIdAsync(userId);
			if (user != null)
			{
				var userVm = _mapper.Map<UserViewModel>(user);
				return View(userVm);
			}
			return RedirectToAction(nameof(Index));
		}

		[Route("/admin/users/edit/{userId}"), HttpPost, ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(string userId, UserViewModel model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}
			var currentUser = await _userManager.FindByIdAsync(userId);

			if (currentUser != null)
			{
				currentUser = _mapper.Map(model, currentUser);

				var result = await _userManager.UpdateAsync(currentUser);
				await _userManager.UpdateSecurityStampAsync(currentUser);


				if (result.Succeeded)
				{
					return RedirectToAction(nameof(Index));
				}
				ModelState.AddModelErrorList(result.Errors.Select(x => x.Description).ToList());
				return View(model);

			}
			ModelState.AddModelError(string.Empty, "Kullanıcı Bulunamadı!");
			return View(model);
		}
		[Route("/admin/users/remove/{userId}")]
		public async Task<IActionResult> Remove(string userId)
		{
			var selectedUser = await _userManager.FindByIdAsync(userId);
			if (selectedUser != null)

			{
				var result = await _userManager.DeleteAsync(selectedUser);
				if (result.Succeeded)
				{
					return RedirectToAction(nameof(Index));
				}
				ModelState.AddModelErrorList(result.Errors.Select(x => x.Description).ToList());
				return RedirectToAction(nameof(Index));
			}
			ModelState.AddModelError(string.Empty, "Kullanıcı Bulunamadı!");
			return RedirectToAction(nameof(Index));
		}
	}
}
