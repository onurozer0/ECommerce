using FFF.Core.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace FFF.Core.Services
{
	public interface IUserService
	{
		Task<SignInResult> SignInAsync(SignInViewModel model, string? returnUrl = null);
		Task<SignInResult> SignInAsync(SignUpViewModel model, string? returnUrl = null);
		Task<UserAccountDetailsViewModel> GetMemberAccountDetailsAsync(string userName);
		Task<IdentityResult> ChangeUserPasswordAsync(string currentUsername, string currentPassword, string newPassword);
		Task<IdentityResult> ResetPassword(string userId, string token, string newPassword);
		Task<IdentityResult> RegisterUserAsync(SignUpViewModel model);
		Task<IdentityResult> UpdateAccountDetailsAsync(UserAccountDetailsViewModel model, string userName);
		Task<List<UserAddressesViewModel>> GetUserAddressesAsync(string userID);
		Task<List<UserViewModel>> GetUserListAsync();
	}
}
