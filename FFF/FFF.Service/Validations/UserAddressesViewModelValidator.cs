using FFF.Core.ViewModels;
using FluentValidation;

namespace FFF.Service.Validations
{
	public class UserAddressesViewModelValidator : AbstractValidator<UserAddressesViewModel>
	{
		public UserAddressesViewModelValidator()
		{
			RuleFor(x => x.Title).NotNull().WithMessage("Başlık Alanı Boş Bırakılamaz.");
			RuleFor(x => x.NameSurname).NotNull().WithMessage("Ad-Soyad Alanı Boş Bırakılamaz.");
			RuleFor(x => x.Address).NotNull().WithMessage("Adres Alanı Boş Bırakılamaz.");
			RuleFor(x => x.Zipcode).NotNull().WithMessage("Posta Kodu Alanı Boş Bırakılamaz.").Length(5).WithMessage("Posta Kodu Geçersizdir!");
			RuleFor(x => x.Phone).NotNull().WithMessage("Telefon Numarası Alanı Boş Bırakılamaz.");
			RuleFor(x => x.City).NotNull().WithMessage("Lütfen Şehir Seçimi Yapınız");
		}
	}
}
