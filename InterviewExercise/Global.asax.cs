using Autofac.Integration.Mvc;
using InterviewExercise.DI;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace InterviewExercise
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            var container = DependencyConfig.RegisterComponents();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}
