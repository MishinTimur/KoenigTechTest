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
//            routes.MapRoute(
//                name: "account",
//                url: "account",
//                defaults: new {controller = "Home", action = "Index"});
//            routes.MapRoute(
//                name: "signup",
//                url: "signup",
//                defaults: new { controller = "Account", action = "Login" });
//            routes.MapRoute("accountControllerRoute", "{action}", new {controller = "Account"}, new {action = "^Login$|^Register$|^LogOff$"});
//            routes.MapRoute("Default", "account", new { controller = "Home", action="Index" });
            routes.MapRoute(
                name: "common",
                url: "{controller}/{action}",
                defaults: new { controller = "Home", action = "Index" }
            );
        }
    }
}
