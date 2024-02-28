using FFF.Core.Entities;
using FFF.Core.ViewModels;
using FFF.Web.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FFF.Web.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize(Roles = "Yonetici")]
	public class RoleController : Controller
	{
		private readonly UserManager<AppUser> _userManager;
		private readonly RoleManager<AppRole> _roleManager;
		public RoleController(RoleManager<AppRole> roleManager = null, UserManager<AppUser> userManager = null)
		{
			_roleManager = roleManager;
			_userManager = userManager;
		}
		[Route("/admin/roles")]
		public async Task<IActionResult> Index()
		{
			var roles = await _roleManager.Roles.Select(x => new RoleViewModel()
			{
				ID = x.Id,
				Name = x.Name,
			}).ToListAsync();
			if (roles != null)
			{
				return View(roles);
			}
			ModelState.AddModelError(string.Empty, "Rol Bulunamadı!");
			return View();
		}
		[Route("/admin/roles/create")]
		public IActionResult CreateRole()
		{
			return View();
		}
		[Route("/admin/roles/create"), HttpPost, ValidateAntiForgeryToken]
		public async Task<IActionResult> CreateRole(CreateRoleViewModel model)
		{
			if (!ModelState.IsValid)
			{
				return View();
			}

			var result = await _roleManager.CreateAsync(new AppRole()
			{
				Name = model.Name,
			});
			if (!result.Succeeded)
			{
				ModelState.AddModelErrorList(result.Errors.Select(x => x.Description).ToList());
				return View();
			}
			return RedirectToAction(nameof(Index));
		}
		[Route("/admin/roles/update/{roleId}")]
		public async Task<IActionResult> Update(string roleId)
		{
			var role = await _roleManager.FindByIdAsync(roleId);
			if (role != null)
			{
				return View(new RoleUpdateViewModel()
				{
					ID = role.Id,
					Name = role.Name,
				});
			}
			return RedirectToAction(nameof(Index));

		}
		[Route("/admin/roles/update/{roleId}"), HttpPost, ValidateAntiForgeryToken]
		public async Task<IActionResult> Update(RoleUpdateViewModel model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}
			var role = await _roleManager.FindByIdAsync(model.ID);
			if (role != null)
			{
				role.Name = model.Name;
				await _roleManager.UpdateAsync(role);
			}
			return RedirectToAction(nameof(Index));
		}
		[Route("/admin/roles/remove/{roleId}")]
		public async Task<IActionResult> Remove(string roleId)
		{
			var role = await _roleManager.FindByIdAsync(roleId);
			if (role != null)
			{
				var result = await _roleManager.DeleteAsync(role);
				if (!result.Succeeded)
				{
					throw new Exception(result.Errors.Select(x => x.Description).First());
				}
			}
			return RedirectToAction(nameof(Index));
		}
		[Route("/admin/roles/touser/{userId}")]
		public async Task<IActionResult> AssignRoleToUser(string userId)
		{
			var user = await _userManager.FindByIdAsync(userId);
			if (user == null)
			{
				return RedirectToAction(nameof(UserController.Index), "User");
			}
			var roles = await _roleManager.Roles.ToListAsync();
			var roleViewModelList = new List<AssignRoleToUserViewModel>();
			var userRoles = await _userManager.GetRolesAsync(user);
			foreach (var role in roles)
			{
				var assignRoleToUserVm = new AssignRoleToUserViewModel()
				{
					RoleId = role.Id,
					Name = role.Name,
				};
				if (userRoles.Contains(role.Name))
				{
					assignRoleToUserVm.isExist = true;
				}
				roleViewModelList.Add(assignRoleToUserVm);
			}
			ViewBag.UserId = userId;
			return View(roleViewModelList);
		}
		[Route("/admin/roles/touser/{userId}"), HttpPost, ValidateAntiForgeryToken]
		public async Task<IActionResult> AssignRoleToUser(string userId, List<AssignRoleToUserViewModel> requestList)
		{
			var user = await _userManager.FindByIdAsync(userId);

			foreach (var role in requestList)
			{
				if (role.isExist)
				{
					await _userManager.AddToRoleAsync(user, role.Name);
				}
				if (!role.isExist)
				{
					await _userManager.RemoveFromRoleAsync(user, role.Name);
				}
			}
			return RedirectToAction(nameof(Index), "User");
		}
	}
}
