using FFF.Core.Entities;
using FluentValidation;

namespace FFF.Service.Validations
{
	public class BrandValidator : AbstractValidator<Brand>
	{
		public BrandValidator()
		{
			RuleFor(x => x.Name).NotEmpty().WithMessage("Ad Alanı Boş Bırakılamaz!").NotNull().WithMessage("Ad Alanı Boş Bırakılamaz!");
			RuleFor(x => x.DisplayIndex).NotNull().WithMessage("Görüntülenme Sırası Alanı Boş Bırakılamaz!").NotEmpty().WithMessage("Görüntülenme Sırası Alanı Boş Bırakılamaz!");
		}
	}
}
