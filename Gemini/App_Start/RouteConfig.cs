using System;
using System.Linq;
using Gemini.Models;
using System.Web.Mvc;
using System.Web.Routing;

namespace Gemini
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            try
            {
                routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

                #region Add portal admin in router
                var db = new GeminiEntities();

                routes.MapRoute(
                    name: "admin",
                    url: "admin" + "/{controller}/{action}/{id}/{Menu}",
                    defaults: new { Portal = "admin", controller = "Admin", action = "Index", id = UrlParameter.Optional, Menu = "start" }
                );
                #endregion

                routes.MapRoute(
                    name: "Property",
                    url: "property/{guid}",
                    defaults: new { controller = "HomeCommon", action = "Property", guid = UrlParameter.Optional }
                );

                routes.MapRoute(
                    name: "Search",
                    url: "search",
                    defaults: new { controller = "HomeCommon", action = "Search" }
                );

                routes.MapRoute(
                    name: "Category",
                    url: "category/{type}/{propertyType}",
                    defaults: new { controller = "HomeCommon", action = "Category", type = UrlParameter.Optional, propertyType = UrlParameter.Optional }
                );

                routes.MapRoute(
                    name: "AboutUs",
                    url: "about-us",
                    defaults: new { controller = "HomeCommon", action = "AboutUs" }
                );

                routes.MapRoute(
                    name: "Error",
                    url: "error",
                    defaults: new { controller = "HomeCommon", action = "Error" }
                );

                #region Router defaults
                //==================================================================//
                // Router cho trang chu va cac PartialView
                routes.MapRoute(
                    name: "Default",
                    url: "{controller}/{action}/{id}/{id1}",
                    defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional, id1 = UrlParameter.Optional }
                );
                #endregion
            }
            catch (Exception ex)
            {

            }
        }
    }
}