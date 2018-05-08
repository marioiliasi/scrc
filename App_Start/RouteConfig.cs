using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace scrc
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Books",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Books", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
               "Users",                                           // Route name
               "{controller}/{action}/{id}",                            // URL with parameters
               new { controller = "Users", action = "Index", id = UrlParameter.Optional }  // Parameter defaults
           );
        }
    }
}
