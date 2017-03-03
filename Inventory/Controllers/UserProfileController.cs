using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using Inventory.Service;
using System.Data;
using Inventory.Models;

namespace Inventory.Controllers
{
    public class UserProfileController : Controller
    {
        // GET: UserProfile
        public ActionResult Index(string id)
        {
            ViewBag.profile = GetUserProfile(int.Parse(id));
            ViewBag.timeZoneInfos = TimeZoneInfo.GetSystemTimeZones().Select(m => m.DisplayName).ToList(); //Available Time Zones
            return View();
        }


        public List<UserMaster> GetUserProfile(int id)
        {
            SqlDataReader value = LoginService.GetUserProfile(id);
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
                                      DB_Name =row["DB_Name"].ToString(),
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



