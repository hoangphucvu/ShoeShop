using System.Web.Mvc;
using System.Web.Routing;

namespace ShoeShop
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapMvcAttributeRoutes();
            routes.MapRoute("Default", "{controller}/{action}/{id}",
                new {controller = "Home", action = "Index", id = UrlParameter.Optional}
                );
            // routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            //routes.MapRoute(
            //    name: "Details",
            //    url: "san-pham/{name}-{id}",
            //               defaults: new { controller = "Home", action = "Details" }
            //);
            //  routes.MapRoute(
            //    name: "Category",
            //    url: "chung-loai/{name}-{id}.html",
            //               defaults: new { controller = "Home", action = "Category" }
            //);
        }
    }
}