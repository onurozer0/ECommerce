using FFF.Core.Entities;
using FluentValidation;

namespace FFF.Service.Validations
{
	public class SlideValidator : AbstractValidator<Slide>
	{
		public SlideValidator()
		{
			RuleFor(x => x.Title).NotEmpty().WithMessage("Başlık Alanı Boş Bırakılamaz!").MaximumLength(250);
			RuleFor(x => x.Description).NotEmpty().WithMessage("Açıklama Alanı Boş Bırakılamaz!").MaximumLength(150);
			RuleFor(x => x.DisplayIndex).NotEmpty().WithMessage("Görüntülenme Sırası Alanı Boş Bırakılamaz!");
		}
	}
}
