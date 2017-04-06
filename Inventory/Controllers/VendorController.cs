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
using System.Drawing;
using System.Globalization;

namespace Inventory.Controllers
{
    public class VendorController : Controller
    {
        // GET: Vendor
        public ActionResult Index()
        {
            //ViewBag.vendor_Id = getMaxVendorID();
            ViewBag.country = new SelectList(CountryList(), "Value", "Text");
            return View();
        }
        //companypic upload
        [HttpPost]
        public ActionResult UpdateCompanyPic(HttpPostedFileBase helpSectionImages, string company_Id)
        {
            if (System.Web.HttpContext.Current.Request.Files.AllKeys.Any())
            {
                var pic = System.Web.HttpContext.Current.Request.Files["helpSectionImages"];
                Image img = Bitmap.FromStream(pic.InputStream);
                ImageConverter _imageConverter = new ImageConverter();
                byte[] companypic = (byte[])_imageConverter.ConvertTo(img, typeof(byte[]));
                string base64String = Convert.ToBase64String(companypic);
                //int count = VendorService.updatecompanyprofile(int.Parse(company_Id), base64String);
                return Json(base64String);
            }
            return Json(JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult UpdateVendorPic(HttpPostedFileBase helpSectionImages, string Vendor_Id)
        {
            if (System.Web.HttpContext.Current.Request.Files.AllKeys.Any())
            {
                var pic = System.Web.HttpContext.Current.Request.Files["helpSectionImages"];
                Image img = Bitmap.FromStream(pic.InputStream);
                ImageConverter _imageConverter = new ImageConverter();
                byte[] companypic = (byte[])_imageConverter.ConvertTo(img, typeof(byte[]));
                string base64String = Convert.ToBase64String(companypic);
                 return Json(base64String);
            }
            return Json(JsonRequestBehavior.AllowGet);
        }
        

        private List<SelectListItem> CountryList()
        {
            List<SelectListItem> cultureList = new List<SelectListItem>();
            CultureInfo[] getCultureInfo = CultureInfo.GetCultures(CultureTypes.SpecificCultures);
            if (getCultureInfo.Count() > 0)
            {
                foreach (CultureInfo cultureInfo in getCultureInfo)
                {
                    RegionInfo getRegionInfo = new RegionInfo(cultureInfo.LCID);
                    var newitem = new SelectListItem { Text = getRegionInfo.EnglishName, Value = getRegionInfo.EnglishName };
                    cultureList.Add(newitem);
                }
            }
            return cultureList;
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
            if (exec.Read() && exec != null)
            {
                company_Id = int.Parse(exec["company_Id"].ToString());
            }
            else
            {
                RedirectToAction("savecompany");
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

        public List<Vendor> getcontactDetail(DataTable dt)
        {
            List<Vendor> contact = new List<Vendor>();
            contact = (from DataRow row in dt.Rows
                       select new Vendor()
                       {
                           Vendor_Id = row["Vendor_Id"].ToString(),
                           Contact_PersonFname = row["Contact_PersonFname"].ToString(),
                           Contact_PersonLname = row["Contact_PersonLname"].ToString(),
                           emailid = row["emailid"].ToString()
                       }).OrderByDescending(m => m.Vendor_Id).ToList();
            return contact;
        }

        public JsonResult getAllDetails(int company_Id)
        {

            VendorService.getlastinsertedcompany(company_Id);
            var data = VendorService.getAllDetails(company_Id);
            long set;
            long set1;
            long set2;
            if (data.Read())
            {
                if (data["Bank_Acc_Number"].ToString() == "")
                    set = 0;
                else
                    set = long.Parse(data["Bank_Acc_Number"].ToString());
                if (data["company_Id"].ToString() == "")
                    set1 = 0;
                else
                    set1 = long.Parse(data["company_Id"].ToString());
                if (data["Mobile_No"].ToString() == "")
                    set2 = 0;
                else
                    set2 = long.Parse(data["Mobile_No"].ToString());

                Vendor vs = new Vendor
                {
                    company_Id = (int)set1,
                    Company_Name = data["Company_Name"].ToString(),
                    Email = data["Email"].ToString(),
                    Bank_Acc_Number = (int)set,//int.Parse(data["Bank_Acc_Number"].ToString()),
                    Bank_Branch = data["Bank_Branch"].ToString(),
                    Bank_Name = data["Bank_Name"].ToString(),
                    IFSC_No = data["IFSC_No"].ToString(),
                    Note = data["Note"].ToString(),
                    logo=data["logo"].ToString(),
                    Contact_PersonFname = data["Contact_PersonFname"].ToString(),
                    Contact_PersonLname = data["Contact_PersonLname"].ToString(),
                    emailid = data["emailid"].ToString(),
                    Job_position = data["Job_position"].ToString(),
                    Mobile_No = (int)set2,//int.Parse(data["Mobile_No"].ToString()),
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
        public JsonResult updatecompany(int company_Id, string Company_Name, string Email,string logo)
        {
            string s1 = Company_Name.TrimStart();
            Company_Name = s1.TrimEnd();
            var data = VendorService.UpdateCompany1(company_Id, Company_Name, Email, logo);
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
        public JsonResult updatecontactdetails(string Vendor_Id, string Contact_PersonFname, string Contact_PersonLname, long Mobile_No,
                          string emailid, string Adhar_Number, string Job_position,string image)
        {
            var data = VendorService.VendorUpdateContact(Vendor_Id, Contact_PersonFname, Contact_PersonLname, Mobile_No, emailid, Adhar_Number, Job_position,image);
            if (data > 0)
            {
                ViewBag.Vendor_Id = Vendor_Id;
                ViewBag.Contact_PersonFname = Contact_PersonFname;
                ViewBag.Contact_PersonLname = Contact_PersonLname;
                ViewBag.Mobile_No = Mobile_No;
                ViewBag.emailid = emailid;
                ViewBag.Adhar_Number = Adhar_Number;
                ViewBag.Job_position = Job_position;
                ViewBag.iamge = image;
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

        public JsonResult savecompany(string Company_Name, string Email, string logo)
        {
            string s1 = Company_Name.TrimStart();
            Company_Name = s1.TrimEnd();
            var existingNo = VendorService.checkcompany1(Company_Name);
            if (existingNo.Read())
            {
                return Json("exists", JsonRequestBehavior.AllowGet);
            }
            else { 
            var data = VendorService.CompanyInsertRow(Company_Name, Email, logo);
            if (data > 0)
            {
                int company_Id = getMaxCompanyID();
                ViewBag.Company_Name = Company_Name;
                ViewBag.Email = Email;
                ViewBag.logo = logo;
                var result = new { Result = "sucess",ID = company_Id };
                return Json(result, JsonRequestBehavior.AllowGet);
                // return Json("sucess");
            }
            }
            return Json("unique", JsonRequestBehavior.AllowGet);
        }
        public class Suggestion
        {
            public string value { get; set; }
            public string data { get; set; }
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
                          string emailid, string Adhar_Number, string Job_position,string image)
        {
                        List<Vendor> contact = new List<Vendor>();
            var data = VendorService.VendorInsertRow(company_Id, Contact_PersonFname, Contact_PersonLname, Mobile_No, emailid, Adhar_Number, Job_position,image);
            if (data > 0)
            {
                string Vendor_Id = getMaxVendorID();
                ViewBag.Vendor_Id = Vendor_Id;
                var result = new { Result = "sucess", ID = Vendor_Id };
                return Json(result, JsonRequestBehavior.AllowGet);
                }
            return Json("unique", JsonRequestBehavior.AllowGet);
        }

        public JsonResult deleteRecord(int company_Id)
        {

            var data = VendorService.deleteRecord(company_Id);
            if (data > 0)
            {
                ViewBag.company_Id = company_Id;
                return Json("sucess");
            }
            return Json("unique", JsonRequestBehavior.AllowGet);
        }

        public JsonResult deleteVendor(string Vendor_Id)
        {

            var data = VendorService.deleteVendor(Vendor_Id);
            if (data > 0)
            {
                ViewBag.Vendor_Id = Vendor_Id;
                return Json("sucess");
            }
            return Json("unique", JsonRequestBehavior.AllowGet);
        }
        public JsonResult inviteVendor(string Vendor_Id, int company_Id)
        {
            var user = (CustomPrinciple)System.Web.HttpContext.Current.User;
            string id = user.ID;
            string DBname = null;
            string fname = null;
            string lname = null;
            string eMail = null;
            string image = null;
            string companyname = null;
            string companylogo = null;
            string Password = "ABC@123456";
        //    private static string RandomString(int Size) {
        //    string input = "abcdefghijklmnopqrstuvwxyz0123456789";
        //    var chars = Enumerable.Range(0, Size).Select(x => input[random.Next(0, input.Length)]);
        //    return new string(chars.ToArray());
        //}
        
            string mObile = null;
            string activationCode = Guid.NewGuid().ToString();
            int usertype = (int)LoginService.GetUserTypeId("Vendor", 0);
            string Date_Format = null, Timezone = null, Currency = null, UserSite=null;
            int Subscription=0;
            DateTime? SubscriptionDate = null;

            SqlDataReader exec = VendorService.getusermaster(id);
            SqlDataReader exec1 = VendorService.getVendorContact(Vendor_Id);
            SqlDataReader exec2 = VendorService.getlastinsertedcompany(company_Id);
            if (exec.Read())
                DBname = exec["DB_Name"].ToString();
            Subscription= int.Parse(exec["Subscriptionid"].ToString());
             if (exec1.Read())
            {
                fname= exec1["Contact_PersonFname"].ToString();
                lname = exec1["Contact_PersonLname"].ToString();
                eMail = exec1["emailid"].ToString();
                mObile = exec1["Mobile_No"].ToString();
                image= exec1["image"].ToString();
            }
            if (exec2.Read())
            {
                companyname = exec2["Company_Name"].ToString();
                companylogo = exec2["logo"].ToString();
            }
            UserSite = companyname.Trim();
            var data = LoginService.Authenticateuser("checkemail1", eMail,null,UserSite,0);
            if (data.HasRows) { 
                return Json("Exists");
            }
            else
            {
            int count = LoginService.CreateUser(eMail, fname, lname, DBname, DateTime.UtcNow, Password, Subscription, usertype, UserSite, companyname, mObile, SubscriptionDate, 0, activationCode, image, Date_Format, Timezone, Currency, companylogo);
            if (count > 0)
            {
                    Email(fname, lname, eMail, activationCode, Password); //Sending Email
                    return Json("sucess");
                }
            }
            return Json("unique", JsonRequestBehavior.AllowGet);
        }
        public PartialViewResult VendorContact(string id)
        {
            if (id == null)
            {
                return PartialView("VendorRecords", null);
            }
            else
            {
                var records = VendorService.getcontactdetail(int.Parse(id));
                var dt = new DataTable();
                dt.Load(records);
                ViewBag.records = getcontactDetail(dt);
                ViewBag.id = id;
                return PartialView("VendorRecords", ViewBag.records);
            }
        }

        //to get vendor contact details based on vendor id

        public JsonResult getVendorContact(string Vendor_Id)
        {
            var data = VendorService.getVendorContact(Vendor_Id);
            long set;
            if (data.Read())
            {
                if (data["Mobile_No"].ToString() == "")
                    set = 0;
                else
                    set = long.Parse(data["Mobile_No"].ToString());

                Vendor vs = new Vendor
                {
                    Contact_PersonFname = data["Contact_PersonFname"].ToString(),
                    Contact_PersonLname = data["Contact_PersonLname"].ToString(),
                    emailid = data["emailid"].ToString(),
                    Job_position = data["Job_position"].ToString(),
                    Mobile_No = (int)set,
                    Adhar_Number = data["Adhar_Number"].ToString(),
                    Vendor_Id = data["Vendor_Id"].ToString(),
                    image = data["image"].ToString()

                };

                string json = JsonConvert.SerializeObject(vs);
                return Json(json);
            }
            return Json("unique", JsonRequestBehavior.AllowGet);
        }
       
        public PartialViewResult VendorCompany()
        {
            var records = VendorService.getcomapnies();
            var dt = new DataTable();
            dt.Load(records);
            List<Vendor> vendor = new List<Vendor>();
            vendor = (from DataRow row in dt.Rows
                      select new Vendor()
                      {
                          company_Id = int.Parse(row["company_Id"].ToString()),
                          Company_Name = row["Company_Name"].ToString(),
                          Email = row["Email"].ToString(),
                          logo = row["logo"].ToString()
                      }).OrderByDescending(m => m.company_Id).ToList();
            ViewBag.records = vendor;
            return PartialView("VendorCompany", ViewBag.records);
            }
               
    }

}


