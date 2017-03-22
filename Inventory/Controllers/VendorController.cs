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
            ViewBag.contact1 = getcontactDetail();

            var company = getlastinsertedcompany(ViewBag.company_Id);
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
                company_Id = int.Parse(exec["company_Id"].ToString());
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
            int set;
            int set1;
            int set2;
            if (data.Read())
            {
                if (data["Bank_Acc_Number"].ToString() == "")
                    set = 0;
                else
                    set = int.Parse(data["Bank_Acc_Number"].ToString());
                if (data["company_Id"].ToString() == "")
                    set1 = 0;
                else
                    set1 = int.Parse(data["company_Id"].ToString());
                if (data["Mobile_No"].ToString() == "")
                    set2 = 0;
                else
                    set2 = int.Parse(data["Mobile_No"].ToString());

                Vendor vs = new Vendor
                {
                    company_Id = set1,
                    Company_Name = data["Company_Name"].ToString(),
                    Email = data["Email"].ToString(),
                    Bank_Acc_Number = set,//int.Parse(data["Bank_Acc_Number"].ToString()),
                    Bank_Branch = data["Bank_Branch"].ToString(),
                    Bank_Name = data["Bank_Name"].ToString(),
                    IFSC_No = data["IFSC_No"].ToString(),
                    Note = data["Note"].ToString(),
                    Contact_PersonFname = data["Contact_PersonFname"].ToString(),
                    Contact_PersonLname = data["Contact_PersonLname"].ToString(),
                    emailid = data["emailid"].ToString(),
                    Job_position = data["Job_position"].ToString(),
                    Mobile_No = set2,//int.Parse(data["Mobile_No"].ToString()),
                    Adhar_Number = data["Adhar_Number"].ToString(),
                    Vendor_Id = data["Vendor_Id"].ToString(),
                    bill_city = data["bill_city"].ToString(),
                    bill_country = data["bill_country"].ToString(),
                    bill_street = data["bill_street"].ToString(),
                    bill_state = data["bill_state"].ToString(),
                    bill_postalcode = data["bill_postalcode"].ToString(),
                    ship_city = data["ship_city"].ToString(),
                    ship_country = data["ship_country"].ToString(),
                    ship_state = data["ship_state"].ToString(),
                    ship_street = data["ship_street"].ToString(),
                    ship_postalcode = data["ship_postalcode"].ToString(),

                };

                string json = JsonConvert.SerializeObject(vs);
                return Json(json);//new { vs.Company_Name, vs.Email }
            }
            return Json("unique", JsonRequestBehavior.AllowGet);
        }
        public JsonResult updatecompany(int company_Id, string Company_Name, string Email)
        {

            var data = VendorService.UpdateCompany1(company_Id, Company_Name, Email);
            if (data > 0)
            {
                ViewBag.company_Id = company_Id;
                ViewBag.Company_Name = Company_Name;
                ViewBag.Email = Email;
                return Json("sucess");
            }
            return Json("unique", JsonRequestBehavior.AllowGet);
        }
        public JsonResult updatecompanyaddress(int company_Id, string bill_street, string bill_city, string bill_state, string bill_postalcode, string bill_country, string ship_street, string ship_city, string ship_state, string ship_postalcode, string ship_country)
        {
            var data = VendorService.VendorAddressupdateRow(company_Id, bill_street, bill_city, bill_state, bill_postalcode, bill_country, ship_street, ship_city, ship_state, ship_postalcode, ship_country);
            if (data > 0)
            {
                ViewBag.company_Id = company_Id;
                ViewBag.bill_street = bill_street;
                ViewBag.bill_city = bill_city;
                ViewBag.bill_state = bill_state;
                ViewBag.bill_postalcode = bill_postalcode;
                ViewBag.bill_country = bill_country;
                ViewBag.ship_street = ship_street;
                ViewBag.ship_city = ship_city;
                ViewBag.ship_state = ship_state;
                ViewBag.ship_postalcode = ship_postalcode;
                ViewBag.ship_country = ship_country;

                return Json("sucess");
            }
            return Json("unique", JsonRequestBehavior.AllowGet);
        }

        public JsonResult updatecompanybankdetails(int company_Id, long Bank_Acc_Number, string Bank_Name, string Bank_Branch, string IFSC_No)
        {

            var data = VendorService.UpdateCompany(company_Id, Bank_Acc_Number, Bank_Name, Bank_Branch, IFSC_No);
            if (data > 0)
            {
                ViewBag.company_Id = company_Id;
                ViewBag.Bank_Acc_Number = Bank_Acc_Number;
                ViewBag.Bank_Name = Bank_Name;
                ViewBag.Bank_Branch = Bank_Branch;
                ViewBag.IFSC_No = IFSC_No;
                return Json("sucess");
            }
            return Json("unique", JsonRequestBehavior.AllowGet);
        }
        public JsonResult updatecontactdetails(int company_Id, string Contact_PersonFname, string Contact_PersonLname, long Mobile_No,
                          string emailid, string Adhar_Number, string Job_position)
        {
            var data = VendorService.VendorUpdateContact(company_Id, Contact_PersonFname, Contact_PersonLname, Mobile_No, emailid, Adhar_Number, Job_position);
            if (data > 0)
            {
                ViewBag.company_Id = company_Id;
                ViewBag.Contact_PersonFname = Contact_PersonFname;
                ViewBag.Contact_PersonLname = Contact_PersonLname;
                ViewBag.Mobile_No = Mobile_No;
                ViewBag.emailid = emailid;
                ViewBag.Adhar_Number = Adhar_Number;
                ViewBag.Job_position = Job_position;
                return Json("sucess");
            }
            return Json("unique", JsonRequestBehavior.AllowGet);
        }

        public JsonResult updatecompanynote(int company_Id, string Note)
        {
            var data = VendorService.UpdateNotes(company_Id, Note);
            if (data > 0)
            {
                ViewBag.company_Id = company_Id;
                ViewBag.Note = Note;
                return Json("sucess");
            }
            return Json("unique", JsonRequestBehavior.AllowGet);
        }

        public JsonResult savecompany(string Company_Name, string Email)
        {

            var data = VendorService.CompanyInsertRow(Company_Name, Email);
            if (data > 0)
            {
                ViewBag.Company_Name = Company_Name;
                ViewBag.Email = Email;
                return Json("sucess");
            }
            return Json("unique", JsonRequestBehavior.AllowGet);
        }
        public JsonResult savecompanyaddress(int company_Id, string bill_street, string bill_city, string bill_state, string bill_postalcode,
                string bill_country, string ship_street, string ship_city, string ship_state, string ship_postalcode, string ship_country)
        {
            company_Id = getMaxCompanyID();
            var data = VendorService.VendorAddressupdateRow(company_Id, bill_street, bill_city, bill_state, bill_postalcode,
                bill_country, ship_street, ship_city, ship_state, ship_postalcode, ship_country);
            if (data > 0)
            {
                ViewBag.company_Id = company_Id;
                ViewBag.bill_street = bill_street;
                ViewBag.bill_city = bill_city;
                ViewBag.bill_state = bill_state;
                ViewBag.bill_postalcode = bill_postalcode;
                ViewBag.bill_country = bill_country;
                ViewBag.ship_street = ship_street;
                ViewBag.ship_city = ship_city;
                ViewBag.ship_state = ship_state;
                ViewBag.ship_postalcode = ship_postalcode;
                ViewBag.ship_country = ship_country;

                return Json("sucess");
            }
            return Json("unique", JsonRequestBehavior.AllowGet);
        }

        public JsonResult savecompanynote(int company_Id, string Note)
        {
            company_Id = getMaxCompanyID();
            var data = VendorService.UpdateNotes(company_Id, Note);
            if (data > 0)
            {
                //ViewBag.company_Id =getMaxCompanyID();
                ViewBag.company_Id = company_Id;
                ViewBag.Note = Note;
                return Json("sucess");
            }
            return Json("unique", JsonRequestBehavior.AllowGet);
        }
        public JsonResult savecompanybankdetails(int company_Id, long Bank_Acc_Number, string Bank_Name, string Bank_Branch, string IFSC_No)
        {
            company_Id = getMaxCompanyID();
            var data = VendorService.UpdateCompany(company_Id, Bank_Acc_Number, Bank_Name, Bank_Branch, IFSC_No);
            if (data > 0)
            {
                ViewBag.company_Id = company_Id;
                ViewBag.Bank_Acc_Number = Bank_Acc_Number;
                ViewBag.Bank_Name = Bank_Name;
                ViewBag.Bank_Branch = Bank_Branch;
                ViewBag.IFSC_No = IFSC_No;
                return Json("sucess");
            }
            return Json("unique", JsonRequestBehavior.AllowGet);
        }

        public JsonResult savecontactdetails(int company_Id, string Contact_PersonFname, string Contact_PersonLname, long Mobile_No,
                          string emailid, string Adhar_Number, string Job_position)
        {
            company_Id = getMaxCompanyID();
            var data = VendorService.VendorUpdateContact(company_Id, Contact_PersonFname, Contact_PersonLname, Mobile_No, emailid, Adhar_Number, Job_position);
            if (data > 0)
            {
                ViewBag.company_Id = company_Id;
                ViewBag.Contact_PersonFname = Contact_PersonFname;
                ViewBag.Contact_PersonLname = Contact_PersonLname;
                ViewBag.Mobile_No = Mobile_No;
                ViewBag.emailid = emailid;
                ViewBag.Adhar_Number = Adhar_Number;
                ViewBag.Job_position = Job_position;
                return Json("sucess");
            }
            return Json("unique", JsonRequestBehavior.AllowGet);
        }
        
    }

}


