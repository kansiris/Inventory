using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Inventory.Service;
using System.Data.SqlClient;
using System.Data;
using Inventory.Models;
using Newtonsoft.Json;
using Inventory.Utility;

namespace Inventory.Controllers
{
    public class AvailableCompaniesController : Controller
    {
        // GET: AvailableCompanies
        public ActionResult Index()
        {
            string emailid = Request.QueryString["email"];
            SqlDataReader value = LoginService.Authenticateuser("loginsite", emailid, null, null, 0);
            DataTable dt = new DataTable();
            dt.Load(value);
            List<UserMaster> userMaster = new List<UserMaster>();
            userMaster = (from DataRow row in dt.Rows
                          select new UserMaster()
                          {
                              ID = row["ID"].ToString(),
                              User_Site = row["User_Site"].ToString(),
                              UserTypeId = (int)row["UserTypeId"],
                              company_logo = row["company_logo"].ToString(),
                              EmailId = row["EmailId"].ToString(),
                              First_Name = row["First_Name"].ToString()
                              //EmailId = row["EmailId"].ToString()
                          }).ToList();
            ViewBag.records = userMaster;
            value.Close();
            if (TempData["msg"] != null)
            { ViewBag.msg = TempData["msg"]; }
            if (TempData["umsg"] != null)
            { ViewBag.umsg = TempData["umsg"]; }
            return View();
        }

        [HttpPost]
        public ActionResult Index(string email, string usertype, string loginpassword, string userid)
        {
            SqlDataReader value = LoginService.Authenticateuser("redirectuser", email, loginpassword, userid, long.Parse(usertype));
            UserMaster userMaster = new UserMaster();
            if (value.HasRows)
            {
                userMaster = convert(value, userid);
                if (userMaster.IsActive != 0)
                {
                    value.Close();
                    string userData = JsonConvert.SerializeObject(userMaster);
                    ValidUser.SetAuthCookie(userData, userMaster.ID);
                    return RedirectToAction("Index", "LandingPage", new { email, usertype, userMaster.User_Site });
                }
                TempData["umsg"] = "Please Click on Activation Link Sent to Your Registered Email-ID and Proceed Furthur";
                return RedirectToAction("Index", "AvailableCompanies", new { email = email });
            }
            TempData["msg"] = "Invalid Login!!! Try Again";
            return RedirectToAction("Index", "AvailableCompanies", new { email = email });
            //return Content("<script language='javascript' type='text/javascript'>alert('Invalid Login!!! Try Again');location.href='" + @Url.Action("Index", "AvailableCompanies", new { email = email }) + "'</script>"); // Stays in Same View

            //SqlDataReader value = LoginService.Authenticateuser("redirectuser", email, loginpassword, null, long.Parse(usertype));
            //UserMaster userMaster = new UserMaster();
            //if (value.HasRows)
            //{
            //    userMaster = convert(value);
            //    if (userMaster != null)
            //    {
            //        string userData = JsonConvert.SerializeObject(userMaster);
            //        ValidUser.SetAuthCookie(userData, userMaster.ID);
            //    }
            //    return RedirectToAction("Index", "LandingPage", new { email, usertype, userMaster.User_Site });
            //}
            //return Content("<script language='javascript' type='text/javascript'>alert('Invalid Login!!! Try Again');location.href='" + @Url.Action("Index", "AvailableCompanies", new { email = email }) + "'</script>"); // Stays in Same View
        }

        public UserMaster convert(SqlDataReader sqlDataReader, string userid)
        {
            DataTable dt = new DataTable();
            dt.Load(sqlDataReader);
            List<UserMaster> userMaster = new List<UserMaster>();
            userMaster = (from DataRow row in dt.Rows
                          select new UserMaster()
                          {
                              ID = row["ID"].ToString(),
                              EmailId = row["EmailId"].ToString(),
                              First_Name = row["First_Name"].ToString(),
                              Last_Name = row["Last_Name"].ToString(),
                              DB_Name = row["DB_Name"].ToString(),
                              Created_Date = (DateTime)row["Created_Date"],
                              SubscriptionId = (int)row["SubscriptionId"],
                              UserTypeId = (int)row["UserTypeId"],
                              User_Site = row["User_Site"].ToString(),
                              CompanyName = row["CompanyName"].ToString(),
                              //Phone = row["Phone"].ToString(),
                              //SubscriptionDate = (DateTime)row["SubscriptionDate"],
                              IsActive = (int)row["IsActive"],
                              //Profile_Picture = (byte[])row["Profile_Picture"],
                              //Date_Format = row["Date_Format"].ToString(),
                              //Timezone = row["Timezone"].ToString(),
                              //Currency=row["Currency"].ToString()
                          }).Where(m => m.ID == userid).ToList();
            return userMaster.First();
        }
    }
}