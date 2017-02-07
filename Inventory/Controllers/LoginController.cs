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
//using Micrososft.SqlServer.Management.Common;
using System.Data.Entity;
using Microsoft.SqlServer.Management.Smo;

namespace Inventory.Controllers
{
    public class LoginController : Controller
    {
        ValuesController1 valuesController1 = new ValuesController1();
        //LoginService loginService = new LoginService();
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(UserMaster userMaster, string command)
        {
            if (command == "Authenticate")
            {
                //string check = valuesController1.Get(login.Email_ID, login.Password);
                //if(check == "success")
                //    return RedirectToAction("Index", "Dashboard");
                //else
                //    ViewBag.invalid = "Invalid Credentials";
                SqlDataReader value = LoginService.Authenticateuser(null, userMaster.EmailId, userMaster.Password,null,0);
                if (value.HasRows)
                {
                    //var val = Request.Url.GetLeftPart(UriPartial.Authority) + Request.ApplicationPath;
                    return RedirectToAction("Index", "AvailableCompanies",new { email = userMaster.EmailId } );
                }
                else
                {
                    ViewBag.invalid = "Invalid Credentials";
                }
            }
            if (command == "Insert")
            {
                string DBname = userMaster.EmailId.Split('@')[0] + ".Inventory";
                int Subscription = (int)LoginService.getsubscriptionid("Free Member");
                int usertype = (int)LoginService.GetUserTypeId("Owner",0);
                int count = LoginService.CreateUser(userMaster.EmailId, userMaster.First_Name, userMaster.Last_Name, DBname, DateTime.UtcNow, userMaster.Password, Subscription, usertype, userMaster.User_Site, userMaster.CompanyName, userMaster.Phone);
                //createdb();
            }
            return View();
        }

        public JsonResult checkemail(string emailid,string site,string type)
        {
            var data = LoginService.Authenticateuser(type, emailid, null,site,0);
            if (data.HasRows)
                return Json("exists", JsonRequestBehavior.AllowGet); // if email ID already exists
            return Json("unique", JsonRequestBehavior.AllowGet); // if email ID is unique
        }

        //public string createdb()
        //{
        //    //string connLocal = "Data Source=XSIINDU;Database=InventoryMaster;Integrated Security=False;User ID=user_inv;Password=123456;"; //"Data Source=(local); Integrated Security=SSPI;"
        //    //SqlConnection connection = new SqlConnection(connLocal);
        //    //Server server = new Server(new ServerConnection(connection));
        //    //Database db = new Database(server, "test");
        //    //db.Create();
        //    //connection.Close();
        //    //return "";
        //}
    }
}