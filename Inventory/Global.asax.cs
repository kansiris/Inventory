using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using Newtonsoft.Json;
using Inventory.Models;
using Inventory.Content;

namespace Inventory
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_PostAuthenticateRequest(Object sender, EventArgs e)
        {
            HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie != null && !string.IsNullOrEmpty(authCookie.Value))
            {
                FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);

                if (authTicket != null)
                {
                    var serializeModel = JsonConvert.DeserializeObject<UserMaster>(authTicket.UserData);
                    var newUser = new CustomPrinciple(authTicket.Name)
                    {
                        ID = serializeModel.ID,
                        FirstName = serializeModel.First_Name,
                        LastName = serializeModel.Last_Name,
                        Emailid = serializeModel.EmailId,
                        DbName = serializeModel.DB_Name,
                        UserSite = serializeModel.User_Site,
                        CompanyName = serializeModel.CompanyName,
                        Phone = serializeModel.Phone
                    };
                    HttpContext.Current.User = newUser;

                }
            }
        }
    }
}
