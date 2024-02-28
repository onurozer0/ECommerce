using FFF.Core.ViewModels;
using FluentValidation;

namespace FFF.Service.Validations
{
	public class CategoryViewModelValidator : AbstractValidator<CategoryViewModel>
	{
		public CategoryViewModelValidator()
		{
			RuleFor(x => x.Category.Name).NotEmpty().WithMessage("Kategori Adı Alanı Boş Bırakılamaz!").NotNull().WithMessage("Kategori Adı Alanı Boş Bırakılamaz!");
			RuleFor(x => x.Category.DisplayIndex).NotEmpty().WithMessage("Görüntülenme Sırası Alanı Boş Bırakılamaz!").NotNull().WithMessage("Görüntülenme Sırası Adı Alanı Boş Bırakılamaz!");
		}
	}
}
