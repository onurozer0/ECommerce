using FFF.Core.ViewModels;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFF.Service.Validations
{
	public class CreateOrderViewModelValidator : AbstractValidator<CreateOrderViewModel>
	{
        public CreateOrderViewModelValidator()
        {
            RuleFor(x => x.OrderDt.AddressId).NotNull().WithMessage("Lütfen Adres Seçimi Yapınız!").NotEmpty().WithMessage("Lütfen Adres Seçimi Yapınız!");
            RuleFor(x => x.OrderDt.CardNumber).NotNull().WithMessage("Lütfen Kart Numarası Alanını Doldurunuz!").NotEmpty().WithMessage("Lütfen Kart Numarası Alanını Doldurunuz!");
			RuleFor(x => x.OrderDt.CardMonth).NotNull().WithMessage("Lütfen Kart Son Kullanma Tarihi(Ay) Alanını Doldurunuz!").NotEmpty().WithMessage("Lütfen Kart Son Kullanma Tarihi(Ay) Alanını Doldurunuz!");
            RuleFor(x => x.OrderDt.CarddYear).NotNull().WithMessage("Lütfen Kart Son Kullanma Tarihi(Yıl) Alanını Doldurunuz!").NotEmpty().WithMessage("Lütfen Kart Son Kullanma Tarihi(Yıl) Alanını Doldurunuz!");
            RuleFor(x => x.OrderDt.CardCv2).NotNull().WithMessage("Lütfen Kart CVV Alanını Doldurunuz!").NotEmpty().WithMessage("Lütfen Kart CVV Alanını Doldurunuz!");
        }
    }
}
