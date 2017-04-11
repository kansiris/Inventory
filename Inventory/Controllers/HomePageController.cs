using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Inventory.Content;
using Inventory.Models;
using System.Data.SqlClient;
using System.Data;
using Inventory.Service;

namespace Inventory.Controllers
{
    public class HomePageController : Controller
    {
        LoginService loginService = new LoginService();
        // GET: HomePage
        public ActionResult Index()
        {
            if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
            {
                var user = (CustomPrinciple)System.Web.HttpContext.Current.User;
                var profile = loginService.GetUserProfile(int.Parse(user.ID)); //Get's User Profile
                int usertype = (int)profile[0].UserTypeId;
                var type = LoginService.GetUserTypeId(null, usertype);
                string controller = "";
                if (type.ToString() == "Staff" || type.ToString() == "Owner") //checking type
                    controller = "UserHome";
                else
                    controller = type.ToString() + "Home";
                return RedirectToAction("Index", controller, new { email = profile[0].EmailId, site = profile[0].User_Site, usertype = usertype });
            }
            return View();
        }
    }
}