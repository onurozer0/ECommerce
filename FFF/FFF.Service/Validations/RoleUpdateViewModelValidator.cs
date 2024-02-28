using FFF.Core.ViewModels;
using FluentValidation;

namespace FFF.Service.Validations
{
	public class RoleUpdateViewModelValidator : AbstractValidator<RoleUpdateViewModel>
	{
		public RoleUpdateViewModelValidator()
		{
			RuleFor(x => x.Name).NotEmpty().WithMessage("Rol Adı Alanı Boş Bırakılamaz!");
		}
	}
}
