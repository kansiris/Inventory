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

namespace Inventory.Controllers
{
    public class UserHomeController : Controller
    {
        // GET: UserHome

        public ActionResult Index(string email, string usertype, string Site)
        {
            List<UserMaster> userMaster = getuserdetails(email, usertype, Site);
            string futuredate = userMaster[0].SubscriptionDate.Value.Date.AddDays(15).Day.ToString(); // adding 15 days to subscription date
            string currentdate = DateTime.UtcNow.Day.ToString(); //getting current date
            ViewBag.accessexpiry = int.Parse(futuredate) - int.Parse(currentdate);//calculating difference b/w current date & future date & passing value to view
            ViewBag.timeZoneInfos = TimeZoneInfo.GetSystemTimeZones().Select(m => m.DisplayName).ToList(); //Available Time Zones
            //ViewBag.currency = TimeZoneInfo.GetSystemTimeZones().Select(m => m.)
            ViewBag.record = userMaster;
            ViewBag.Timezone = "Your selected Timezone is " + userMaster[0].Timezone + " and Date Format is " + userMaster[0].Date_Format + "";
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
                              Date_Format = row["Date_Format"].ToString()
                          }).ToList();
            return userMaster;
        }
    }
}


//string command, string email, string usertype, string Site
//var userId = (CustomPrinciple)System.Web.HttpContext.Current.User;