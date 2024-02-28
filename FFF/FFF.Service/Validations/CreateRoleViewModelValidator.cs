using FFF.Core.ViewModels;
using FluentValidation;

namespace FFF.Service.Validations
{
	public class CreateRoleViewModelValidator : AbstractValidator<CreateRoleViewModel>
	{
		public CreateRoleViewModelValidator()
		{
			RuleFor(x => x.Name).NotEmpty().WithMessage("Rol Adı Alanı Boş Bırakılamaz!");
		}
	}
}
