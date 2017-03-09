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
                             company_Id=int.Parse(row["company_Id"].ToString()),
                             Company_Name = row["Company_Name"].ToString(),
                             Email = row["Email"].ToString()
                         }).ToList();
            ViewBag.records = vendor;
            ViewBag.company_Id = getMaxCompanyID();
            ViewBag.vendor_Id = getMaxVendorID();
            return View();
            
        }
        [HttpPost]
        public ActionResult Index(Vendor vendor, string command)
        {

            var user = (CustomPrinciple)System.Web.HttpContext.Current.User;//to get loged owner dbname
            if (command=="insertcompany") { 
            int count = VendorService.CompanyInsertRow(vendor.Company_Name, vendor.Email);
            }
            if (command == "insertcontact")
            {
                int counts = VendorService.VendorInsertRow(vendor.company_Id, vendor.Contact_PersonFname, vendor.Contact_PersonLname, vendor.Mobile_No, vendor.LandLine_Num, vendor.Remarks,
                        vendor.Email, vendor.Adhar_Number, vendor.Job_position);
            }
            if (command == "insertaddress")
            {
                int countng = VendorService.VendorAddressInsertRow(vendor.Vendor_Id, vendor.bill_street, vendor.bill_city, vendor.bill_state, vendor.bill_postalcode,
                vendor.bill_country, vendor.ship_street, vendor.ship_city, vendor.ship_state, vendor.ship_postalcode, vendor.ship_country);
            }
            if (command == "updatecompany")
            {
                int county = VendorService.UpdateCompany(vendor.company_Id, vendor.Bank_Acc_Number, vendor.Bank_Name,
                    vendor.Bank_Branch, vendor.Paytym_Number, vendor.emailid);
            }
            if (command == "updatenote")
            {
                int countyy = VendorService.UpdateNotes(vendor.company_Id, vendor.Note);
            }
           
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
        private int getMaxCompanyID()
        {
            int company_Id=0;
            SqlDataReader exec = VendorService.getcompanyId();
            if (exec.Read())
            {
                company_Id = (int)exec["company_Id"];
                            }
                       return company_Id;
        }

        private string getMaxVendorID()
        {
            string vendor_Id = null;
            SqlDataReader exec = VendorService.getvendorId();
            if (exec.Read())
            {
                vendor_Id = exec["vendor_Id"].ToString();
           }
                        return vendor_Id;
        }
    }
}


