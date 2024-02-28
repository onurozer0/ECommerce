using Autofac;
using Autofac.Extensions.DependencyInjection;
using FFF.Core.OptionModels;
using FFF.Core.ViewModels;
using FFF.Repository;
using FFF.Web.Extensions;
using FFF.Web.Modules;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews(options => options.ModelValidatorProviders.Clear());
builder.Services.AddDbContext<AppDbContext>(opt =>
{
	opt.UseSqlServer(builder.Configuration.GetConnectionString("CS1"));
});
builder.Services.AddIdentityAndFluentValidationViaExtension();
builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));
builder.Services.ConfigureApplicationCookie(opt =>
{
	var cookieBuilder = new CookieBuilder();
	cookieBuilder.Name = "AuthCookie";
	opt.LoginPath = new PathString("/signin");
	opt.LogoutPath = new PathString("/logout");
	opt.AccessDeniedPath = new PathString("/Member/AccessDenied");
	opt.Cookie = cookieBuilder;
	opt.ExpireTimeSpan = TimeSpan.FromDays(365);
	opt.SlidingExpiration = true;

});
builder.Host.UseServiceProviderFactory
	(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder => containerBuilder.RegisterModule(new RepositoryServiceModule()));
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
	options.SuppressModelStateInvalidFilter = true;
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseStatusCodePagesWithRedirects("/error/{0}");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}
app.UseStatusCodePagesWithRedirects("/error/{0}");
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
	name: "areas",
	pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");


app.Run();
