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
        public ActionResult Index(Vendor vendor)
        {

            var user = (CustomPrinciple)System.Web.HttpContext.Current.User;//to get loged owner dbname

            if (Request.Form["submitbutton0"] != null)
            {
                int countng = VendorService.CompanyInsertRow(vendor.Company_Name, vendor.Bank_Acc_Number, vendor.Bank_Name, vendor.Bank_Branch,
                                        vendor.Paytym_Number, vendor.Email, vendor.Logo);
               
            }

            if (Request.Form["submitbutton1"] != null)
            {
                int countin = VendorService.VendorAddressInsertRow(vendor.bill_street, vendor.bill_city, vendor.bill_state,
                vendor.bill_postalcode, vendor.bill_country, vendor.ship_street, vendor.ship_city, vendor.ship_state, vendor.ship_postalcode, vendor.ship_country);
            }

            if (Request.Form["submitbutton5"] != null)
                {
                int count = VendorService.VendorInsertRow(vendor.Contact_PersonFname, vendor.Contact_PersonLname, vendor.Mobile_No,
               vendor.LandLine_Num, vendor.Remarks,vendor.emailid, vendor.Adhar_Number, vendor.Job_position);
              
            }

            if (Request.Form["submitbutton6"] != null)
            {
                int company_Id = vendor.company_Id;
                int count = VendorService.UpdateCompany(vendor.company_Id,vendor.Company_Name,vendor.Bank_Acc_Number,vendor.Bank_Name,vendor.Bank_Branch,vendor.Paytym_Number,vendor.Email,vendor.Logo);
            }


                //string DBName = user.DbName;
                //string PassWord = Membership.GeneratePassword(12, 1);
                //DateTime Created_Date = DateTime.Today;
                //int SubscriptionId = 0;
                //int UserTypeId = 1;
                //string User_Site = null;
                //string[] words = (vendor.Company_Name).Split(' ');
                //string Site = words[0];
                //var data = LoginService.getusersite(Site);
                //if (data.HasRows)
                //    User_Site = Site + user.ID; //append owner id to user site 
                //else
                //User_Site = Site;
                //DateTime? SubscriptionDate = null;
                //int IsActive = 0;
                //string Phone = (vendor.Mobile_No).ToString();
                //string activationcode = Guid.NewGuid().ToString();
                //string Profile_Picture = null;
                //string Date_Format = null;
                //string Timezone = null;

                //string Currency = null;
                //int counts = LoginService.CreateUser(vendor.Email, vendor.Contact_PersonFname, vendor.Contact_PersonLname, DBName, Created_Date, PassWord,
                //      SubscriptionId, UserTypeId, User_Site, vendor.Company_Name, Phone, SubscriptionDate, IsActive, activationcode, Profile_Picture, Date_Format, Timezone, Currency);


                //Email(vendor.Contact_PersonFname, vendor.Contact_PersonLname, vendor.Email, activationcode, PassWord); //Sending Email

                return Content("<script language='javascript' type='text/javascript'>alert('Inserted successfully.');location.href='" + @Url.Action("Index", "vendor") + "'</script>"); // Stays in Same View                                                                                                                                                                     //createdb();
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


