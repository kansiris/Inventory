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
using Newtonsoft.Json;

namespace Inventory.Controllers
{
    public class VendorController : Controller
    {
        // GET: Vendor
        public ActionResult Index(string status, string cid)
        {
            SqlDataReader value = VendorService.getcomapnies();
            DataTable dt = new DataTable();
            dt.Load(value);
            List<Vendor> vendor = new List<Vendor>();
            vendor = (from DataRow row in dt.Rows
                      select new Vendor()
                      {
                          company_Id = int.Parse(row["company_Id"].ToString()),
                          Company_Name = row["Company_Name"].ToString(),
                          Email = row["Email"].ToString()
                      }).OrderByDescending(m => m.company_Id).ToList();
            ViewBag.records = vendor;
            ViewBag.company_Id = getMaxCompanyID();
            ViewBag.vendor_Id = getMaxVendorID();
            ViewBag.contact1  = getcontactDetail();
           
            var company = getlastinsertedcompany();
            if (status == "complete")
            {
                ViewBag.company = 1;
                ViewBag.companyname = company.Company_Name;
                ViewBag.email = company.Email;
            }
                    
            return View();
        }
        [HttpPost]
        public ActionResult Index(Vendor vendor, string command)
        {
 
//company insertion           
            if (command == "Save")
            {
                int count = VendorService.CompanyInsertRow(vendor.Company_Name, vendor.Email);
                if (count > 0)
                {
                    ViewBag.company_Id = getMaxCompanyID();
                    ViewBag.vendor_Id = getMaxVendorID();
                    var company = getlastinsertedcompany();
                    int count2 = VendorService.VendorAddressInsertRow(company.company_Id, null, null, null, null, null, null, null, null, null, null);
                    return Content("<script language='javascript' type='text/javascript'>alert('Company Added successfully!!!!click on Additional fields');location.href='" + @Url.Action("Index", "Vendor", new { status = "complete" }) + "'</script>");
                }
                return Content("<script language='javascript' type='text/javascript'>alert('Failed!!!');location.href='" + @Url.Action("Index", "Vendor", new { status = "complete" }) + "'</script>");
            }

 //contact person insertion
            if (command == "insertcontact")
            {
                ViewBag.company_Id = getMaxCompanyID();
                ViewBag.vendor_Id = getMaxVendorID();
                var company = getlastinsertedcompany();
                int counts = VendorService.VendorInsertRow(ViewBag.company_Id, vendor.Contact_PersonFname, vendor.Contact_PersonLname, vendor.Mobile_No,
                         vendor.emailid, vendor.Adhar_Number, vendor.Job_position);
                if (counts > 0)
                {
                    return Content("<script language='javascript' type='text/javascript'>alert('contact person updated successfully');location.href='" + @Url.Action("Index", "Vendor", new { status = "complete" }) + "'</script>");
                }
                return Content("<script language='javascript' type='text/javascript'>alert('Failed!!!');location.href='" + @Url.Action("Index", "Vendor", new { status = "complete" }) + "'</script>");
            }
            
//address insertion
            if (command == "saveaddress")
            {
                int countng = VendorService.VendorAddressupdateRow(vendor.company_Id, vendor.bill_street, vendor.bill_city, vendor.bill_state, vendor.bill_postalcode,
                vendor.bill_country, vendor.ship_street, vendor.ship_city, vendor.ship_state, vendor.ship_postalcode, vendor.ship_country);
                if (countng > 0)
                {
                    return Content("<script language='javascript' type='text/javascript'>alert('Address updated successfully');location.href='" + @Url.Action("Index", "Vendor", new { status = "complete" }) + "'</script>");
                }
                return Content("<script language='javascript' type='text/javascript'>alert('Failed!!!');location.href='" + @Url.Action("Index", "Vendor", new { status = "complete" }) + "'</script>");
            }

//bankdetails insertion
            if (command == "updatecompany")
            {

                int county = VendorService.UpdateCompany(vendor.company_Id, vendor.Bank_Acc_Number, vendor.Bank_Name,
                    vendor.Bank_Branch, vendor.IFSC_No, vendor.Email);
                if (county > 0)
                {
                    return Content("<script language='javascript' type='text/javascript'>alert('Bankdetails updated successfully');location.href='" + @Url.Action("Index", "Vendor", new { status = "complete" }) + "'</script>");
                }
                return Content("<script language='javascript' type='text/javascript'>alert('Failed!!!');location.href='" + @Url.Action("Index", "Vendor", new { status = "complete" }) + "'</script>");
            }

//note insertion
            if (command == "updatenote")
            {

                int countyy = VendorService.UpdateNotes(vendor.company_Id, vendor.Note);
                if (countyy > 0)
                {
                    return Content("<script language='javascript' type='text/javascript'>alert('Notes updated successfully');location.href='" + @Url.Action("Index", "Vendor", new { status = "complete" }) + "'</script>");
                }
                return Content("<script language='javascript' type='text/javascript'>alert('Failed!!!');location.href='" + @Url.Action("Index", "Vendor", new { status = "complete" }) + "'</script>");
            }

 //updating company on edit click 
            if (command == "update")
            {

                int county = VendorService.UpdateCompany1(vendor.company_Id, vendor.Company_Name, vendor.Email);
                if (county > 0)
                {
                    return Content("<script language='javascript' type='text/javascript'>alert('Bankdetails updated successfully');location.href='" + @Url.Action("Index", "Vendor", new { status = "complete" }) + "'</script>");
                }
                return Content("<script language='javascript' type='text/javascript'>alert('Failed!!!');location.href='" + @Url.Action("Index", "Vendor", new { status = "complete" }) + "'</script>");
            }
            

 // updating address on edit click   
 if (command == "updateaddress")
            {

                int countng = VendorService.VendorAddressupdateRow(vendor.company_Id, vendor.bill_street, vendor.bill_city, vendor.bill_state, vendor.bill_postalcode,
                vendor.bill_country, vendor.ship_street, vendor.ship_city, vendor.ship_state, vendor.ship_postalcode, vendor.ship_country);
                if (countng > 0)
                {
                    return Content("<script language='javascript' type='text/javascript'>alert('Address updated successfully');location.href='" + @Url.Action("Index", "Vendor", new { status = "" }) + "'</script>");
                }
                return Content("<script language='javascript' type='text/javascript'>alert('Failed!!!');location.href='" + @Url.Action("Index", "Vendor", new { status = "" }) + "'</script>");
            }

            return View();
        }

        // used methods        
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
            int company_Id = 0;
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
        private Vendor getlastinsertedcompany(int company_Id)
        {
            //int company_Id = ViewBag.company_Id;
            SqlDataReader value = VendorService.getlastinsertedcompany(company_Id);
            DataTable dt = new DataTable();
            dt.Load(value);
            Vendor vendor = new Vendor();
            vendor = (from DataRow row in dt.Rows
                      select new Vendor()
                      {
                          //company_Id = int.Parse(row["company_Id"].ToString()),
                          Company_Name = row["Company_Name"].ToString(),
                          Email = row["Email"].ToString()
                      }).FirstOrDefault();
            //ViewBag.records = vendor;

            return vendor;
        }

        private List<Vendor> getcontactDetail()
        {
            int company_Id = ViewBag.company_Id;
            SqlDataReader value = VendorService.getcontactdetail(company_Id);
            DataTable dt = new DataTable();
            dt.Load(value);
            List<Vendor> contact = new List<Vendor>();
            VendorService.getcontactdetail(company_Id);
            contact = (from DataRow row in dt.Rows
                       select new Vendor()
                       {
                           Contact_PersonFname = row["Contact_PersonFname"].ToString(),
                           Contact_PersonLname = row["Contact_PersonLname"].ToString(),
                           emailid = row["emailid"].ToString()
                       }).ToList();
            return contact;
        }


        public JsonResult getAllDetails(int company_Id)
        {
            VendorService.getlastinsertedcompany(company_Id);
            var data = VendorService.getAllDetails(company_Id);

            if (data.Read())
            {
                Vendor vs = new Vendor {
                    company_Id=int.Parse(data["company_Id"].ToString()),
                    Company_Name = data["Company_Name"].ToString(),
                    Email = data["Email"].ToString(),
                    bill_street = data["bill_street"].ToString(),
                    bill_city = data["bill_city"].ToString(),
                    bill_state = data["bill_state"].ToString(),
                    bill_postalcode = data["bill_postalcode"].ToString(),
                    bill_country = data["bill_country"].ToString(),
                    ship_street = data["ship_street"].ToString(),
                    ship_city = data["ship_city"].ToString(),
                    ship_state = data["ship_state"].ToString(),
                    ship_postalcode = data["ship_postalcode"].ToString(),
                    ship_country = data["ship_country"].ToString(),
                    Contact_PersonFname = data["Contact_PersonFname"].ToString(),
                    Contact_PersonLname = data["Contact_PersonLname"].ToString(),
                    Mobile_No = int.Parse(data["Mobile_No"].ToString()),
                    emailid = data["emailid"].ToString(),
                    Adhar_Number = data["Adhar_Number"].ToString(),
                    Job_position = data["Job_position"].ToString(),
                    Bank_Acc_Number = int.Parse(data["Bank_Acc_Number"].ToString()),
                    Bank_Name = data["Bank_Name"].ToString(),
                    Bank_Branch = data["Bank_Branch"].ToString(),
                    IFSC_No = data["IFSC_No"].ToString(),
                    //Remarks = data["Remarks"].ToString(),
                    Note = data["Note"].ToString(),
                };
                string json = JsonConvert.SerializeObject(vs);
                return Json(json);//new { vs.Company_Name, vs.Email }
            }
            return Json("unique", JsonRequestBehavior.AllowGet);
        }

    }
    
    }


