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
            ViewBag.country = new SelectList(CountryList().OrderBy(x => x.Value), "Value", "Text");
            var list = AvailableJobPositions();
            if(list != null)
                    ViewBag.jobpositions = AvailableJobPositions().Select(m => m.Job_position).Distinct();
            else
            ViewBag.jobpositions = "";
            if (TempData["msg"] != null)
            {
                ViewBag.msg = TempData["msg"];
            }
            if (TempData["smsg"] != null)
            {
                ViewBag.smsg = TempData["smsg"];
            }
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
            //cultureList.OrderBy(x => x).ToList();
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
        private int getMaxCompanyID(string Company_Name)
        {
            if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
            {
                int company_Id = 0;
            var user = (CustomPrinciple)System.Web.HttpContext.Current.User;
            SqlDataReader exec = VendorService.getcompanyId(Company_Name,user.DbName);
            if (exec.Read() && exec != null)
            {
                company_Id = int.Parse(exec["company_Id"].ToString());
            }
            else
            {
                RedirectToAction("savecompany");
            }
                exec.Close();

            return company_Id;
        }
            return 0;
        }
        private string getMaxVendorID()
        {
            if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
            {
                string vendor_Id = null;
                var user = (CustomPrinciple)System.Web.HttpContext.Current.User;
                SqlDataReader exec = VendorService.getvendorId(user.DbName);
                if (exec.Read())
                {
                    vendor_Id = exec["vendor_Id"].ToString();
                }
                exec.Close();
                return vendor_Id;
            }
            return null;
        }
        private Vendor getlastinsertedcompany(int company_Id)
        {
            if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
            {
                var user = (CustomPrinciple)System.Web.HttpContext.Current.User;
                SqlDataReader value = VendorService.getlastinsertedcompany(company_Id, user.DbName);
                DataTable dt = new DataTable();
                dt.Load(value);
                Vendor vendor = new Vendor();
                vendor = (from DataRow row in dt.Rows
                          select new Vendor()
                          {

                              Company_Name = row["Company_Name"].ToString(),
                              Email = row["Email"].ToString()
                          }).FirstOrDefault();
                return vendor;
            }
            return null;
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
                           emailid = row["emailid"].ToString(),
                           image= row["image"].ToString()
                       }).OrderByDescending(m => m.Vendor_Id).ToList();
            return contact;
        }

        public JsonResult getAllDetails(int company_Id)
        {
            if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
            {
                var user = (CustomPrinciple)System.Web.HttpContext.Current.User;
            VendorService.getlastinsertedcompany(company_Id, user.DbName);
            var data = VendorService.getAllDetails(company_Id, user.DbName);
           
            long set1;
            
            if (data.Read())
            {
                
                if (data["company_Id"].ToString() == "")
                    set1 = 0;
                else
                    set1 = long.Parse(data["company_Id"].ToString());
               

                Vendor vs = new Vendor
                {
                    company_Id = (int)set1,
                    Company_Name = data["Company_Name"].ToString(),
                    Email = data["Email"].ToString(),
                    Bank_Acc_Number = data["Bank_Acc_Number"].ToString(),
                    Bank_Branch = data["Bank_Branch"].ToString(),
                    Bank_Name = data["Bank_Name"].ToString(),
                    IFSC_No = data["IFSC_No"].ToString(),
                    Note = data["Note"].ToString(),
                    logo=data["logo"].ToString(),
                    Contact_PersonFname = data["Contact_PersonFname"].ToString(),
                    Contact_PersonLname = data["Contact_PersonLname"].ToString(),
                    emailid = data["emailid"].ToString(),
                    Job_position = data["Job_position"].ToString(),
                    Mobile_No = data["Mobile_No"].ToString(),
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
            return Json(null);
        }
        public JsonResult updatecompany(int company_Id, string Company_Name, string Email,string logo)
        {
            if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
            {
                var user = (CustomPrinciple)System.Web.HttpContext.Current.User;
            string s1 = Company_Name.TrimStart();
            Company_Name = s1.TrimEnd();
            var data = VendorService.UpdateCompany1(company_Id, Company_Name, Email, logo, user.DbName);
            if (data > 0)
            {
                ViewBag.company_Id = company_Id;
                ViewBag.Company_Name = Company_Name;
                ViewBag.Email = Email;
                return Json("sucess");
            }
            return Json("unique", JsonRequestBehavior.AllowGet);
        }
            return Json(null);
        }
        public JsonResult updatecompanyaddress(int company_Id, string bill_street, string bill_city, string bill_state, string bill_postalcode, string bill_country, string ship_street, string ship_city, string ship_state, string ship_postalcode, string ship_country)
        {
            if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
            {
                var user = (CustomPrinciple)System.Web.HttpContext.Current.User;
            var data = VendorService.VendorAddressupdateRow(company_Id, bill_street, bill_city, bill_state, bill_postalcode, bill_country, ship_street, ship_city, ship_state, ship_postalcode, ship_country, user.DbName);
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
            return Json(null);
        }
        public JsonResult updatecompanybankdetails(int company_Id, string Bank_Acc_Number, string Bank_Name, string Bank_Branch, string IFSC_No)
        {
            if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
            {
                var user = (CustomPrinciple)System.Web.HttpContext.Current.User;
            var data = VendorService.UpdateCompany(company_Id, Bank_Acc_Number, Bank_Name, Bank_Branch, IFSC_No, user.DbName);
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
            return Json(null);
        }
        public JsonResult updatecontactdetails(string Vendor_Id, string Contact_PersonFname, string Contact_PersonLname, string Mobile_No,
                          string emailid, string Adhar_Number, string Job_position,string image)
        {
            if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
            {
                var user = (CustomPrinciple)System.Web.HttpContext.Current.User;
            var data = VendorService.VendorUpdateContact(Vendor_Id, Contact_PersonFname, Contact_PersonLname, Mobile_No, emailid, Adhar_Number, Job_position,image, user.DbName);
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
            return Json(null);
        }
        public JsonResult updatecompanynote(int company_Id, string Note)
        {
            if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
            {
                var user = (CustomPrinciple)System.Web.HttpContext.Current.User;
            var data = VendorService.UpdateNotes(company_Id, Note, user.DbName);
            if (data > 0)
            {
                ViewBag.company_Id = company_Id;
                ViewBag.Note = Note;
                return Json("sucess");
            }
            return Json("unique", JsonRequestBehavior.AllowGet);
        }
            return Json(null);
        }
        public JsonResult savecompany(string Company_Name, string Email, string logo)
        {
            if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
            {
                var user = (CustomPrinciple)System.Web.HttpContext.Current.User;
                string s1 = Company_Name.TrimStart();
                Company_Name = s1.TrimEnd();
                var existingNo = VendorService.checkcompany1(Company_Name, user.DbName);
                if (existingNo.Read())
                {
                    return Json("exists", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var data = VendorService.CompanyInsertRow(Company_Name, Email, logo, user.DbName);
                    if (data > 0)
                    {
                        int company_Id = getMaxCompanyID(Company_Name);
                        ViewBag.Company_Name = Company_Name;
                        ViewBag.Email = Email;
                        ViewBag.logo = logo;
                        var result = new { Result = "sucess", ID = company_Id };
                        return Json(result, JsonRequestBehavior.AllowGet);
                        // return Json("sucess");
                    }
                }
                return Json("unique", JsonRequestBehavior.AllowGet);
            }
            return Json(null);
        }
        public class Suggestion
        {
            public string value { get; set; }
            public string data { get; set; }
        }
        public JsonResult savecompanyaddress(int company_Id, string bill_street, string bill_city, string bill_state, string bill_postalcode,
                string bill_country, string ship_street, string ship_city, string ship_state, string ship_postalcode, string ship_country)
        {
            if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
            {
                var user = (CustomPrinciple)System.Web.HttpContext.Current.User;
            var data = VendorService.VendorAddressupdateRow(company_Id, bill_street, bill_city, bill_state, bill_postalcode,
                bill_country, ship_street, ship_city, ship_state, ship_postalcode, ship_country, user.DbName);
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
            return Json(null);
        }

        public JsonResult savecompanynote(int company_Id, string Note)
        {
            if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
            {
                var user = (CustomPrinciple)System.Web.HttpContext.Current.User;
            var data = VendorService.UpdateNotes(company_Id, Note, user.DbName);
            if (data > 0)
            {
                ViewBag.company_Id = company_Id;
                ViewBag.Note = Note;
                return Json("sucess");
            }
            return Json("unique", JsonRequestBehavior.AllowGet);
        }
            return Json(null);
        }
        public JsonResult savecompanybankdetails(int company_Id, string Bank_Acc_Number, string Bank_Name, string Bank_Branch, string IFSC_No)
        {
            if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
            {
                var user = (CustomPrinciple)System.Web.HttpContext.Current.User;
                var data = VendorService.UpdateCompany(company_Id, Bank_Acc_Number, Bank_Name, Bank_Branch, IFSC_No, user.DbName);
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
            return Json(null);
        }

        public JsonResult savecontactdetails(int company_Id, string Contact_PersonFname, string Contact_PersonLname, string Mobile_No,
                          string emailid, string Adhar_Number, string Job_position, string image)
        {
            if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
            {
                var user = (CustomPrinciple)System.Web.HttpContext.Current.User;
                List<Vendor> contact = new List<Vendor>();
                var data = VendorService.VendorInsertRow(company_Id, Contact_PersonFname, Contact_PersonLname, Mobile_No, emailid, Adhar_Number, Job_position, image, user.DbName);
                if (data > 0)
                {
                    string Vendor_Id = getMaxVendorID();
                    ViewBag.Vendor_Id = Vendor_Id;
                    var result = new { Result = "sucess", ID = Vendor_Id };
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                return Json("unique", JsonRequestBehavior.AllowGet);
            }
            return Json(null);
        }
        public ActionResult deleteRecord(int company_Id, string status)
        {
            if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
            {
                var user = (CustomPrinciple)System.Web.HttpContext.Current.User;
                var data = VendorService.deleteRecord(company_Id, status, user.DbName);
                if (status == "Active")
                    TempData["smsg"] = "Now Company with " + company_Id + " is " + status + "";
                else
                    TempData["msg"] = "Now Company with " + company_Id + " is " + status + "";
                return RedirectToAction("Index", "Vendor");
            }

            
            return View();
        }

        public JsonResult deleteVendor(string Vendor_Id)
        {
            if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
            {
                var user = (CustomPrinciple)System.Web.HttpContext.Current.User;
                var data = VendorService.deleteVendor(Vendor_Id, user.DbName);
                if (data > 0)
                {
                    ViewBag.Vendor_Id = Vendor_Id;
                    return Json("sucess");
                }
                return Json("unique", JsonRequestBehavior.AllowGet);
            }
            return Json(null);
        }
        public JsonResult inviteVendor(string Vendor_Id, int company_Id)
        {
            if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
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
                string mObile = null;
                string activationCode = Guid.NewGuid().ToString();
                int usertype = (int)LoginService.GetUserTypeId("Vendor", 0);
                string Date_Format = null, Timezone = null, Currency = null, UserSite = null;
                int Subscription = 0;
                DateTime? SubscriptionDate = null;

                SqlDataReader exec = VendorService.getusermaster(id, user.DbName);
                SqlDataReader exec1 = VendorService.getVendorContact(Vendor_Id, user.DbName);
                SqlDataReader exec2 = VendorService.getlastinsertedcompany(company_Id, user.DbName);
                if (exec.Read())
                    DBname = exec["DB_Name"].ToString();
                Subscription = int.Parse(exec["Subscriptionid"].ToString());
                exec.Close();
                if (exec1.Read())
                {
                    fname = exec1["Contact_PersonFname"].ToString();
                    lname = exec1["Contact_PersonLname"].ToString();
                    eMail = exec1["emailid"].ToString();
                    mObile = exec1["Mobile_No"].ToString();
                    image = exec1["image"].ToString();
                }
                exec1.Close();
                if (exec2.Read())
                {
                    companyname = exec2["Company_Name"].ToString();
                    companylogo = exec2["logo"].ToString();
                }
                exec2.Close();
                UserSite = companyname.Trim();
                var data = LoginService.Authenticateuser("checkemail1", eMail, null, UserSite, 0);
                if (data.HasRows)
                {
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
            return Json(null);
        }
        public PartialViewResult VendorContact(string id)
        {
            var user = (CustomPrinciple)System.Web.HttpContext.Current.User;
            if (id == null || id=="")
            {
                return PartialView("VendorRecords", null);
            }
            else
            {
                user = (CustomPrinciple)System.Web.HttpContext.Current.User;
                var records = VendorService.getcontactdetail(int.Parse(id), user.DbName);
                var dt = new DataTable();
                dt.Load(records);
                ViewBag.records = getcontactDetail(dt);
                ViewBag.id = id;
                return PartialView("VendorRecords", ViewBag.records);
            }
        }
        //now writting
        public JsonResult addPosition(string Job_position, int company_Id)
        {
            if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
            {
                var user = (CustomPrinciple)System.Web.HttpContext.Current.User;
                var existingNo = VendorService.getjobposition(Job_position, user.DbName);
                if (existingNo.Read())
                {
                    return Json("exists", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var data = VendorService.insertjobposition(Job_position, company_Id, user.DbName);
                    if (data > 0)
                    {
                        var positions = AvailableJobPositions().Select(m => m.Job_position.TrimEnd());
                        var result = new { Result = "sucess", ID = positions };
                        return Json(result, JsonRequestBehavior.AllowGet);
                        // return Json("sucess");
                    }
                }
                return Json("unique", JsonRequestBehavior.AllowGet);
            }
            return Json(null);
        }

        public List<Vendor> AvailableJobPositions()
        {
            if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
            {
                var user = (CustomPrinciple)System.Web.HttpContext.Current.User;
                var records = VendorService.getalljobposition(user.DbName);
                var dt = new DataTable();
                dt.Load(records);
                List<Vendor> availpositions = (from DataRow row in dt.Rows select new Vendor() { Job_position = row["Job_position"].ToString() }).Distinct().ToList();
                return availpositions;
            }
            return null;
        }
        //to get vendor contact details based on vendor id

        public JsonResult getVendorContact(string Vendor_Id)
        {

            if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
            {
                var user = (CustomPrinciple)System.Web.HttpContext.Current.User;
                var data = VendorService.getVendorContact(Vendor_Id, user.DbName);
                if (data.Read())
                {
                    Vendor vs = new Vendor
                    {
                        Contact_PersonFname = data["Contact_PersonFname"].ToString(),
                        Contact_PersonLname = data["Contact_PersonLname"].ToString(),
                        emailid = data["emailid"].ToString(),
                        Job_position = data["Job_position"].ToString(),
                        Mobile_No = data["Mobile_No"].ToString(),
                        Adhar_Number = data["Adhar_Number"].ToString(),
                        Vendor_Id = data["Vendor_Id"].ToString(),
                        image = data["image"].ToString()

                    };

                    string json = JsonConvert.SerializeObject(vs);
                    return Json(json);
                }
                return Json("unique", JsonRequestBehavior.AllowGet);
            }
            return Json(null);
        }
        public PartialViewResult VendorCompany()
        {
            if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
            {
                var user = (CustomPrinciple)System.Web.HttpContext.Current.User;
                var records = VendorService.getcomapnies(user.DbName);
                var dt = new DataTable();
                dt.Load(records);
                List<Vendor> vendor = new List<Vendor>();
                vendor = (from DataRow row in dt.Rows
                          select new Vendor()
                          {
                              company_Id = int.Parse(row["company_Id"].ToString()),
                              Company_Name = row["Company_Name"].ToString(),
                              Email = row["Email"].ToString(),
                              logo = row["logo"].ToString(),
                              status= row["status"].ToString()
                          }).OrderByDescending(m => m.company_Id).ToList();
                ViewBag.records = vendor;
                return PartialView("VendorCompany", ViewBag.records);
            }
            return PartialView("VendorCompany", null);
            }
               
    }

}


