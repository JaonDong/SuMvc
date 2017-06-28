using System.Web.Mvc;
using System.Web.Routing;
using SuMvc.Core.Autofacs;

namespace SuMvc.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            EngineContext.Current.Initialize();

            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}
