using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;

namespace SuMvc.Core.Autofacs
{
    public class AutofacEngine:IEngine
    {
        private ContainerManager _containerManager;
        public ContainerManager ContainerManager => _containerManager;

        public void Initialize()
        {
           //初始化
           var builder=new ContainerBuilder();
            
            var typeFinder = new WebAppTypeFinder();
            //注册自身
            builder.RegisterInstance(this).As<IEngine>().SingleInstance();
            builder.RegisterInstance(typeFinder).As<ITypeFinder>().SingleInstance();

            //找到程序集里所有继承IDependencyRegistrar的类，并对里面需要注册进行统一注册
            var drTypes = typeFinder.FindClassesOfType<IDependencyRegistrar>();
            var drInstances = new List<IDependencyRegistrar>();
            foreach (var drType in drTypes)
                drInstances.Add((IDependencyRegistrar)Activator.CreateInstance(drType));
            //sort
            drInstances = drInstances.AsQueryable().OrderBy(t => t.Order).ToList();
            foreach (var dependencyRegistrar in drInstances)
                dependencyRegistrar.Register(builder, typeFinder);

            var container = builder.Build();
            this._containerManager = new ContainerManager(container);

            //实现MVC的依赖注入，并指定使用的容器
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }

        public T Resolve<T>() where T : class
        {
            throw new NotImplementedException();
        }

        public object Resolve(Type type)
        {
            throw new NotImplementedException();
        }

        public T[] ResolveAll<T>()
        {
            throw new NotImplementedException();
        }
    }
}