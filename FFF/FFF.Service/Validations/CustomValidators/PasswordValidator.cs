using FFF.Core.Entities;
using Microsoft.AspNetCore.Identity;

namespace FFF.Service.Validations.CustomValidators
{
	public class PasswordValidator : IPasswordValidator<AppUser>
	{
		public Task<IdentityResult> ValidateAsync(UserManager<AppUser> manager, AppUser user, string? password)
		{
			var errors = new List<IdentityError>();
			if (password!.ToLower().Contains(user.UserName!.ToLower()) || password!.ToLower().Contains(user.Name.ToLower()) || password!.Contains(user.Surname.ToLower()))
			{
				errors.Add(new()
				{
					Code = "pwdContainsName",
					Description = "Şifre Alanı kullanıcı adı,ad,soyad alanlarını içeremez"
				});
			}
			if (errors.Any())
			{
				return Task.FromResult(IdentityResult.Failed(errors.ToArray()));
			}
			return Task.FromResult(IdentityResult.Success);

		}
	}
}
