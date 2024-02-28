using FFF.Core.Entities;
using FFF.Repository;
using FFF.Service.Localization;
using FFF.Service.Mapping;
using FFF.Service.Validations;
using FFF.Service.Validations.CustomValidators;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;

namespace FFF.Web.Extensions
{
	public static class StartingExtensions
	{
		public static void AddIdentityAndFluentValidationViaExtension(this IServiceCollection services)
		{
			services.AddAutoMapper(typeof(MapProfile));
			services.Configure<DataProtectionTokenProviderOptions>(options =>
			{
				options.TokenLifespan = TimeSpan.FromMinutes(30);
			});
			services.AddIdentity<AppUser, AppRole>(opt =>
			{
				opt.User.RequireUniqueEmail = true;
				opt.User.AllowedUserNameCharacters = "1234567890abcdefghijklmnoprstuvwxyz_";
				opt.Password.RequireNonAlphanumeric = true;
				opt.Password.RequiredLength = 8;
				opt.Password.RequireUppercase = true;
				opt.Password.RequireLowercase = true;
				opt.Password.RequireDigit = true;

				// LockOut Ayarları
				opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
				opt.Lockout.MaxFailedAccessAttempts = 5;
				// -------------------------------------

			}).AddPasswordValidator<PasswordValidator>().AddEntityFrameworkStores<AppDbContext>().AddErrorDescriber<LocalizationIdentityErrorDescriber>().AddDefaultTokenProviders();
			services.AddFluentValidationClientsideAdapters();
			services.AddFluentValidationAutoValidation();
			services.AddValidatorsFromAssemblyContaining<SignInViewModelValidator>();
			services.Configure<SecurityStampValidatorOptions>(opt =>
			{
				opt.ValidationInterval = TimeSpan.FromMinutes(30);
			});


		}
	}
}
