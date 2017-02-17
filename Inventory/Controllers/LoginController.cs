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

namespace Inventory.Controllers
{
    public class LoginController : Controller
    {
        ValuesController1 valuesController1 = new ValuesController1();
        //LoginService loginService = new LoginService();
        public ActionResult Index()
        {
            createdb();
            DateTime utcTime = DateTime.UtcNow;
            TimeZoneInfo tzi = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
            DateTime localTime = TimeZoneInfo.ConvertTimeFromUtc(utcTime, tzi); // convert from utc to local
            //ViewBag.country = RegionInfo.CurrentRegion.DisplayName;
            ViewBag.country = localTime;
            ViewBag.server = DateTime.UtcNow;
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
                    ViewBag.invalid = "Invalid Credentials";
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
                int count = LoginService.CreateUser(userMaster.EmailId, userMaster.First_Name, userMaster.Last_Name, DBname, DateTime.UtcNow, userMaster.Password, Subscription, usertype, userMaster.User_Site, userMaster.CompanyName, userMaster.Phone, SubscriptionDate, 0, activationCode);
                if (count > 0)
                {
                    Email(userMaster.First_Name, userMaster.Last_Name, userMaster.EmailId, activationCode); //Sending Email
                    return Content("<script language='javascript' type='text/javascript'>alert('Registration successful. Activation email has been sent to your Email Address.');location.href='" + @Url.Action("Index", "Login") + "'</script>"); // Stays in Same View
                }
                return Content("<script language='javascript' type='text/javascript'>alert('Registration Failed!!!!');location.href='" + @Url.Action("Index", "Login") + "'</script>"); // Stays in Same View
                //createdb();
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

        public void createdb()
        {
            //< add name = "APIContext" connectionString = "server=.;database=MaaAahwanam;Integrated Security=True; MultipleActiveResultSets=True;" providerName = "System.Data.SqlClient" />
            string sqlConnectionString = @"Integrated Security=True;Initial Catalog=master;Data Source=.";
            FileInfo File = new FileInfo("E:\\Project\\Inventory\\Inventory\\Inventory\\Models\\finalscript.sql");
            string script = File.OpenText().ReadToEnd();
            SqlConnection conn = new SqlConnection(sqlConnectionString);
            Server server = new Server(new ServerConnection(conn));
            var db = new Database(server, "sampledb");
            db.Create();
            db.ExecuteNonQuery(script);
            //server.ConnectionContext.ExecuteNonQuery(script);
            //script = script.Replace("DB_NAME_MDF", File).Replace("DB_NAME_LDF", appPathLog);
            //ExecSql(script, sqlConnectionString, "sampledb");
            //server.ConnectionContext.ExecuteNonQuery(script,sqlConnectionString,"sample");
            //server.ConnectionContext.ex
            //File.OpenText().Close();
        }

        public void ExecSql(string sql, string connectionString, string dataBaseNameToPrepend)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                Server server = new Server(new ServerConnection(conn));
                var db = new Database(server, dataBaseNameToPrepend);
                db.Create();
                db.ExecuteNonQuery(sql);
               
            }
        }

        public void Email(string First_Name, string Last_Name, string EmailId, string activationCode)
        {
            // Designing Email Part
            SendEmail abc = new SendEmail();
            string body = "Hello " + First_Name + Last_Name + ",";
            body += "<br /><br />Please click the following link to activate your account";
            body += "<br /><a href = '" + Request.Url.AbsoluteUri.Replace(Request.Url.AbsoluteUri, Request.Url.AbsoluteUri + "Login/ActivateEmail?ActivationCode=" + activationCode + "&&Email=" + EmailId) + "'>Click here to activate your account.</a>";
            body += "<br /><br />Thanks";
            string message = body;
            abc.EmailAvtivation(EmailId, message, "Account Activation");
        }

        public ActionResult ActivateEmail(string ActivationCode, string Email)
        {
            //Checking Activation code
            if (ActivationCode != null && ActivationCode != "")
            {
                SqlDataReader value = LoginService.Authenticateuser("email", Email, null, null, 0);
                if (value.Read())
                {
                    if (value["activationcode"].ToString() == ActivationCode)
                    {
                        int activateemail = LoginService.ActivateEmail(Email, 0, DateTime.UtcNow, 1, null);
                        if (activateemail > 0)
                            return View();
                    }
                }
            }
            return View();
        }
    }
}





//string connLocal = "Data Source=.;Database=InventoryMaster;Integrated Security=False;User ID=user_inv;Password=123456;"; //"Data Source=(local); Integrated Security=SSPI;"
//SqlConnection connection = new SqlConnection(connLocal);
//Server server = new Server(new ServerConnection(connection));
//Database db = new Database(server, "test");
//db.Create();
//connection.Close();
//return "";