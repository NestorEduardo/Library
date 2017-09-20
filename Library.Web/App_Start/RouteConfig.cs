using System.Web.Mvc;
using System.Web.Routing;

namespace Library.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "BookTypes",
                "{controller}/{action}/{id}",
                new { controller = "BookTypes", action = "List", id = UrlParameter.Optional });

            routes.MapRoute(
                "BookTypesNavigation",
                  "{controller}Page{page}",
                  new { controller = "BookTypes", action = "List", id = UrlParameter.Optional });

            routes.MapRoute(
                "Default",
                "",
                new { controller = "Home", action = "Index" });
        }
    }
}

