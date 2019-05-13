using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Prikhodko.NewsWebsite.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Tags",
                url: "{controller}/{action}",
                defaults: new { controller = "Tags", action = "Index"}
            );

            //routes.MapRoute(
            //    name: "Posts",
            //    url: "{controller}/{id}",
            //    defaults: new { controller = "Post", action = "Details", id = UrlParameter.Optional }
            //);
        }
    }
}
