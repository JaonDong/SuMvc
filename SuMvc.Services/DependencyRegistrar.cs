using System.Linq;
using System.Reflection;
using System.Web.Compilation;
using Autofac;
using Autofac.Integration.Mvc;
using CpMvc.Data;
using SuMvc.Core.Autofacs;
using SuMvc.Core.Data;
using SuMvc.Data.EntityFramework;

namespace SuMvc.Services
{
    public class DependencyRegistrar: IDependencyRegistrar
    {
        public void Register(ContainerBuilder builder, ITypeFinder typeFinder)
        {
            //注册mvc c
            builder.RegisterControllers(typeFinder.GetAssemblies().ToArray());
            //注册数据层
            builder.Register<IDbContext>(c => new MvcContext()).InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(EfRepository<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope();

            // 自动注册Service
            var assemblies = BuildManager.GetReferencedAssemblies().Cast<Assembly>()
                .Where(
                    assembly =>
                        assembly.GetTypes().FirstOrDefault(type => type.GetInterfaces().Contains(typeof(IService))) !=
                        null
                );

            // RegisterAssemblyTypes 注册程序集
            var enumerable = assemblies as Assembly[] ?? assemblies.ToArray();
            if (enumerable.Any())
            {
                builder.RegisterAssemblyTypes(enumerable)
                    .Where(type => type.GetInterfaces().Contains(typeof(IService))).AsImplementedInterfaces().InstancePerDependency();
            }
        }

        public int Order { get; }
    }
}