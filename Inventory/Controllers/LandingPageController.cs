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
        public ActionResult Index(string command, string email, string usertype, string Site)
        {
            if (command == "Project")
            {
                SqlDataReader value = LoginService.Authenticateuser("getuserprofile", email, null, Site, long.Parse(usertype));
                var type = LoginService.GetUserTypeId(null, long.Parse(usertype));
                //SqlDataReader value = LoginService.Authenticateuser("redirectuser", email, loginpassword, null, long.Parse(usertype));
                string controller = "";
                if (type.ToString() == "Staff" || type.ToString() == "Owner") //checking type
                    controller = "UserHome";
                else
                    controller = type.ToString() + "Home";
                if (value.HasRows == false) //Failed Login
                    return Content("<script language='javascript' type='text/javascript'>alert('Invalid Login!!! Try Again');location.href='" + @Url.Action("Index", "AvailableCompanies", new { email = email }) + "'</script>"); // Stays in Same View

                DataTable dt = new DataTable();
                dt.Load(value);
                UserMaster userMaster = new UserMaster();
                userMaster = (from DataRow row in dt.Rows
                              select new UserMaster()
                              {
                                  First_Name = row["First_Name"].ToString(),
                                  Last_Name = row["Last_Name"].ToString(),
                                  EmailId = row["EmailId"].ToString(),
                                  Created_Date = (DateTime)row["Created_Date"],
                                  CompanyName = row["CompanyName"].ToString(),
                                  Phone = row["Phone"].ToString(),
                                  User_Site = row["User_Site"].ToString(),
                                  UserTypeId = (int)row["UserTypeId"],
                                  SubscriptionDate= (DateTime)row["SubscriptionDate"]
                              }).FirstOrDefault();

                return RedirectToAction("Index", controller, userMaster); // Redirects to Particular View
            }
            return View();
        }
    }
}