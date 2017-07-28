using Inventory.Content;
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
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;


namespace Inventory.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer
        LoginService loginService = new LoginService();
        public ActionResult Index()
        {
            if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
            {
                var user = (CustomPrinciple)System.Web.HttpContext.Current.User;
                var userdetails = loginService.GetUserProfile(int.Parse(user.ID)).FirstOrDefault();
                ViewBag.typeofuser = LoginService.GetUserTypeId("", (int)userdetails.UserTypeId).ToString();
                ViewBag.country = new SelectList(CountryList().OrderBy(x => x.Value), "Value", "Text");
                var list = AvailableJobPositions();
                if (list != null)
                    ViewBag.cusjobpositions = AvailableJobPositions().Select(m => m.cus_Job_position).Distinct();
                ViewBag.jobpositions = "";
                if (TempData["msg"] != null)
                {
                    ViewBag.msg = TempData["msg"];
                }
                if (TempData["smsg"] != null)
                {
                    ViewBag.smsg = TempData["smsg"];
                }
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
            return View();
        }
        //Partial view for loading all customer compines
        public PartialViewResult CustomerCompany()
        {
            if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
            {
                var user = (CustomPrinciple)System.Web.HttpContext.Current.User;
                var userdetails = loginService.GetUserProfile(int.Parse(user.ID)).FirstOrDefault();
                ViewBag.typeofuser = LoginService.GetUserTypeId("", (int)userdetails.UserTypeId).ToString();
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
                                cus_logo = row["cus_logo"].ToString(),
                                status = row["status"].ToString(),
                                new_POs = row["new_POs"].ToString(),
                                total_POs = row["total_POs"].ToString(),
                                due = row["due"].ToString(),
                                overdue = row["overdue"].ToString()
                            }).OrderByDescending(m => m.cus_company_Id).ToList();
                ViewBag.totalnewpos = customer.Select(m => int.Parse(m.new_POs)).Sum();
                ViewBag.totalinvoicedpos = customer.Select(m => int.Parse(m.total_POs)).Sum();
                ViewBag.totaldues = customer.Select(m => float.Parse(m.due)).Sum();
                ViewBag.totaloverdues = customer.Select(m => float.Parse(m.overdue)).Sum();
                // ViewBag.grand_total = productsinpos.Select(m => float.Parse(m.grand_total)).Distinct().Sum();
                if (ViewBag.typeofuser == "Admin" || ViewBag.typeofuser == "AdminStaff")
                {
                    ViewBag.records = customer;
                }
                if (ViewBag.typeofuser == "Customer" || ViewBag.typeofuser == "Staff")
                {
                    ViewBag.records = customer.Where(m => m.cus_company_name == userdetails.CompanyName.Trim()).ToList();
                }
                //ViewBag.records = customer;
                
                return PartialView("CustomerCompany", ViewBag.records);
            }

            return PartialView("CustomerCompany", null);
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
        public ActionResult UpdatecuscontactPic(HttpPostedFileBase helpSectionImages, string Customer_Id)
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

        public void Email(string First_Name, string Last_Name, string EmailId, string activationCode, string PassWord)
        {
            // Designing Email Part
            SendEmail abc = new SendEmail();
            string url = Request.Url.Scheme + "://" + Request.Url.Authority + "/Login/ActivateEmail?ActivationCode=" + activationCode + "&&Email=" + EmailId;
            FileInfo File = new FileInfo(Server.MapPath("/Content/mailer1.html"));
            string readFile = File.OpenText().ReadToEnd();
            readFile = readFile.Replace("[ActivationLink]", url);
            readFile = readFile.Replace("password", PassWord);
            readFile = readFile.Replace("[Name]", First_Name);
            string message = readFile;
            abc.EmailAvtivation(EmailId, message, "Account Activation");
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
            if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
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
                exec.Close();
                return cus_company_Id;
            }
            return 0;
        }
        private string getMaxcustomerID()
        {
            if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
            {
                string Customer_Id = null;
                var user = (CustomPrinciple)System.Web.HttpContext.Current.User;
                SqlDataReader exec = CustomerService.getCustomerId(user.DbName);
                if (exec.Read())
                {
                    Customer_Id = exec["Customer_Id"].ToString();
                }
                exec.Close();
                return Customer_Id;
            }

            return null;
        }
        private Customer getlastinsertedcuscompany(int cus_company_Id)
        {
            if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
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
            return null;
        }
        public List<Customer> getcuscontactDetail(DataTable dt)
        {
            if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
            {
                List<Customer> contact = new List<Customer>();
                contact = (from DataRow row in dt.Rows
                           select new Customer()
                           {
                               Customer_Id = row["Customer_Id"].ToString(),
                               Customer_contact_Fname = row["Customer_contact_Fname"].ToString(),
                               Customer_contact_Lname = row["Customer_contact_Lname"].ToString(),
                               Email_Id = row["Email_Id"].ToString(),
                               image = row["image"].ToString(),
                               status = row["status"].ToString()
                           }).OrderByDescending(m => m.Customer_Id).ToList();
                return contact;
            }
            return null;
        }
        public JsonResult getAllcusDetails(int cus_company_Id)
        {
            if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
            {
                var user = (CustomPrinciple)System.Web.HttpContext.Current.User;
                CustomerService.getlastinsertedcompany(cus_company_Id, user.DbName);
                var data = CustomerService.getAllcusDetails(cus_company_Id, user.DbName);

                long set1;
                long set2;
                long set3;

                if (data.Read())
                {

                    if (data["tds_apply"].ToString() == "")
                        set2 = 0;
                    else
                        set2 = long.Parse(data["tds_apply"].ToString());
                    if (data["cus_company_Id"].ToString() == "")
                        set1 = 0;
                    else
                        set1 = long.Parse(data["cus_company_Id"].ToString());
                    if (data["tax_exemption"].ToString() == "")
                        set3 = 0;
                    else
                        set3 = long.Parse(data["tax_exemption"].ToString());

                    Customer cs = new Customer
                    {
                        cus_company_Id = int.Parse(set1.ToString()),
                        cus_company_name = data["cus_company_name"].ToString(),
                        cus_email = data["cus_email"].ToString(),
                        cus_Note = data["cus_Note"].ToString(),
                        cus_logo = data["cus_logo"].ToString(),
                        Customer_Id = data["Customer_Id"].ToString(),
                        Adhar_Number = data["Adhar_Number"].ToString(),
                        GSTIN_Number = data["GSTIN_Number"].ToString(),
                        tax_reg_no = data["tax_reg_no"].ToString(),
                        pan_no = data["pan_no"].ToString(),
                        tds_apply = int.Parse(set2.ToString()),
                        tax_exemption = int.Parse(set3.ToString()),
                        tax_files = data["tax_files"].ToString(),
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
                    string json = JsonConvert.SerializeObject(cs);
                    return Json(json);
                }
                return Json("unique", JsonRequestBehavior.AllowGet);
            }
            return Json(null);
        }
        public JsonResult updatecuscompany(int cus_company_Id, string cus_company_name, string cus_email, string cus_logo)
        {
            if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
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
            return Json(null);
        }
        public JsonResult updatecuscompanyaddress(int cus_company_Id, string bill_street, string bill_city, string bill_state, string bill_postalcode, string bill_country, string ship_street, string ship_city, string ship_state, string ship_postalcode, string ship_country)
        {
            if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
            {
                var user = (CustomPrinciple)System.Web.HttpContext.Current.User;
                var data = CustomerService.CustomerAddressUpdateRow(cus_company_Id, bill_street, bill_city, bill_state, bill_postalcode, bill_country, ship_street, ship_city, ship_state, ship_postalcode, ship_country, user.DbName);
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
            return Json(null);
        }

        public JsonResult updatecuscontactdetails(string Customer_Id, string Customer_contact_Fname, string Customer_contact_Lname, string Mobile_No,
                          string Email_Id, string Adhar_Number, string cus_Job_position, string image)
        {
            if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
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
            return Json(null);
        }
        public JsonResult updatecuscompanynote(int cus_company_Id, string cus_Note)
        {
            if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
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
            return Json(null);
        }
        public JsonResult savecuscompany(string cus_company_name, string cus_email, string cus_logo)
        {
            if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
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
            return Json(null);
        }
        public JsonResult savecuscompanyaddress(int cus_company_Id, string bill_street, string bill_city, string bill_state, string bill_postalcode,
                string bill_country, string ship_street, string ship_city, string ship_state, string ship_postalcode, string ship_country)
        {
            if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
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
            return Json(null);
        }
        public JsonResult savecuscompanynote(int cus_company_Id, string cus_Note)
        {
            if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
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
            return Json(null);
        }

        public JsonResult savecuscontactdetails(int cus_company_Id, string Customer_contact_Fname, string Customer_contact_Lname, string Mobile_No,
                          string Email_Id, string Adhar_Number, string cus_Job_position, string image)
        {
            if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
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
            return Json(null);
        }

        public ActionResult deletecusRecord(int cus_company_Id, string status)
        {
            if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
            {
                var user = (CustomPrinciple)System.Web.HttpContext.Current.User;
                var data = CustomerService.deletecuscompRecord(cus_company_Id, status, user.DbName);
                if (status == "Active")
                    TempData["smsg"] = "Now Company with company id " + cus_company_Id + " is " + status + "";
                else
                    TempData["msg"] = "Now Company with company id" + cus_company_Id + " is " + status + "";
                return RedirectToAction("Index", "Customer");
            }


            return View();
        }

        public JsonResult deleteCustomer(string Customer_Id, string status)
        {
            if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
            {
                var user = (CustomPrinciple)System.Web.HttpContext.Current.User;
                var data = CustomerService.deleteCustomer(Customer_Id, status, user.DbName);
                if (data > 0)
                {
                    ViewBag.Customer_Id = Customer_Id;
                    var result = new { Result = "sucess", ID = Customer_Id, stat = status };
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                return Json("unique", JsonRequestBehavior.AllowGet);
            }
            return Json(null);
        }
        //customer invite to  email
        public JsonResult inviteCustomerForPos(int cus_company_Id)
        {
            if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
            {
                var user = (CustomPrinciple)System.Web.HttpContext.Current.User;
                string id = user.ID;
                string DBname = null;
                string fname = null;
                string lname = null;
                string cus_email = null;
                string image = null;
                string companyname = null;
                string companylogo = null;
                string Password = Guid.NewGuid().ToString().Split('-')[0]; //"ABC@123456";
                string mObile = null;
                string activationCode = Guid.NewGuid().ToString();
                int usertype = (int)LoginService.GetUserTypeId("Customer", 0);
                string Date_Format = null, Timezone = null, Currency = null, UserSite = null;
                int Subscription = 0;
                DateTime? SubscriptionDate = null;

                SqlDataReader exec = CustomerService.getusermaster(id, user.DbName);
                SqlDataReader exec2 = CustomerService.getlastinsertedcompany(cus_company_Id, user.DbName);
                if (exec.Read())
                {
                    DBname = exec["DB_Name"].ToString();
                    Subscription = int.Parse(exec["Subscriptionid"].ToString());
                    Date_Format = exec["Date_Format"].ToString();
                    Timezone = exec["Timezone"].ToString();
                    Currency = exec["Currency"].ToString();
                }
                if (exec2.Read())
                {
                    companyname = exec2["cus_company_name"].ToString();
                    cus_email = exec2["cus_email"].ToString();
                    //companylogo = exec2["cus_logo"].ToString();
                }
                exec2.Close();
                exec.Close();
                UserSite = companyname.Trim();
                fname = companyname.Trim();
                lname = companyname.Trim();

                UserProfileController uc = new UserProfileController();
                int data1 = uc.invitechecking(cus_email, UserSite, usertype.ToString());
                if (data1 == 1)
                    return Json("invitationsent");
                if (data1 == 0)
                    return Json("emailverified");
                else
                {
                    var data = LoginService.Authenticateuser("checkemail1", cus_email, null, UserSite, usertype);
                    if (data.HasRows)
                        return Json("Exists");
                    else
                    {
                        int count = LoginService.CreateUser(cus_email, fname, lname, DBname, DateTime.UtcNow, Password, Subscription, usertype, UserSite, companyname, mObile, SubscriptionDate, 0, activationCode, image, Date_Format, Timezone, Currency, companylogo);
                        if (count > 0)
                        {
                            Email(fname, null, cus_email, activationCode, Password); //Sending Email
                            return Json("sucess");
                        }
                    }
                    data.Close();
                    return Json("unique", JsonRequestBehavior.AllowGet);
                }
            }
            return Json(null);
        }

        //contact person invite
        public JsonResult inviteCustomer(string Customer_Id, string cus_company_Id)
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
                string Password = Guid.NewGuid().ToString().Split('-')[0];//"ABC@123456";
                string mObile = null;
                string activationCode = Guid.NewGuid().ToString();
                int usertype = (int)LoginService.GetUserTypeId("Staff", 0);
                string Date_Format = null, Timezone = null, Currency = null, UserSite = null;
                int Subscription = 0;
                DateTime? SubscriptionDate = null;

                SqlDataReader exec = CustomerService.getusermaster(id, user.DbName);
                SqlDataReader exec1 = CustomerService.getCustomerContact(Customer_Id, user.DbName);
                SqlDataReader exec2 = CustomerService.getlastinsertedcompany(int.Parse(cus_company_Id), user.DbName);
                if (exec.Read())
                {
                    DBname = exec["DB_Name"].ToString();
                    Subscription = int.Parse(exec["Subscriptionid"].ToString());
                    Date_Format = exec["Date_Format"].ToString();
                    Timezone = exec["Timezone"].ToString();
                    Currency = exec["Currency"].ToString();
                }
                exec.Close();
                if (exec1.Read())
                {
                    fname = exec1["Customer_contact_Fname"].ToString();
                    lname = exec1["Customer_contact_Lname"].ToString();
                    eMail = exec1["Email_Id"].ToString();
                    mObile = exec1["Mobile_No"].ToString();
                    image = exec1["image"].ToString();
                    //Date_Format = exec1["Date_Format"].ToString();
                    //Timezone = exec1["Timezone"].ToString();
                    //Currency = exec1["Currency"].ToString();
                }
                exec1.Close();
                if (exec2.Read())
                {
                    companyname = exec2["cus_company_name"].ToString();
                    companylogo = exec2["cus_logo"].ToString();
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
                data.Close();
                return Json("unique", JsonRequestBehavior.AllowGet);
            }
            //data.Close();
            return Json(null);
        }
        public PartialViewResult CustomerContact(string id)
        {
            if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
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
                    ViewBag.id = id.ToString();
                    return PartialView("CustomerContact", ViewBag.records);
                }
            }
            return PartialView("CustomerContact", null);
        }
        //for Jobpostions adding
        public JsonResult addPosition(string cus_Job_position, int cus_company_Id)
        {
            if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
            {
                var user = (CustomPrinciple)System.Web.HttpContext.Current.User;
                var existingNo = CustomerService.getcusJobposition(cus_Job_position, user.DbName);
                if (existingNo.Read())
                {
                    return Json("exists", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var data = CustomerService.insertcusJobposition(cus_Job_position, cus_company_Id, user.DbName);
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
            return Json(null);
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
            if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
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
            return Json(null);
        }

        //tax file upload
        [HttpPost]
        public ActionResult TaxExemptionfile(HttpPostedFileBase file, int cus_company_Id, string Adhar_Number, string GSTIN_Number, string tax_reg_no, string pan_no, int tds_apply, int tax_exemption, string clickeditem)
        {
            string filename = "";
            var user = (CustomPrinciple)System.Web.HttpContext.Current.User;
            if (System.Web.HttpContext.Current.Request.Files.AllKeys.Any())
            {
                if (file != null && file.ContentLength > 0)
                {
                    var streamfile = new StreamReader(file.InputStream);
                    var streamline = string.Empty;
                    List<string> taxfile = new List<string>();

                    while ((streamline = streamfile.ReadLine()) != null)
                    {
                        taxfile.Add(streamline);
                    }
                    //Uploaded Images

                    for (int i = 0; i < Request.Files.Count; i++)
                    {
                        var file1 = Request.Files[i];
                        if (file1 != null && file1.ContentLength > 0)
                        {
                            filename = filename + "," + cus_company_Id + "_" + file1.FileName;
                            file1.SaveAs(Server.MapPath("~/TaxFiles/" + cus_company_Id + "_" + file1.FileName));
                        }
                    }
                }
            }

            filename = filename.TrimStart(',');

            if (clickeditem == "updatetaxdetails")
            {
                int count = CustomerService.Updatecustax(cus_company_Id, Adhar_Number, GSTIN_Number, tax_reg_no, pan_no, tds_apply, tax_exemption, filename, user.DbName);
                if (count > 0)
                    return Json("success");
            }
            else
            {
                int count = CustomerService.Updatecustax(cus_company_Id, Adhar_Number, GSTIN_Number, tax_reg_no, pan_no, tds_apply, tax_exemption, filename, user.DbName);
                if (count > 0)
                    return Json("success1");
            }

            return Json("unsucess");
        }
    }
}