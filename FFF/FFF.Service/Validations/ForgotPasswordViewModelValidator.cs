using FFF.Core.ViewModels;
using FluentValidation;

namespace FFF.Service.Validations
{
	public class ForgotPasswordViewModelValidator : AbstractValidator<ForgotPasswordViewModel>
	{
		public ForgotPasswordViewModelValidator()
		{
			RuleFor(x => x.Email).NotNull().WithMessage("E-Posta Alanı Boş Bırakılamaz.").EmailAddress().WithMessage("E-Posta Doğru Formatta değil");
		}
	}
}
