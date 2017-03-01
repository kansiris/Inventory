using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Inventory.Service;
using Inventory.Models;
using System.Data.SqlClient;
using System.Web.Routing;
using System.Web.Caching;
using System.Globalization;
using Microsoft.Web.Administration;
using Microsoft.SqlServer.Management.Smo;
using System.IO;
using Microsoft.SqlServer.Management.Common;
using System.Data;
using System.Web.Hosting;
using Inventory.Utility;
using System.Web.Security;

namespace Inventory.Controllers
{
    public class LoginController : Controller
    {
        ValuesController1 valuesController1 = new ValuesController1();
        //LoginService loginService = new LoginService();
        public ActionResult Index()
        {
            DateTime utcTime = DateTime.UtcNow;
            TimeZoneInfo tzi = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
            DateTime localTime = TimeZoneInfo.ConvertTimeFromUtc(utcTime, tzi); // convert from utc to local
            //ViewBag.country = RegionInfo.CurrentRegion.DisplayName;
            ViewBag.country = localTime;
            ViewBag.server = DateTime.UtcNow;
            //string url = (Request.Url.ToString()).TrimEnd(Request.RawUrl.ToCharArray());
            return View();
        }
        [HttpPost]
        public ActionResult Index(UserMaster userMaster, string command, string plan)
        {
            if (command == "Authenticate")
            {
                //for API
                //string check = valuesController1.Get(login.Email_ID, login.Password);
                //if(check == "success")
                //    return RedirectToAction("Index", "Dashboard");
                //else
                //    ViewBag.invalid = "Invalid Credentials";
                SqlDataReader value = LoginService.Authenticateuser(null, userMaster.EmailId, null, null, 0);
                if (value.HasRows)
                {
                    return RedirectToAction("Index", "AvailableCompanies", new { email = userMaster.EmailId });
                }
                else
                {
                    ViewBag.invalid = "Confirm Your Email-ID then Login";
                }
            }
            if (command == "Insert")
            {
                int Subscription;
                DateTime? SubscriptionDate = null;
                string activationCode = Guid.NewGuid().ToString();//Auto Generated code
                string DBname = userMaster.EmailId.Split('@')[0] + ".Inventory"; //Assigning Particular DB Name
                if (plan != null)
                    Subscription = (int)LoginService.getsubscriptionid(plan);
                else
                    Subscription = (int)LoginService.getsubscriptionid("Free Member");
                int usertype = (int)LoginService.GetUserTypeId("Owner", 0);
                string Profile_Picture = null;
                string Date_Format = null;
                string Timezone = null;
                string Currency = null;
                int count = LoginService.CreateUser(userMaster.EmailId, userMaster.First_Name, userMaster.Last_Name, DBname, DateTime.UtcNow, userMaster.Password, Subscription, usertype, userMaster.User_Site, userMaster.CompanyName, userMaster.Phone, SubscriptionDate, 0, activationCode, Profile_Picture, Date_Format, Timezone, Currency);
                if (count > 0)
                {
                    Email(userMaster.First_Name, userMaster.Last_Name, userMaster.EmailId, activationCode); //Sending Email
                    return Content("<script language='javascript' type='text/javascript'>alert('Registration successful. Please click on Activation link which has been sent to your Email to enable your Login Access.');location.href='" + @Url.Action("Index", "Login") + "'</script>"); // Stays in Same View
                }
                return Content("<script language='javascript' type='text/javascript'>alert('Registration Failed!!!!');location.href='" + @Url.Action("Index", "Login") + "'</script>"); // Stays in Same View
            }
            return View();
        }

        public JsonResult checkemail(string emailid, string site, string type)
        {
            int usertype = (int)LoginService.GetUserTypeId("Owner", 0);
            var data = LoginService.Authenticateuser(type, emailid, null, site, usertype);
            if (data.HasRows)
                return Json("exists", JsonRequestBehavior.AllowGet); // if email ID already exists
            return Json("unique", JsonRequestBehavior.AllowGet); // if email ID is unique
        }

        public void createdb(string Email)
        {
            string DBname = Email.Split('@')[0] + ".Inventory";
            //string sqlConnectionString = @"Integrated Security=False;Initial Catalog=master;Data Source=192.168.0.131;User ID=user_inv;Password=123456;"; //for local
            //< add name = "DbConnection" connectionString = "Data Source=183.82.97.220;Database=Inventory;Integrated Security=False;User ID=user_inv;Password=123456;" providerName = "System.Data.SqlClient" />
            string sqlConnectionString = @"Integrated Security=False;Initial Catalog=master;Data Source=183.82.97.220;User ID=user_inv;Password=123456;"; //for server
            FileInfo File = new FileInfo(Server.MapPath("../Models/createdbscript.sql"));
            string script = File.OpenText().ReadToEnd();
            SqlConnection conn = new SqlConnection(sqlConnectionString);
            Server server = new Server(new ServerConnection(conn));
            var db = new Database(server, DBname);
            db.Create();
            //db.ExecuteNonQuery(script);
            string sqlConnectionString1 = @"Integrated Security=False;Initial Catalog=" + DBname + ";Data Source=183.82.97.220;User ID=user_inv;Password=123456;"; //for server
            SqlConnection conn1 = new SqlConnection(sqlConnectionString1);
            Server server1 = new Server(new ServerConnection(conn1));
            server1.ConnectionContext.ExecuteNonQuery(script);
        }
        public void Email(string First_Name, string Last_Name, string EmailId, string activationCode)
        {
            // Designing Email Part
            SendEmail abc = new SendEmail();
            string url = Request.Url.Scheme + "://" + Request.Url.Authority + "/Login/ActivateEmail?ActivationCode=" + activationCode + "&&Email=" + EmailId;
            string body = "Hello " + First_Name + " " + Last_Name + ",";
            body += "<br /><br />Please click the following link to activate your account";
            body += "<br /><a href = '" + url + "'>Click here to activate your account.</a>";
            body += "<br /><br />Thanks";
            string message = body;
            abc.EmailAvtivation(EmailId, message, "Account Activation");
        }

        public ActionResult ActivateEmail(string ActivationCode, string Email)
        {
            //Checking Activation code
            if (ActivationCode != null && ActivationCode != "")
            {
                int activateemail = 0;
                SqlDataReader value = LoginService.getuserrecord(Email, ActivationCode); //retrieves particular user record
                int usertype = (int)LoginService.GetUserTypeId("owner", 0);//get owner user type id
                if (value.Read())
                {
                    if (value["activationcode"].ToString() == ActivationCode && (int)value["UserTypeId"] == usertype) // if usertype is owner
                    {
                        createdb(Email); //creating DB
                        activateemail = LoginService.ActivateEmail(Email, ActivationCode);
                    }
                    else
                    {
                        activateemail = LoginService.ActivateEmail(Email, ActivationCode);
                    }
                    if (activateemail < 0)
                        return Content("<script language='javascript' type='text/javascript'>alert('Email ID Confirmation Failed!!!');location.href='" + @Url.Action("Index", "Login") + "'</script>"); // Stays in Same View
                }
            }
            return View();
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Login");
        }
    }
}


//&& usertype == (int)value["UserTypeId"]