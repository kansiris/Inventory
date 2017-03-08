using Inventory.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Inventory.Service;
using Inventory.Utility;
using System.Web.Security;
using Inventory.Content;
using System.Data;

namespace Inventory.Controllers
{
    public class VendorController : Controller
    {
        // GET: Vendor
        public ActionResult Index()
        {
            //var user1 = (CustomPrinciple)System.Web.HttpContext.Current.User;
            SqlDataReader value = VendorService.getcomapnies();
            DataTable dt = new DataTable();
            dt.Load(value);
            List<Vendor> vendor = new List<Vendor>();
            vendor = (from DataRow row in dt.Rows
                         select new Vendor()
                         {
                             Company_Name = row["Company_Name"].ToString(),
                             Email = row["Email"].ToString()
                         }).ToList();
            ViewBag.records = vendor;
            return View();
            
        }
        [HttpPost]
        public ActionResult Index(Vendor vendor, string command)
        {

            var user = (CustomPrinciple)System.Web.HttpContext.Current.User;//to get loged owner dbname

            int count = VendorService.CompanyInsertRow(vendor.Company_Name, vendor.Email);

            if (count > 0) 
                return Content("<script language='javascript' type='text/javascript'>alert('Company Added successfully');location.href='" + @Url.Action("Index", "Vendor") + "'</script>"); // Stays in Same View
                        return Content("<script language='javascript' type='text/javascript'>alert('Failed!!!');location.href='" + @Url.Action("Index", "Vendor") + "'</script>");
        } 
        public void Email(string First_Name, string Last_Name, string EmailId, string activationCode, string PassWord)
        {
            // Designing Email Part
            SendEmail abc = new SendEmail();
            string url = Request.Url.Scheme + "://" + Request.Url.Authority + "/Login/ActivateEmail?ActivationCode=" + activationCode + "&&Email=" + EmailId;
            string body = "Hello " + First_Name + Last_Name + ",";
            body += "<br /><br />Please click the following link to activate your account";
            body += "<br /><a href = '" + url + "'>Click here to activate your account.</a>";
            body += "<br /><br />your temprory password is  " + PassWord;
            body += "<br /><br />Thanks";
            string message = body;
            abc.EmailAvtivation(EmailId, message, "Account Activation");
        }
        public ActionResult ActivatesEmail(string ActivationCode, string Email, string DBName)
        {
            //Checking Activation code
            if (ActivationCode != null && ActivationCode != "")
            {
                int activateemail = 0;


                activateemail = LoginService.ActivatesEmail(Email, ActivationCode, DBName);

            }
            return View();
        }
    }
}


