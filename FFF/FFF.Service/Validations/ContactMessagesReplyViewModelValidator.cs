using FFF.Core.ViewModels;
using FluentValidation;

namespace FFF.Service.Validations
{
	public class ContactMessagesReplyViewModelValidator : AbstractValidator<ContactMessageReplyViewModel>
	{
		public ContactMessagesReplyViewModelValidator()
		{
			RuleFor(x => x.ReplyMessage).NotNull().WithMessage("Mesaj Alanı Boş Bırakılamaz.").MaximumLength(2000).WithMessage("Maksimum 2000 karakter gönderilebilir!");
			RuleFor(x => x.Email).NotNull().WithMessage("E-Posta Alanı Boş Bırakılamaz.");
			RuleFor(x => x.Message).NotNull().WithMessage("Mesaj Alanı Boş Bırakılamaz.");
		}
	}
}
