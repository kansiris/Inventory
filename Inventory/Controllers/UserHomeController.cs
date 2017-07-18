using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Inventory.Service;
using Inventory.Models;
using System.Web.Security;
using System.Data.SqlClient;
using System.Data;
using System.Diagnostics;

namespace Inventory.Controllers
{
    public class UserHomeController : Controller
    {
        // GET: UserHome
        public ActionResult Index(string email, string usertype, string Site)
        {
            List<UserMaster> userMaster = getuserdetails(email, usertype, Site);
            ViewBag.record = userMaster;
            ViewBag.typeofuser = LoginService.GetUserTypeId("", userMaster.FirstOrDefault().UserTypeId).ToString();
            if (ViewBag.typeofuser == "Owner")
            {
                string date = DateTime.Now.AddDays(16).ToShortDateString();
                DateTime futuredate = userMaster[0].SubscriptionDate.Value.AddDays(16); // adding 15 days to subscription date
                DateTime currentdate = DateTime.UtcNow; //getting current date
                TimeSpan diff = futuredate - currentdate; // calculating difference between 2 dates
                if (diff.Days < 0)
                    ViewBag.accessexpiry = "(Your Trial Period has ended)";
                else
                    ViewBag.accessexpiry = "(Your Login Access is about to expire in " + diff.Days + " Days)";
                ViewBag.timeZoneInfos = TimeZoneInfo.GetSystemTimeZones().Select(m => m.DisplayName).ToList(); //Available Time Zones
                if (userMaster[0].Timezone != null && userMaster[0].Date_Format != null)
                    ViewBag.Timezone = "1";
            }
            else if (ViewBag.typeofuser == "Franchise")
            {
                ViewBag.accessexpiry = "";
            }
            return View();
        }

        [HttpPost]
        public ActionResult Index(string command, UserMaster userMaster)
        {
            if (command == "updatetimezone")
            {
                int value = LoginService.updatetimezone(userMaster.Date_Format, userMaster.Timezone, userMaster.ID);
                return Content("<script language='javascript' type='text/javascript'>alert('Time Zone Updated');location.href='" + @Url.Action("Index", "UserHome", new { email = Request.QueryString["email"], usertype = Request.QueryString["usertype"], Site = Request.QueryString["Site"] }) + "'</script>"); // Stays in Same View
            }
            return View();
        }

        public List<UserMaster> getuserdetails(string email, string usertype, string Site)
        {
            SqlDataReader value = LoginService.Authenticateuser("getuserprofile", email, null, Site, long.Parse(usertype));
            DataTable dt = new DataTable();
            dt.Load(value);
            List<UserMaster> userMaster = new List<UserMaster>();
            userMaster = (from DataRow row in dt.Rows
                          select new UserMaster()
                          {
                              ID = row["ID"].ToString(),
                              First_Name = row["First_Name"].ToString(),
                              Last_Name = row["Last_Name"].ToString(),
                              EmailId = row["EmailId"].ToString(),
                              Created_Date = (DateTime)row["Created_Date"],
                              CompanyName = row["CompanyName"].ToString(),
                              Phone = row["Phone"].ToString(),
                              User_Site = row["User_Site"].ToString(),
                              UserTypeId = (int)row["UserTypeId"],
                              SubscriptionDate = (DateTime)row["SubscriptionDate"],
                              Timezone = row["Timezone"].ToString(),
                              Date_Format = row["Date_Format"].ToString(),
                              Profile_Picture = row["Profile_Picture"].ToString()
                          }).ToList();
            return userMaster;
        }
    }
}