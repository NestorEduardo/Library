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
          "Default", // Route name
          "",        // URL with parameters
          new { controller = "Home", action = "Index" }  // Parameter defaults
      );
        }
    }
}
