using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Inventory.Content;

namespace Inventory
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //routes.MapRoute(
            //        name : "UserHome_redirect",
            //        url : "{username}/{controller}/{action}/{id}",
            //        defaults:new { controller = "UserHome", action = "Index"},
            //        constraints: new { username = new usernameroute() }
            //);

        //    routes.MapRoute(
        //    name: "UserNameRoute_redirect",
        //    url: "{username}",
        //    defaults: new { controller = "UserHome", action = "Index" , username =""}
        //);

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "homepage", action = "Index", id = UrlParameter.Optional }
            );
        }

        //public class VanityUrlContraint : IRouteConstraint
        //{
        //    private static readonly string[] Controllers =  Assembly.GetExecutingAssembly().GetTypes().Where(x => typeof(IController).IsAssignableFrom(x)).Select(x => x.Name.ToLower().Replace("controller", "")).ToArray();

        //    public bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values,
        //                      RouteDirection routeDirection)
        //    {
        //        return !Controllers.Contains(values[parameterName].ToString().ToLower());
        //    }
        //}

        //public class usernameroute : IRouteConstraint
        //{
        //    public bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
        //    {
        //        var username = values["username"].ToString();

        //        //if (httpContext.User.Identity.IsAuthenticated)
        //        //{
        //        //Compare the username to the logged in user
        //        if (username == "UserHome")
        //        {
        //            //var user = (CustomPrinciple)System.Web.HttpContext.Current.User;
        //            return true;
        //        }


        //        //}

        //        return false;

        //        //List<string> users = new List<string>() { "abc@gmail.com", "stephen" };
        //        //// Get the username from the url
        //        //var username = values["username"].ToString().ToLower();
        //        //// Check for a match (assumes case insensitive)
        //        //var data = users.Any(x => x.ToLower() == username);
        //        //return data;
        //    }
        //}
    }
}
