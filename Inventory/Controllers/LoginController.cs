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
                try
                {
                    SqlDataReader value = LoginService.Authenticateuser(null, userMaster.EmailId, null, null, 0);
                    if (value.Read())
                    {
                        int active = int.Parse(value["IsActive"].ToString());
                        value.Close();
                        if (active > 0)
                        {
                            ViewBag.smsg = "Please Click on Activation Link Sent to Your Registered Email-ID and Proceed Furthur";
                            return RedirectToAction("Index", "AvailableCompanies", new { email = userMaster.EmailId });
                        }
                        ViewBag.umsg = "Please Click on Activation Link Sent to Your Registered Email-ID and Proceed Furthur";
                        //return Content("<script language='javascript' type='text/javascript'>alert('Please Click on Activation Link Sent to Your Registered Email-ID and Proceed Furthur');location.href='" + @Url.Action("Index", "Login") + "'</script>"); // Stays in Same View
                    }
                    else
                    {
                        ViewBag.msg = "Please Register";
                        //return JavaScript("overhang()");
                        //return Content("<script language='javascript' type='text/javascript'>" + JavaScript("overhang") +";location.href='" + @Url.Action("Index", "Login") + "'</script>"); // Stays in Same View
                        //return Content("<script language='javascript' type='text/javascript'>alert('Please Register');location.href='" + @Url.Action("Index", "Login") + "'</script>"); // Stays in Same View
                        //ViewBag.invalid = "Confirm Your Email-ID then Login";   
                    }
                }
                catch (Exception)
                {
                    return RedirectToAction("Index", "ServerDown");
                }
                
            }
            if (command == "Insert")
            {
                SqlDataReader value1 = LoginService.Authenticateuser("email", userMaster.EmailId, null, null, 0);
                int companycount = value1.Cast<object>().Count();
                if (companycount <= 2)
                {
                    var data = LoginService.Authenticateuser("checkemail", userMaster.EmailId, null, userMaster.User_Site, 0);
                    if (data.HasRows)
                    {
                        data.Close();
                        ViewBag.msg = "Email Id Already Exists!!! Try Another";
                        //return Content("<script language='javascript' type='text/javascript'>alert('Email Id Already Exists!!! Try Another');location.href='" + @Url.Action("Index", "Login") + "'</script>"); // Stays in Same View
                    }
                    else
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
                        int count = LoginService.CreateUser(userMaster.EmailId, userMaster.First_Name, userMaster.Last_Name, DBname, DateTime.UtcNow, userMaster.Password, Subscription, usertype, userMaster.User_Site, userMaster.CompanyName, userMaster.Phone, SubscriptionDate, 0, activationCode, Profile_Picture, Date_Format, Timezone, Currency, companylogo);
                        if (count > 0)
                        {
                            Email(userMaster.First_Name, userMaster.Last_Name, userMaster.EmailId, activationCode); //Sending Email
                            ViewBag.smsg = "Registration successful. Please click on Activation link which has been sent to your Email to enable your Login Access.";
                            //return Content("<script language='javascript' type='text/javascript'>alert('Registration successful. Please click on Activation link which has been sent to your Email to enable your Login Access.');location.href='" + @Url.Action("Index", "Login") + "'</script>"); // Stays in Same View
                        }
                        ViewBag.msg = "Registration Failed!!!!";
                        //return Content("<script language='javascript' type='text/javascript'>alert('Registration Failed!!!!');location.href='" + @Url.Action("Index", "Login") + "'</script>"); // Stays in Same View
                    }
                }
                ViewBag.umsg = "Free Companies Limit Reached!!!UpGrade Your Account To Add More";
            }
            return View();
        }

        public JsonResult checkemail(string emailid, string site, string type)
        {
            int usertype = (int)LoginService.GetUserTypeId("Owner", 0);
            var data = LoginService.Authenticateuser(type, emailid, null, site, usertype);
            if (data.HasRows)
            {
                data.Close();
                return Json("exists", JsonRequestBehavior.AllowGet); // if email ID already exists
            }
            return Json("unique", JsonRequestBehavior.AllowGet); // if email ID is unique
        }

        public void createdb(string DBname)
        {
            try
            {
                //string DBname = null;
                //SqlDataReader value = LoginService.Authenticateuser("email", Email, null, null, 0);
                //if (value.Read())
                //    DBname = value["User_Site"].ToString() + "_Inventory";
                //value.Close();
                //string sqlConnectionString = @"Integrated Security=False;Initial Catalog=master;Data Source=192.168.0.131;User ID=user_inv;Password=user1234;"; //for local
                string sqlConnectionString = @"Integrated Security=False;Initial Catalog=master;Data Source=183.82.97.220;User ID=user_inv;Password=user1234;"; //for server
                FileInfo File = new FileInfo(Server.MapPath("../Models/May08.sql"));
                string script = File.OpenText().ReadToEnd();
                SqlConnection conn = new SqlConnection(sqlConnectionString);
                Server server = new Server(new ServerConnection(conn));
                var db = new Database(server, DBname);
                db.Create();
                //db.ExecuteNonQuery(script); for local
                //string sqlConnectionString1 = @"Integrated Security=False;Initial Catalog=" + DBname + ";Data Source=192.168.0.131;User ID=user_inv;Password=user1234;"; //for local
                //for server 
                string sqlConnectionString1 = @"Integrated Security=False;Initial Catalog=" + DBname + ";Data Source=183.82.97.220;User ID=user_inv;Password=user1234;";
                SqlConnection conn1 = new SqlConnection(sqlConnectionString1);
                Server server1 = new Server(new ServerConnection(conn1));
                server1.ConnectionContext.ExecuteNonQuery(script);
                conn.Close();
                conn1.Close();
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
                        createdb(value["DB_Name"].ToString()); //creating DB
                        activateemail = LoginService.ActivateEmail(Email, ActivationCode);
                        value.Close();
                    }
                    else
                    {
                        activateemail = LoginService.ActivateEmail(Email, ActivationCode);
                    }
                    if (activateemail < 0)
                        ViewBag.msg = "Email ID Confirmation Failed!!!";
                    return RedirectToAction("Index", "Login");
                        //return Content("<script language='javascript' type='text/javascript'>alert('Email ID Confirmation Failed!!!');location.href='" + @Url.Action("Index", "Login") + "'</script>"); // Stays in Same View
                }
            }
            return View();
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Login");
        }

        [HttpGet]
        public PartialViewResult ProfileProgressPartial()
        {
            var user = (CustomPrinciple)System.Web.HttpContext.Current.User;
            LoginService loginService = new LoginService();
            var profilepic = loginService.GetUserProfile(int.Parse(user.ID)).FirstOrDefault();
            var ownerstaff = LoginService.GetStaff(int.Parse(user.ID), "");
            //ViewBag.profilepic = profilepic[0].Profile_Picture;
            int basic = 0, caddress = 0, uaddress = 0, users = 0, localization = 0;
            //int Warehouse = 0, Vendor = 0, Products = 0;
            string Progress = null, colour = null;
            if (profilepic.First_Name != null && profilepic.Last_Name != null)
            {
                basic = 1;
            }
            if (profilepic.CLine1 != null && profilepic.CLine2 != null && profilepic.Ccity != null && profilepic.Cstate != null && profilepic.Cpostalcode != null && profilepic.Ccountry != null)
            {
                caddress = 1;
            }
            if (profilepic.ULine1 != null && profilepic.ULine2 != null && profilepic.Ucity != null && profilepic.Ustate != null && profilepic.Upostalcode != null && profilepic.Ucountry != null)
            {
                uaddress = 1;
            }
            if (profilepic.Date_Format != null && profilepic.Timezone != null && profilepic.Currency != null)
            {
                localization = 1;
            }
            if (ownerstaff.Read())
            {
                users = 1;
            }
            if (basic > 0) { Progress = ProgressBar.Level1; colour = "Red"; }
            if (basic > 0 && caddress > 0 || basic > 0 && uaddress > 0 || basic > 0 && users > 0 || basic > 0 && localization > 0) { Progress = ProgressBar.Level2; colour = "Blue"; }
            if (basic > 0 && caddress > 0 && uaddress > 0 || basic > 0 && uaddress > 0 && localization > 0 || basic > 0 && localization > 0 && users > 0 || basic > 0 && caddress > 0 && users > 0) { Progress = ProgressBar.Level3; colour = "Orange"; }
            //if (basic > 0 && caddress > 0 && uaddress > 0 && localization > 0 || basic > 0 && uaddress > 0 && localization > 0 && users > 0 || basic > 0 && caddress > 0 && uaddress > 0 && users > 0) { Progress = ProgressBar.Level4; colour = "YellowGreen"; }
            if (basic > 0 && caddress > 0 && uaddress > 0 && localization > 0 || caddress > 0 && uaddress > 0 && localization > 0 && users > 0 || uaddress > 0 && localization > 0 && users > 0 && basic > 0 || localization > 0 && users > 0 && basic > 0 && caddress > 0 || basic > 0 && caddress > 0 && uaddress > 0 && users > 0) { Progress = ProgressBar.Level4; colour = "YellowGreen"; }
            if (basic > 0 && caddress > 0 && uaddress > 0 && localization > 0 && users > 0) { Progress = ProgressBar.Level5; colour = "Green"; }
            ViewBag.Progress = Progress;
            ViewBag.color = colour;
            ownerstaff.Close();
            return PartialView("ProfileProgressPartial", profilepic);
        }
    }
}