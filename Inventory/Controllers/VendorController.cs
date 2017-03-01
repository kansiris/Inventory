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

namespace Inventory.Controllers
{
    public class VendorController : Controller
    {
        // GET: Vendor
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(Vendor vendor, string command)
        {

            //var user = (CustomPrinciple)System.Web.HttpContext.Current.User;
            
            int count=VendorService.VendorInsertRow(vendor.Contact_Person1Fname, vendor.Mobile_No, vendor.Bank_Acc_Number,
                vendor.Paytym_Number, vendor.Company_Name, vendor.Contact_Person1Lname, vendor.LandLine_Num, vendor.Bank_Name, vendor.Bank_Branch, vendor.Logo,
                vendor.Remarks, vendor.Contact_Person2Lname, vendor.Contact_Person2Fname, vendor.Email, vendor.Adhar_Number);
            //var user = (CustomPrinciple)System.Web.HttpContext.Current.User;
            string DBName = null;
           // string DBName = user.DbName;
            string PassWord = Membership.GeneratePassword(12, 1);
            DateTime Created_Date = DateTime.Today;
            int SubscriptionId = 0;
            int UserTypeId = 1;
            string User_Site = null;
            DateTime? SubscriptionDate = null;
            int IsActive = 0;
            String Phone = (vendor.Mobile_No).ToString();
            string activationcode = Guid.NewGuid().ToString();
            string Profile_Picture = null;
            string Date_Format = null;
            string Timezone = null;
            
            string Currency = null;
            int counts=LoginService.CreateUser(vendor.Email, vendor.Contact_Person1Fname, vendor.Contact_Person1Lname, DBName, Created_Date, PassWord,
                  SubscriptionId, UserTypeId, User_Site, vendor.Company_Name, Phone, SubscriptionDate, IsActive, activationcode, Profile_Picture, Date_Format, Timezone, Currency);


            Email(vendor.Contact_Person1Fname, vendor.Contact_Person1Lname, vendor.Email, activationcode, PassWord); //Sending Email

            int countin=VendorService.VendorAddressInsertRow(vendor.Job_position, vendor.Note, vendor.bill_street, vendor.bill_city, vendor.bill_state,
                vendor.bill_postalcode, vendor.bill_country, vendor.ship_street, vendor.ship_city, vendor.ship_state, vendor.ship_postalcode, vendor.ship_country);
            //if(count>0)
            //return Content("<script language='javascript' type='text/javascript'>alert('Inserted successfully.');location.href='" + @Url.Action("Index", "vendor") + "'</script>"); // Stays in Same View
            return Content("<script language='javascript' type='text/javascript'>alert('Registration successful. Activation email has been sent to your Email Address.');location.href='" + @Url.Action("Index", "vendor") + "'</script>"); // Stays in Same View                                                                                                                                                                     //createdb();
        }
        public void Email(string First_Name, string Last_Name, string EmailId, string activationCode,string PassWord)
        {
            // Designing Email Part
            SendEmail abc = new SendEmail();
            string url = Request.Url.Scheme + "://" + Request.Url.Authority + "/Login/ActivateEmail?ActivationCode=" + activationCode + "&&Email=" + EmailId;
            string body = "Hello " + First_Name + Last_Name + ",";
            body += "<br /><br />Please click the following link to activate your account";
            body += "<br /><a href = '" + url + "'>Click here to activate your account.</a>";
            body += "<br /><br />your temprory password is  "+ PassWord;
            body += "<br /><br />Thanks";
            string message = body;
            abc.EmailAvtivation(EmailId, message, "Account Activation");
        }
        public ActionResult ActivatesEmail(string ActivationCode, string Email,string DBName)
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
    
    
