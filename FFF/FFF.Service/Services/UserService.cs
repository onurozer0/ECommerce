using AutoMapper;
using FFF.Core.Entities;
using FFF.Core.Services;
using FFF.Core.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FFF.Service.Services
{
	public class UserService : IUserService
	{
		private readonly UserManager<AppUser> _userManager;
		private readonly SignInManager<AppUser> _signInManager;
		private readonly IHttpContextAccessor _httpContextAccessor;
		private readonly IGenericService<UserAddresses> _addressesGeneric;
		private readonly IMapper _mapper;

		public UserService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IHttpContextAccessor httpContextAccessor, IMapper mapper, IGenericService<UserAddresses> addressesGeneric)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_httpContextAccessor = httpContextAccessor;
			_mapper = mapper;
			_addressesGeneric = addressesGeneric;
		}

		public async Task<IdentityResult> ChangeUserPasswordAsync(string currentUsername, string currentPassword, string newPassword)
		{
			var currentUser = await _userManager.FindByNameAsync(currentUsername);
			if (currentUser != null)
			{
				var checkOldPassword = await _userManager.CheckPasswordAsync(currentUser, currentPassword);
				if (!checkOldPassword)
				{
					return IdentityResult.Failed(new IdentityError
					{
						Code = "WrongPassword",
						Description = "Şu anki şifreniz yanlış!"
					});
				}

				var result = await _userManager.ChangePasswordAsync(currentUser, currentPassword, newPassword);
				if (result.Succeeded)
				{
					await _userManager.UpdateSecurityStampAsync(currentUser);
					await _signInManager.SignOutAsync();
					await _signInManager.PasswordSignInAsync(currentUser, newPassword, true, false);
					return result;
				}
				else
				{
					return result;
				}
			}
			else
			{
				return IdentityResult.Failed(new IdentityError
				{
					Code = "UserNotFound",
					Description = "Kullanıcı Bulunamadı!"
				});
			}
		}

		public async Task<UserAccountDetailsViewModel> GetMemberAccountDetailsAsync(string userName)
		{
			var currentUser = await _userManager.FindByNameAsync(userName);

			var userViewModel = new UserAccountDetailsViewModel
			{
				Email = currentUser!.Email!,
				PhoneNumber = currentUser.PhoneNumber!,
				FirstName = currentUser.Name,
				LastName = currentUser.Surname,
				DateOfBirth = currentUser.DateOfBirth,
				Gender = currentUser.Gender
			};

			return userViewModel;
		}

		public async Task<SignInResult> SignInAsync(SignInViewModel model, string? returnUrl = null)
		{
			var isUserValid = await _userManager.FindByEmailAsync(model.Email);

			if (isUserValid == null)
			{
				return SignInResult.Failed;
			}
			if (isUserValid.isBanned)
			{
				return SignInResult.Failed;
			}

			var result = await _signInManager.PasswordSignInAsync(isUserValid, model.Password, model.RememberMe, true);
			var ipAddress = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress;
			isUserValid.LastLoginIP = ipAddress.ToString();
			isUserValid.LastLoginDate = DateTime.Now;
			await _userManager.UpdateAsync(isUserValid);
			return result;
		}
		public async Task<SignInResult> SignInAsync(SignUpViewModel model, string? returnUrl = null)
		{
			var isUserValid = await _userManager.FindByEmailAsync(model.Email);

			if (isUserValid == null)
			{
				return SignInResult.Failed;
			}

			var result = await _signInManager.PasswordSignInAsync(isUserValid, model.Password, false, true);
			var ipAddress = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress;
			isUserValid.LastLoginIP = ipAddress.ToString();
			isUserValid.LastLoginDate = DateTime.Now;
			await _userManager.UpdateAsync(isUserValid);
			return result;
		}
		public async Task<IdentityResult> RegisterUserAsync(SignUpViewModel model)
		{
			var user = new AppUser
			{
				UserName = model.UserName,
				Email = model.Email,
				PhoneNumber = model.PhoneNumber,
				Name = model.Name,
				Surname = model.Surname,
			};

			return await _userManager.CreateAsync(user, model.ConfirmPassword);
		}

		public async Task<IdentityResult> ResetPassword(string userId, string token, string newPassword)
		{
			var user = await _userManager.FindByIdAsync(userId);
			if (user != null)
			{
				var result = await _userManager.ResetPasswordAsync(user, token, newPassword);
				if (result.Succeeded)
				{
					await _userManager.UpdateSecurityStampAsync(user);
					return result;
				}
				else
				{
					return result;
				}
			}
			else
			{
				return IdentityResult.Failed(new IdentityError
				{
					Code = "UserNotFound",
					Description = "Kullanıcı bulunamadı!"
				});
			}
		}

		public async Task<IdentityResult> UpdateAccountDetailsAsync(UserAccountDetailsViewModel model, string userName)
		{
			var currentUser = await _userManager.FindByNameAsync(userName);
			if (currentUser != null)
			{
				var checkOldPassword = await _userManager.CheckPasswordAsync(currentUser, model.PasswordConfirm);
				if (!checkOldPassword)
				{
					return IdentityResult.Failed(new IdentityError
					{
						Code = "WrongPwd",
						Description = "Şifre Doğrulanamadı!"
					});
				}
				currentUser.Name = model.FirstName;
				currentUser.Email = model.Email;
				currentUser.DateOfBirth = model.DateOfBirth;
				currentUser.PhoneNumber = model.PhoneNumber;
				currentUser.Surname = model.LastName;
				currentUser.Gender = model.Gender;
				var updateResult = await _userManager.UpdateAsync(currentUser);
				if (!updateResult.Succeeded)
				{
					return IdentityResult.Failed(new IdentityError
					{
						Code = "ProcessFailed",
						Description = "İşlem Sırasında Bir Hata Oluştu!"
					});
				}
				await _userManager.UpdateSecurityStampAsync(currentUser);
				await _signInManager.SignOutAsync();
				await _signInManager.SignInAsync(currentUser, true);
				return IdentityResult.Success;
			}
			return IdentityResult.Failed(new IdentityError
			{
				Code = "UserNotFound",
				Description = "Kullanıcı Bulunamadı!"
			});
		}

		public async Task<List<UserAddressesViewModel>> GetUserAddressesAsync(string userID)
		{
			var addresses = await _addressesGeneric.Where(x => x.UserID == userID).ToListAsync();
			if (addresses.Count == 0)
			{
				return null;
			}

			var addressesVM = _mapper.Map<List<UserAddressesViewModel>>(addresses);
			return addressesVM;
		}

		public async Task<List<UserViewModel>> GetUserListAsync()
		{
			var userList = await _userManager.Users.ToListAsync();

			var userVmList = _mapper.Map<List<UserViewModel>>(userList);
			return userVmList;
		}
	}
}
