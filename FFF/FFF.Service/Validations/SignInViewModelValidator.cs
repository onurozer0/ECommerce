using FFF.Core.ViewModels;
using FluentValidation;

namespace FFF.Service.Validations
{
	public class SignInViewModelValidator : AbstractValidator<SignInViewModel>
	{
		public SignInViewModelValidator()
		{
			RuleFor(x => x.Email).NotNull().WithMessage("E-Posta Alanı Boş Bırakılamaz.").EmailAddress().WithMessage("E-Posta Doğru Formatta değil.");
			RuleFor(x => x.Password).NotNull().WithMessage("Şifre Alanı Boş Bırakılamaz.");
			RuleFor(x => x.RememberMe).NotNull();
		}
	}
}
