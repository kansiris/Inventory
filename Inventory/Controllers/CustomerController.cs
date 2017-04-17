﻿using Inventory.Content;
using Inventory.Models;
using Inventory.Service;
using Inventory.Utility;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Inventory.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer
        public ActionResult Index()
        {
            ViewBag.country = new SelectList(CountryList().OrderBy(x => x.Value), "Value", "Text");
            var list = AvailableJobPositions();
            if (list != null)
                ViewBag.cusjobpositions = AvailableJobPositions().Select(m => m.cus_Job_position).Distinct();
            ViewBag.jobpositions = "";
            return View();
        }
       //Partial view for loading all customer compines
        public PartialViewResult CustomerCompany()
        {
            var user = (CustomPrinciple)System.Web.HttpContext.Current.User;
            var records = CustomerService.getcuscomapnies(user.DbName);
            var dt = new DataTable();
            dt.Load(records);
            List<Customer> customer = new List<Customer>();
            customer = (from DataRow row in dt.Rows
                      select new Customer()
                      {
                          cus_company_Id = int.Parse(row["cus_company_Id"].ToString()),
                          cus_company_name = row["cus_company_name"].ToString(),
                          cus_email = row["cus_email"].ToString(),
                          cus_logo = row["cus_logo"].ToString()
                      }).OrderByDescending(m => m.cus_company_Id).ToList();
            ViewBag.records = customer;
            return PartialView("CustomerCompany", ViewBag.records);
        }


        [HttpPost]
        public ActionResult UpdatecusCompanyPic(HttpPostedFileBase helpSectionImages, string cus_company_Id)
        {
            if (System.Web.HttpContext.Current.Request.Files.AllKeys.Any())
            {
                var pic = System.Web.HttpContext.Current.Request.Files["helpSectionImages"];
                Image img = Bitmap.FromStream(pic.InputStream);
                ImageConverter _imageConverter = new ImageConverter();
                byte[] cuscompanypic = (byte[])_imageConverter.ConvertTo(img, typeof(byte[]));
                string base64String = Convert.ToBase64String(cuscompanypic);

                return Json(base64String);
            }
            return Json(JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult UpdateCustomerPic(HttpPostedFileBase helpSectionImages, string Customer_Id)
        {
            if (System.Web.HttpContext.Current.Request.Files.AllKeys.Any())
            {
                var pic = System.Web.HttpContext.Current.Request.Files["helpSectionImages"];
                Image img = Bitmap.FromStream(pic.InputStream);
                ImageConverter _imageConverter = new ImageConverter();
                byte[] cuscontactpic = (byte[])_imageConverter.ConvertTo(img, typeof(byte[]));
                string base64String = Convert.ToBase64String(cuscontactpic);
                return Json(base64String);
            }
            return Json(JsonRequestBehavior.AllowGet);
        }
        public class Suggestion
        {
            public string value { get; set; }
            public string data { get; set; }
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
        public void Email(string Customer_contact_Fname, string Customer_contact_Lname, string Email_Id, string activationCode, string PassWord)
        {
            // Designing Email Part
            SendEmail abc = new SendEmail();
            string url = Request.Url.Scheme + "://" + Request.Url.Authority + "/Login/ActivateEmail?ActivationCode=" + activationCode + "&&Email=" + Email_Id;
            string body = "Hello " + Customer_contact_Fname + Customer_contact_Lname + ",";
            body += "<br /><br />Please click the following link to activate your account";
            body += "<br /><a href = '" + url + "'>Click here to activate your account.</a>";
            body += "<br /><br />your temprory password is  " + PassWord;
            body += "<br /><br />Thanks";
            string message = body;
            abc.EmailAvtivation(Email_Id, message, "Account Activation");
        }
        public ActionResult ActivatesEmail(string ActivationCode, string Email_Id, string DBName)
        {
            //Checking Activation code
            if (ActivationCode != null && ActivationCode != "")
            {
                int activateemail = 0;


                activateemail = LoginService.ActivatesEmail(Email_Id, ActivationCode, DBName);

            }
            return View();
        }
        private int getMaxcusCompanyID(string cus_company_name)
        {
            int cus_company_Id = 0;
            var user = (CustomPrinciple)System.Web.HttpContext.Current.User;
            SqlDataReader exec = CustomerService.getcuscompanyId(cus_company_name, user.DbName);
            if (exec.Read() && exec != null)
            {
                cus_company_Id = int.Parse(exec["cus_company_Id"].ToString());
            }
            else
            {
                RedirectToAction("savecompany");
            }

            return cus_company_Id;
        }

        private string getMaxcustomerID()
        {
            string Customer_Id = null;
            var user = (CustomPrinciple)System.Web.HttpContext.Current.User;
            SqlDataReader exec = CustomerService.getCustomerId(user.DbName);
            if (exec.Read())
            {
                Customer_Id = exec["Customer_Id"].ToString();
            }
            return Customer_Id;
        }
        private Customer getlastinsertedcuscompany(int cus_company_Id)
        {
            var user = (CustomPrinciple)System.Web.HttpContext.Current.User;
            SqlDataReader value = CustomerService.getlastinsertedcompany(cus_company_Id, user.DbName);
            DataTable dt = new DataTable();
            dt.Load(value);
            Customer customer = new Customer();
            customer = (from DataRow row in dt.Rows
                      select new Customer()
                      {
                          //company_Id = int.Parse(row["company_Id"].ToString()),
                          cus_company_name = row["cus_company_name"].ToString(),
                          cus_email = row["cus_email"].ToString()
                      }).FirstOrDefault();
            

            return customer;
        }

        public List<Customer> getcuscontactDetail(DataTable dt)
        {
            List<Customer> contact = new List<Customer>();
            contact = (from DataRow row in dt.Rows
                       select new Customer()
                       {
                           Customer_Id = row["Customer_Id"].ToString(),
                           Customer_contact_Fname = row["Customer_contact_Fname"].ToString(),
                           Customer_contact_Lname = row["Customer_contact_Lname"].ToString(),
                           Email_Id = row["Email_Id"].ToString(),
                           image = row["image"].ToString()
                       }).OrderByDescending(m => m.Customer_Id).ToList();
            return contact;
        }

        public JsonResult getAllcusDetails(int cus_company_Id)
        {
            var user = (CustomPrinciple)System.Web.HttpContext.Current.User;
            CustomerService.getlastinsertedcompany(cus_company_Id, user.DbName);
            var data = CustomerService.getAllcusDetails(cus_company_Id, user.DbName);
            
            long set1;
           
            if (data.Read())
            {
                
                if (data["cus_company_Id"].ToString() == "")
                    set1 = 0;
                else
                    set1 = long.Parse(data["cus_company_Id"].ToString());
                
                Customer vs = new Customer
                {
                    cus_company_Id = (int)set1,
                    cus_company_name = data["cus_company_name"].ToString(),
                    cus_email = data["cus_email"].ToString(),
                    //Bank_Acc_Number = data["Bank_Acc_Number"].ToString(),
                    //Bank_Branch = data["Bank_Branch"].ToString(),
                    //Bank_Name = data["Bank_Name"].ToString(),
                    //IFSC_No = data["IFSC_No"].ToString(),
                    cus_Note = data["cus_Note"].ToString(),
                    cus_logo = data["cus_logo"].ToString(),
                    Customer_contact_Fname = data["Customer_contact_Fname"].ToString(),
                    Customer_contact_Lname = data["Customer_contact_Lname"].ToString(),
                    Email_Id = data["Email_Id"].ToString(),
                    cus_Job_position = data["cus_Job_position"].ToString(),
                    Mobile_No = data["Mobile_No"].ToString(),
                    Adhar_Number = data["Adhar_Number"].ToString(),
                    Customer_Id = data["Customer_Id"].ToString(),
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
        public JsonResult updatecuscompany(int cus_company_Id, string cus_company_name, string cus_email, string cus_logo)
        {
            var user = (CustomPrinciple)System.Web.HttpContext.Current.User;
            string s1 = cus_company_name.TrimStart();
            cus_company_name = s1.TrimEnd();
            var data = CustomerService.UpdatecusCompany1(cus_company_Id, cus_company_name, cus_email, cus_logo, user.DbName);
            if (data > 0)
            {
                ViewBag.cus_company_Id = cus_company_Id;
                ViewBag.cus_company_name = cus_company_name;
                ViewBag.cus_email = cus_email;
                return Json("sucess");
            }
            return Json("unique", JsonRequestBehavior.AllowGet);
        }
        public JsonResult updatecuscompanyaddress(int cus_company_Id, string bill_street, string bill_city, string bill_state, string bill_postalcode, string bill_country, string ship_street, string ship_city, string ship_state, string ship_postalcode, string ship_country)
        {
            var user = (CustomPrinciple)System.Web.HttpContext.Current.User;
            var data = CustomerService.CustomerAddressInsertRow(cus_company_Id, bill_street, bill_city, bill_state, bill_postalcode, bill_country, ship_street, ship_city, ship_state, ship_postalcode, ship_country, user.DbName);
            if (data > 0)
            {
                ViewBag.cus_company_Id = cus_company_Id;
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

        //public JsonResult updatecompanybankdetails(int company_Id, string Bank_Acc_Number, string Bank_Name, string Bank_Branch, string IFSC_No)
        //{
        //    var user = (CustomPrinciple)System.Web.HttpContext.Current.User;
        //    var data = VendorService.UpdateCompany(company_Id, Bank_Acc_Number, Bank_Name, Bank_Branch, IFSC_No, user.DbName);
        //    if (data > 0)
        //    {
        //        ViewBag.company_Id = company_Id;
        //        ViewBag.Bank_Acc_Number = Bank_Acc_Number;
        //        ViewBag.Bank_Name = Bank_Name;
        //        ViewBag.Bank_Branch = Bank_Branch;
        //        ViewBag.IFSC_No = IFSC_No;
        //        return Json("sucess");
        //    }
        //    return Json("unique", JsonRequestBehavior.AllowGet);
        //}
        public JsonResult updatecuscontactdetails(string Customer_Id, string Customer_contact_Fname, string Customer_contact_Lname, string Mobile_No,
                          string Email_Id, string Adhar_Number, string cus_Job_position, string image)
        {
            var user = (CustomPrinciple)System.Web.HttpContext.Current.User;
            var data = CustomerService.CustomerUpdateContact(Customer_Id, Customer_contact_Fname, Customer_contact_Lname, Mobile_No, Email_Id, Adhar_Number, cus_Job_position, image, user.DbName);
            if (data > 0)
            {
                ViewBag.Customer_Id = Customer_Id;
                ViewBag.Customer_contact_Fname = Customer_contact_Fname;
                ViewBag.Customer_contact_Lname = Customer_contact_Lname;
                ViewBag.Mobile_No = Mobile_No;
                ViewBag.Email_Id = Email_Id;
                ViewBag.Adhar_Number = Adhar_Number;
                ViewBag.cus_Job_position = cus_Job_position;
                ViewBag.iamge = image;
                return Json("sucess");
            }
            return Json("unique", JsonRequestBehavior.AllowGet);
        }

        public JsonResult updatecuscompanynote(int cus_company_Id, string cus_Note)
        {
            var user = (CustomPrinciple)System.Web.HttpContext.Current.User;
            var data = CustomerService.UpdatecusNotes(cus_company_Id, cus_Note, user.DbName);
            if (data > 0)
            {
                ViewBag.cus_company_Id = cus_company_Id;
                ViewBag.cus_Note = cus_Note;
                return Json("sucess");
            }
            return Json("unique", JsonRequestBehavior.AllowGet);
        }

        public JsonResult savecuscompany(string cus_company_name, string cus_email, string cus_logo)
        {
            var user = (CustomPrinciple)System.Web.HttpContext.Current.User;
            string s1 = cus_company_name.TrimStart();
            cus_company_name = s1.TrimEnd();
            var existingNo = CustomerService.checkcuscompany1(cus_company_name, user.DbName);
            if (existingNo.Read())
            {
                return Json("exists", JsonRequestBehavior.AllowGet);
            }
            else
            {
                var data = CustomerService.CustomerCompanyInsertRow(cus_company_name, cus_email, cus_logo, user.DbName);
                if (data > 0)
                {
                    int cus_company_Id = getMaxcusCompanyID(cus_company_name);
                    ViewBag.cus_company_name = cus_company_name;
                    ViewBag.cus_email = cus_email;
                    ViewBag.cus_logo = cus_logo;
                    var result = new { Result = "sucess", ID = cus_company_Id };
                    return Json(result, JsonRequestBehavior.AllowGet);
                    // return Json("sucess");
                }
            }
            return Json("unique", JsonRequestBehavior.AllowGet);
        }
        
        public JsonResult savecuscompanyaddress(int cus_company_Id, string bill_street, string bill_city, string bill_state, string bill_postalcode,
                string bill_country, string ship_street, string ship_city, string ship_state, string ship_postalcode, string ship_country)
        {
            var user = (CustomPrinciple)System.Web.HttpContext.Current.User;
            var data = CustomerService.CustomerAddressUpdateRow(cus_company_Id, bill_street, bill_city, bill_state, bill_postalcode,
                bill_country, ship_street, ship_city, ship_state, ship_postalcode, ship_country, user.DbName);
            if (data > 0)
            {
                ViewBag.cus_company_Id = cus_company_Id;
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

        public JsonResult savecuscompanynote(int cus_company_Id, string cus_Note)
        {
            var user = (CustomPrinciple)System.Web.HttpContext.Current.User;
            var data = CustomerService.UpdatecusNotes(cus_company_Id, cus_Note, user.DbName);
            if (data > 0)
            {
                ViewBag.cus_company_Id = cus_company_Id;
                ViewBag.cus_Note = cus_Note;
                return Json("sucess");
            }
            return Json("unique", JsonRequestBehavior.AllowGet);
        }
        //public JsonResult savecompanybankdetails(int company_Id, string Bank_Acc_Number, string Bank_Name, string Bank_Branch, string IFSC_No)
        //{
        //    var user = (CustomPrinciple)System.Web.HttpContext.Current.User;
        //    var data = VendorService.UpdateCompany(company_Id, Bank_Acc_Number, Bank_Name, Bank_Branch, IFSC_No, user.DbName);
        //    if (data > 0)
        //    {
        //        ViewBag.company_Id = company_Id;
        //        ViewBag.Bank_Acc_Number = Bank_Acc_Number;
        //        ViewBag.Bank_Name = Bank_Name;
        //        ViewBag.Bank_Branch = Bank_Branch;
        //        ViewBag.IFSC_No = IFSC_No;
        //        return Json("sucess");
        //    }
        //    return Json("unique", JsonRequestBehavior.AllowGet);
        //}

        public JsonResult savecuscontactdetails(int cus_company_Id, string Customer_contact_Fname, string Customer_contact_Lname, string Mobile_No,
                          string Email_Id, string Adhar_Number, string cus_Job_position, string image)
        {
            var user = (CustomPrinciple)System.Web.HttpContext.Current.User;
            List<Vendor> contact = new List<Vendor>();
            var data = CustomerService.CustomerInsertRow(cus_company_Id, Customer_contact_Fname, Customer_contact_Lname, Mobile_No, Email_Id, Adhar_Number, cus_Job_position, image, user.DbName);
            if (data > 0)
            {
                string Customer_Id = getMaxcustomerID();
                ViewBag.Customer_Id = Customer_Id;
                var result = new { Result = "sucess", ID = cus_company_Id };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            return Json("unique", JsonRequestBehavior.AllowGet);
        }

        public JsonResult deletecusRecord(int cus_company_Id)
        {
            var user = (CustomPrinciple)System.Web.HttpContext.Current.User;
            var data = CustomerService.deletecuscompRecord(cus_company_Id, user.DbName);
            if (data > 0)
            {
                ViewBag.cus_company_Id = cus_company_Id;
                return Json("sucess");
            }
            return Json("unique", JsonRequestBehavior.AllowGet);
        }

        public JsonResult deleteCustomer(string Customer_Id)
        {
            var user = (CustomPrinciple)System.Web.HttpContext.Current.User;
            var data = CustomerService.deleteCustomer(Customer_Id, user.DbName);
            if (data > 0)
            {
                ViewBag.Customer_Id = Customer_Id;
                return Json("sucess");
            }
            return Json("unique", JsonRequestBehavior.AllowGet);
        }
        public JsonResult inviteCustomer(string Customer_Id, int cus_company_Id)
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
            string Date_Format = null, Timezone = null, Currency = null, UserSite = null;
            int Subscription = 0;
            DateTime? SubscriptionDate = null;

            SqlDataReader exec = CustomerService.getusermaster(id, user.DbName);
            SqlDataReader exec1 = CustomerService.getCustomerContact(Customer_Id, user.DbName);
            SqlDataReader exec2 = CustomerService.getlastinsertedcompany(cus_company_Id, user.DbName);
            if (exec.Read())
                DBname = exec["DB_Name"].ToString();
            Subscription = int.Parse(exec["Subscriptionid"].ToString());
            if (exec1.Read())
            {
                fname = exec1["Customer_contact_Fname"].ToString();
                lname = exec1["Customer_contact_Lname"].ToString();
                eMail = exec1["Email_Id"].ToString();
                mObile = exec1["Mobile_No"].ToString();
                image = exec1["image"].ToString();
            }
            if (exec2.Read())
            {
                companyname = exec2["cus_company_name"].ToString();
                companylogo = exec2["cus_logo"].ToString();
            }
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
        public PartialViewResult CustomerContact(string id)
        {
            var user = (CustomPrinciple)System.Web.HttpContext.Current.User;
            if (id == null || id == "")
            {
                return PartialView("CustomerContact", null);
            }
            else
            {
               user = (CustomPrinciple)System.Web.HttpContext.Current.User;
                var records = CustomerService.getcuscontactdetail(int.Parse(id), user.DbName);
                var dt = new DataTable();
                dt.Load(records);
                ViewBag.records = getcuscontactDetail(dt);
                ViewBag.id = id;
                return PartialView("CustomerContact", ViewBag.records);
            }
        }
        //for Jobpostions adding
        public JsonResult addPosition(string cus_Job_position, int company_Id)
        {
            var user = (CustomPrinciple)System.Web.HttpContext.Current.User;

            var existingNo = CustomerService.getcusJobposition(cus_Job_position, user.DbName);
            if (existingNo.Read())
            {
                return Json("exists", JsonRequestBehavior.AllowGet);
            }
            else
            {
                var data = CustomerService.insertcusJobposition(cus_Job_position, company_Id, user.DbName);
                if (data > 0)
                {
                    var positions = AvailableJobPositions().Select(m => m.cus_Job_position.TrimEnd());
                    var result = new { Result = "sucess", ID = positions };
                    return Json(result, JsonRequestBehavior.AllowGet);
                    // return Json("sucess");
                }
            }
            return Json("unique", JsonRequestBehavior.AllowGet);
        }


        public List<Customer> AvailableJobPositions()
        {
            if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
            {
                var user = (CustomPrinciple)System.Web.HttpContext.Current.User;
                var records = CustomerService.getallcusJobposition(user.DbName);
                var dt = new DataTable();
                dt.Load(records);
                List<Customer> availpositions = (from DataRow row in dt.Rows select new Customer() { cus_Job_position = row["cus_Job_position"].ToString() }).Distinct().ToList();
                return availpositions;
            }
            return null;
        }
        //to get vendor contact details based on vendor id

        public JsonResult getCustomerContact(string Customer_Id)
        {
            var user = (CustomPrinciple)System.Web.HttpContext.Current.User;
            var data = CustomerService.getCustomerContact(Customer_Id, user.DbName);
           
            if (data.Read())
            {
               
                Customer vs = new Customer
                {
                    Customer_contact_Fname = data["Customer_contact_Fname"].ToString(),
                    Customer_contact_Lname = data["Customer_contact_Lname"].ToString(),
                    Email_Id = data["Email_Id"].ToString(),
                    cus_Job_position = data["cus_Job_position"].ToString(),
                    Mobile_No = data["Mobile_No"].ToString(),
                    Adhar_Number = data["Adhar_Number"].ToString(),
                    Customer_Id = data["Customer_Id"].ToString(),
                    image = data["image"].ToString()

                };

                string json = JsonConvert.SerializeObject(vs);
                return Json(json);
            }
            return Json("unique", JsonRequestBehavior.AllowGet);
        }

       }
}