using System.Web.Mvc;
using System.Web.Routing;

namespace Library.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute
                (name: null,
                url: "Page{page}",
                defaults: new { Controller = "BookTypes", action = "List" });

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "BookTypes", action = "List", id = UrlParameter.Optional }
            );
        }
    }
}
