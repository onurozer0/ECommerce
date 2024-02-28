using FFF.Core.ViewModels;
using FluentValidation;

namespace FFF.Service.Validations
{
	public class ProductViewModelValidator : AbstractValidator<ProductViewModel>
	{
		public ProductViewModelValidator()
		{
			RuleFor(x => x.Product.Name).NotEmpty().WithMessage("Ürün Adı Alanı Boş Bırakılamaz!").NotNull().WithMessage("Ürün Adı Alanı Boş Bırakılamaz!");
			RuleFor(x => x.Product.BrandId).NotEmpty().WithMessage("Marka Alanı Boş Bırakılamaz!");
			RuleFor(x => x.Product.CategoryId).NotEmpty().WithMessage("Kategori Alanı Boş Bırakılamaz!");
			RuleFor(x => x.Product.Price).NotEmpty().WithMessage("Fiyat Alanı Boş Bırakılamaz!");
			RuleFor(x => x.Product.UnitsInStock).NotEmpty().WithMessage("Stok Alanı Boş Bırakılamaz!");
			RuleFor(x => x.Product.Details).NotEmpty().WithMessage("Detay Alanı Boş Bırakılamaz!");
			RuleFor(x => x.Product.Description).NotEmpty().WithMessage("Açıklama Alanı Boş Bırakılamaz!").MaximumLength(250).WithMessage("Alana Maksimum 250 Karakter Girilebilir!");
			RuleFor(x => x.Product.ShippingDetails).NotEmpty().WithMessage("Kargo Detayları Alanı Boş Bırakılamaz!").MaximumLength(250).WithMessage("Alana Maksimum 250 Karakter Girilebilir!");
		}
	}
}
