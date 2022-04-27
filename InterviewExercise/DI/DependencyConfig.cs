using Autofac;
using Autofac.Integration.Mvc;
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
			builder.RegisterType<AchievementService>()
				   .As<IAchievementService>();
			//EDMX
			builder.RegisterType<RecycleEntities>()
					.As<RecycleEntities>();
			//EDMX
			builder.RegisterType<Recycle_DBNEntities>()
					.As<Recycle_DBNEntities>();
			//所註冊的Controller
			builder.RegisterControllers(Assembly.GetAssembly(typeof(AdminController)));
			//Build起來
			return builder.Build();
		}
	}
}