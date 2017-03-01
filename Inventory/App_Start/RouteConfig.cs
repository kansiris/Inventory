using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Inventory.Controllers;

namespace Inventory
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "homepage", action = "Index", id = UrlParameter.Optional }
            );
        }

        public class usernameroute : IRouteConstraint
        {
            public bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
            {
                List<string> users = new List<string>() { "abc@gmail.com", "stephen" };
                // Get the username from the url
                var username = values["username"].ToString().ToLower();
                // Check for a match (assumes case insensitive)
                var data = users.Any(x => x.ToLower() == username);
                return data;
            }
        }
    }
}
