using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Inventory.Service;
using System.Data.SqlClient;
using System.Data;
using Inventory.Models;

namespace Inventory.Controllers
{
    public class LandingPageController : Controller
    {
        // GET: LandingPage
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string command, string email, string usertype, string User_Site)
        {
            if (command == "Project")
            {
                SqlDataReader value = LoginService.Authenticateuser("getuserprofile", email, null, User_Site, long.Parse(usertype));
                var type = LoginService.GetUserTypeId(null, long.Parse(usertype));
                string controller = "UserHome";
                //if (type.ToString() == "Staff" || type.ToString() == "Owner") //checking type
                //    controller = "UserHome";
                //else
                //    controller = type.ToString() + "Home";
                if (value.HasRows == false) //Failed Login
                    return Content("<script language='javascript' type='text/javascript'>alert('Invalid Login!!! Try Again');location.href='" + @Url.Action("Index", "AvailableCompanies", new { email = email }) + "'</script>"); // Stays in Same View
                value.Close();
                return RedirectToAction("Index", controller, new { email = email, site = User_Site, usertype = usertype });
            }
            return View();
        }
    }
}