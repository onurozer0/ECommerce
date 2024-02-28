using Microsoft.AspNetCore.Identity;

namespace FFF.Service.Localization
{
	public class LocalizationIdentityErrorDescriber : IdentityErrorDescriber
	{
		public override IdentityError DuplicateUserName(string userName)
		{
			return new()
			{
				Code = "DuplicateUserName",
				Description = $"{userName} kullanıcı adıyla kayıtlı bir kullanıcı mevcuttur."
			};
		}
		public override IdentityError DuplicateEmail(string email)
		{
			return new()
			{
				Code = "DuplicateEmail",
				Description = $"{email} E-Postasıyla kayıtlı bir kullanıcı mevcuttur."
			};
		}
		public override IdentityError PasswordTooShort(int length)
		{
			return new()
			{
				Code = "PwdTooShort",
				Description = "Parola en az 8 karakter uzunluğunda olmalıdır."
			};
		}
		public override IdentityError PasswordRequiresNonAlphanumeric()
		{
			return new()
			{
				Code = "PwdMustContainNonAlphanumeric",
				Description = "Parola özel karakter içermelidir."
			};
		}
		public override IdentityError PasswordRequiresDigit()
		{
			return new()
			{
				Code = "PwdMustContainDigit",
				Description = "Parola sayı içermelidir."
			};
		}
		public override IdentityError PasswordRequiresLower()
		{
			return new()
			{
				Code = "PwdMustContainLowercase",
				Description = "Parola küçük harf içermelidir."
			};
		}
		public override IdentityError PasswordRequiresUpper()
		{
			return new()
			{
				Code = "PwdMustContainUppercase",
				Description = "Parola büyük harf içermelidir."
			};
		}
		public override IdentityError InvalidUserName(string? userName)
		{
			return new()
			{
				Code = "InvalidUserName",
				Description = "Kullanıcı adı sadece (a-z),(0-9),('_') karakterlerini içerebilir."
			};
		}
	}
}
