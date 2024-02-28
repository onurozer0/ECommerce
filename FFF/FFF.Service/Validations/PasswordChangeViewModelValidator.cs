using FFF.Core.ViewModels;
using FluentValidation;

namespace FFF.Service.Validations
{
	public class PasswordChangeViewModelValidator : AbstractValidator<PasswordChangeViewModel>
	{
		public PasswordChangeViewModelValidator()
		{
			RuleFor(x => x.CurrentPassword).NotNull().WithMessage("Şifre Alanı Boş Bırakılamaz.");
			RuleFor(x => x.NewPassword).NotNull().WithMessage("Şifre Alanı Boş Bırakılamaz.");
			RuleFor(x => x.ConfirmPassword).NotNull().WithMessage("Şifre Doğrulama Alanı Boş Bırakılamaz.").Must((model, confirmPassword) => confirmPassword == model.NewPassword).WithMessage("Şifreler Eşleşmiyor.");
		}
	}
}
