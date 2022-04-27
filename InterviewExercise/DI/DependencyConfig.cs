using Autofac;
using Autofac.Integration.Mvc;
using InterviewExercise.Controllers;
using IService;
using Models;
using Service;
using System.Reflection;

namespace InterviewExercise.DI
{
    /// <summary>
    /// DI注入
    /// </summary>
    public static class DependencyConfig
	{
		public static IContainer RegisterComponents()
		{
			//主要的
			var builder = new ContainerBuilder();

			//工作單位
			builder.RegisterGeneric(typeof(UnitOfWork<>))
					.As(typeof(IUnitOfWork<>));
			//使用
			builder.RegisterType<NorthwindService>()
				   .As<INorthwindService>();
			//EDMX
			builder.RegisterType<NorthwindService>()
					.As<INorthwindService>();
			//EDMX
			builder.RegisterType<NorthwindEntities>()
					.As<NorthwindEntities>();

			//所註冊的Controller
			builder.RegisterControllers(Assembly.GetAssembly(typeof(HomeController)));

			//Build起來
			return builder.Build();
		}
	}
}