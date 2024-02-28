using Autofac;
using FFF.Core.Repositories;
using FFF.Core.Services;
using FFF.Core.UnitOfWorks;
using FFF.Core.ViewModels;
using FFF.Repository;
using FFF.Repository.Repositories;
using FFF.Repository.UnitOfWorks;
using FFF.Service.Services;
using System.Reflection;
using Module = Autofac.Module;

namespace FFF.Web.Modules
{
	public class RepositoryServiceModule : Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			builder.RegisterGeneric(typeof(GenericRepository<>)).As(typeof(IGenericRepository<>)).InstancePerLifetimeScope();
			builder.RegisterGeneric(typeof(GenericService<>)).As(typeof(IGenericService<>)).InstancePerLifetimeScope();
			builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();

			var apiAssembly = Assembly.GetExecutingAssembly();
			var repoAssembly = Assembly.GetAssembly(typeof(AppDbContext));
			var serviceAssembly = Assembly.GetAssembly(typeof(UserService));

			builder.RegisterAssemblyTypes(apiAssembly, repoAssembly, serviceAssembly).Where(x => x.Name.EndsWith("Repository")).AsImplementedInterfaces().InstancePerLifetimeScope();
			builder.RegisterAssemblyTypes(apiAssembly, repoAssembly, serviceAssembly).Where(x => x.Name.EndsWith("Service")).AsImplementedInterfaces().InstancePerLifetimeScope();
		}
	}
}
