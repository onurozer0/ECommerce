using FFF.Core.ViewModels;
using FluentValidation;

namespace FFF.Service.Validations
{
	public class SignUpViewModelValidator : AbstractValidator<SignUpViewModel>
	{
		public SignUpViewModelValidator()
		{
			RuleFor(x => x.Password).NotNull().WithMessage("Şifre Alanı Boş Bırakılamaz.");
			RuleFor(x => x.ConfirmPassword).NotNull().WithMessage("Şifre Doğrulama Alanı Boş Bırakılamaz.").Must((model, confirmPassword) => confirmPassword == model.Password)
			.WithMessage("Şifreler Eşleşmiyor.");
			RuleFor(x => x.UserName).NotNull().WithMessage("Kullanıcı Adı Alanı Boş Bırakılamaz.");
			RuleFor(x => x.Email).NotNull().WithMessage("E-Posta Alanı Boş Bırakılamaz.").EmailAddress().WithMessage("E-Posta Doğru Formatta değil");
			RuleFor(x => x.Name).NotNull().WithMessage("İsim Alanı Boş Bırakılamaz.");
			RuleFor(x => x.Surname).NotNull().WithMessage("Soyisim Alanı Boş Bırakılamaz.");
			RuleFor(x => x.PhoneNumber).NotNull().WithMessage("Telefon Numarası Alanı Boş Bırakılamaz.");

		}
	}
}
