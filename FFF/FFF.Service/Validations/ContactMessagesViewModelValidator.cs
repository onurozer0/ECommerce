using FFF.Core.ViewModels;
using FluentValidation;

namespace FFF.Service.Validations
{
	public class ContactMessagesViewModelValidator : AbstractValidator<ContactMessagesViewModel>
	{
		public ContactMessagesViewModelValidator()
		{
			RuleFor(x => x.NameSurname).NotNull().WithMessage("Ad-Soyad Alanı Boş Bırakılamaz.");
			RuleFor(x => x.Email).NotNull().WithMessage("E-Posta Alanı Boş Bırakılamaz.").EmailAddress().WithMessage("E-Posta Adresi Doğru Formatta Değil");
			RuleFor(x => x.Message).NotNull().WithMessage("Mesaj Alanı Boş Bırakılamaz.");

		}
	}
}
