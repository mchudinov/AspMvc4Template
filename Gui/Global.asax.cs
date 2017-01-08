using System;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Gui.Controllers;
using NLog;
using Repositories;
using SimpleInjector;
using SimpleInjector.Integration.Web;
using SimpleInjector.Integration.Web.Mvc;
using UseCases;

namespace Gui
{
    public class MvcApplication : HttpApplication
    {
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            #region SimpleInjector

            var container = new Container();
            container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();
            container.Register<IUserRepository, UserRepository>(Lifestyle.Scoped);
            container.Register<IWidgetRepository, WidgetRepository>(Lifestyle.Scoped);
            container.Register<IUserCase, UserCase>(Lifestyle.Scoped);
            container.Register<IWidgetCase, WidgetCase>(Lifestyle.Scoped);
            container.RegisterMvcControllers(Assembly.GetExecutingAssembly());
            container.RegisterMvcIntegratedFilterProvider();
            container.Verify();
            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));

            #endregion
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            Exception ex = Server.GetLastError();
            if (ex != null)
            {
                StringBuilder err = new StringBuilder();
                err.Append("Error caught in Application_Error event\n");
                err.Append("Error in: " + (Context.Session == null ? string.Empty : Request.Url.ToString()));
                err.Append("\nError Message:" + ex.Message);
                if (null != ex.InnerException)
                    err.Append("\nInner Error Message:" + ex.InnerException.Message);
                err.Append("\n\nStack Trace:" + ex.StackTrace);
                Server.ClearError();

                if (null != Context.Session)
                {
                    err.Append($"Session: Identity name:[{Thread.CurrentPrincipal.Identity.Name}] IsAuthenticated:{Thread.CurrentPrincipal.Identity.IsAuthenticated}");
                }
                Log.Error(err.ToString());

                if (null != Context.Session)
                {
                    var routeData = new RouteData();
                    routeData.Values.Add("controller", "ErrorPage");
                    routeData.Values.Add("action", "Error");
                    routeData.Values.Add("exception", ex);

                    if (ex.GetType() == typeof(HttpException))
                    {
                        routeData.Values.Add("statusCode", ((HttpException)ex).GetHttpCode());
                    }
                    else
                    {
                        routeData.Values.Add("statusCode", 500);
                    }
                    Response.TrySkipIisCustomErrors = true;
                    IController controller = new ErrorPageController();
                    controller.Execute(new RequestContext(new HttpContextWrapper(Context), routeData));
                    Response.End();
                }
            }
        }

        protected void Session_Start(object sender, EventArgs e)
        {
            var identity = Thread.CurrentPrincipal.Identity;
            Log.Info($"Session_Start. Identity name:[{identity.Name}] IsAuthenticated:{identity.IsAuthenticated}");
        }

        protected void Session_End(object sender, EventArgs e) {}
    }
}
