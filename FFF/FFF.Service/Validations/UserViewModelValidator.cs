using FFF.Core.ViewModels;
using FluentValidation;

namespace FFF.Service.Validations
{
	public class UserViewModelValidator : AbstractValidator<UserViewModel>
	{
		public UserViewModelValidator()
		{
			RuleFor(x => x.Email).NotNull().WithMessage("E-Posta Alanı Boş Bırakılamaz.").EmailAddress().WithMessage("E-Posta Doğru Formatta değil");
			RuleFor(x => x.Name).NotNull().WithMessage("Ad Alanı Boş Bırakılamaz.");
			RuleFor(x => x.Surname).NotNull().WithMessage("Soyad Alanı Boş Bırakılamaz.");
			RuleFor(x => x.PhoneNumber).NotNull().WithMessage("Telefon Numarası Alanı Boş Bırakılamaz.");
			RuleFor(x => x.isBanned).NotNull().WithMessage("Lütfen Durum Seçimi Yapınız!");
		}
	}
}
