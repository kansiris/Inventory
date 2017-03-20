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
//using Microsoft.SqlServer.Management.Smo;
using System.IO;
//using Microsoft.SqlServer.Management.Common;
using System.Data;
using System.Web.Hosting;
using Inventory.Utility;
using System.Web.Security;
using Microsoft.AspNet.Identity;
using Inventory.Content;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer.Management.Common;

namespace Inventory.Controllers
{
    public class LoginController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(UserMaster userMaster, string command, string plan)
        {
            if (command == "Authenticate")
            {
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
                string DBname = userMaster.User_Site + "_Inventory"; //Assigning Particular DB Name
                if (plan != null)
                    Subscription = (int)LoginService.getsubscriptionid(plan);
                else
                    Subscription = (int)LoginService.getsubscriptionid("Free Member");
                int usertype = (int)LoginService.GetUserTypeId("Owner", 0);
                string Profile_Picture = null, Date_Format = null, Timezone = null, Currency = null, companylogo = null;
                int count = LoginService.CreateUser(userMaster.EmailId, userMaster.First_Name, userMaster.Last_Name, DBname, DateTime.UtcNow, userMaster.Password, Subscription, usertype, userMaster.User_Site, userMaster.CompanyName, userMaster.Phone, SubscriptionDate, 0, activationCode, Profile_Picture, Date_Format, Timezone, Currency,companylogo);
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
            try
            {
                string DBname = null;
                SqlDataReader value = LoginService.Authenticateuser("email", Email, null, null, 0);
                if (value.Read())
                DBname = value["User_Site"].ToString() + "_Inventory"; 
                //string sqlConnectionString = @"Integrated Security=False;Initial Catalog=master;Data Source=192.168.0.131;User ID=user_inv;Password=user123;"; //for local
                string sqlConnectionString = @"Integrated Security=False;Initial Catalog=master;Data Source=183.82.97.220;User ID=user_inv;Password=user123;"; //for server
                FileInfo File = new FileInfo(Server.MapPath("../Models/mar14.sql"));
                string script = File.OpenText().ReadToEnd();
                SqlConnection conn = new SqlConnection(sqlConnectionString);
                Server server = new Server(new ServerConnection(conn));
                var db = new Database(server, DBname);
                db.Create();
                //db.ExecuteNonQuery(script); for local
                //string sqlConnectionString1 = @"Integrated Security=False;Initial Catalog=" + DBname + ";Data Source=192.168.0.131;User ID=user_inv;Password=123456;"; //for local
                //for server 
                string sqlConnectionString1 = @"Integrated Security=False;Initial Catalog=" + DBname + ";Data Source=183.82.97.220;User ID=user_inv;Password=user123;"; 
                SqlConnection conn1 = new SqlConnection(sqlConnectionString1); 
                Server server1 = new Server(new ServerConnection(conn1));
                server1.ConnectionContext.ExecuteNonQuery(script);
            }
            catch (Exception ex)
            {
                throw ex.GetBaseException();
            }
        }

        public void Email(string First_Name, string Last_Name, string EmailId, string activationCode)
        {
            // Designing Email Part
            SendEmail abc = new SendEmail();
            string url = Request.Url.Scheme + "://" + Request.Url.Authority + "/Login/ActivateEmail?ActivationCode=" + activationCode + "&&Email=" + EmailId;
            //string message = body;
            //StreamReader reader = new StreamReader(Server.MapPath("../Content/mailer.html"));
            //string readFile = reader.ReadToEnd();
            FileInfo File = new FileInfo(Server.MapPath("/Content/mailer.html"));
            string readFile = File.OpenText().ReadToEnd();
            string body = "Hello " + First_Name + " " + Last_Name + ",";
            body += "<br /><br />Please click the following link to activate your account";
            body += "<br /><a href = '" + url + "'>Click here to activate your account.</a>";
            body += "<br /><br />Thanks";
            string message = readFile + body;
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

        [ChildActionOnly]
        public PartialViewResult ProfileProgressPartial()
        {
            var user = (CustomPrinciple)System.Web.HttpContext.Current.User;
            LoginService loginService = new LoginService();
            //string id = HttpContext.User.Identity.Name;
            var profilepic = loginService.GetUserProfile(int.Parse(user.ID));
            ViewBag.profilepic = profilepic[0].Profile_Picture;
            int Warehouse = 0, Vendor = 0, Products = 0;
            string Progress = null, colour = null;
            SqlDataReader record = LoginService.GetProfileProgress(user.DbName);
            if (record.Read())
            {
                Warehouse = (int)record["W"];
                Vendor = (int)record["v"];
                Products = (int)record["i"];
            }
            if (Warehouse == 0 && Vendor == 0 && Products == 0)
            { Progress = ProgressBar.Level1; colour = "Red"; }
            if (Warehouse > 0 || Vendor > 0 || Products > 0)
            { Progress = ProgressBar.Level2; colour = "Orange"; }
            if (Warehouse > 0 && Vendor > 0 || Vendor > 0 && Products > 0 || Warehouse > 0 && Products > 0)
            { Progress = ProgressBar.Level3; colour = "YellowGreen"; }
            if (Warehouse > 0 && Vendor > 0 && Products > 0)
            { Progress = ProgressBar.Level4; colour = "Green"; }
            ViewBag.Progress = Progress;
            ViewBag.color = colour;
            return PartialView("ProfileProgressPartial");
        }
    }
}

