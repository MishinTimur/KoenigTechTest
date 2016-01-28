using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace KoenigTechTest
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute("accountControllerRoute", "{action}", new {controller = "Account"}, new {action = "^Login$|^Register$|^LogOff$"});
            routes.MapRoute("Default", "Account", new { controller = "Home", action="Index" });
            routes.MapRoute(
                name: "common",
                url: "{controller}/{action}",
                defaults: new { controller = "Home", action = "Index" }
            );
        }
    }
}
