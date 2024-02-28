using FFF.Core.ViewModels;
using FluentValidation;

namespace FFF.Service.Validations
{
	public class UserAccountDetailsViewModelValidator : AbstractValidator<UserAccountDetailsViewModel>
	{
		public UserAccountDetailsViewModelValidator()
		{
			RuleFor(x => x.Password).NotNull().WithMessage("Şifre Alanı Boş Bırakılamaz.");
			RuleFor(x => x.PasswordConfirm).NotNull().WithMessage("Şifre Doğrulama Alanı Boş Bırakılamaz.").Must((model, confirmPassword) => confirmPassword == model.Password)
			.WithMessage("Şifreler Eşleşmiyor.");
			RuleFor(x => x.Email).NotNull().WithMessage("E-Posta Alanı Boş Bırakılamaz.").EmailAddress().WithMessage("E-Posta Doğru Formatta değil");
			RuleFor(x => x.FirstName).NotNull().WithMessage("İsim Alanı Boş Bırakılamaz.");
			RuleFor(x => x.LastName).NotNull().WithMessage("Soyisim Alanı Boş Bırakılamaz.");
			RuleFor(x => x.PhoneNumber).NotNull().WithMessage("Telefon Numarası Alanı Boş Bırakılamaz.");

			RuleFor(x => x.DateOfBirth.Value.Year).ExclusiveBetween(DateTime.Now.AddYears(-100).Year, DateTime.Now.Year);
		}
	}
}
