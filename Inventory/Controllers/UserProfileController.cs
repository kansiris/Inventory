using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using Inventory.Service;
using System.Data;
using Inventory.Models;
using System.Globalization;
using System.IO;
using System.Drawing;
using System.Text;
using Newtonsoft.Json;
using Inventory.Content;
using Inventory.Utility;

namespace Inventory.Controllers
{
    public class UserProfileController : Controller
    {
        // GET: UserProfile
        LoginService loginService = new LoginService();
        public ActionResult Index(string id)
        {
            var profile = loginService.GetUserProfile(int.Parse(id)); //Get's User Profile
            ViewBag.timeZoneInfos = new SelectList(TimeZoneInfo.GetSystemTimeZones(), "DisplayName", "DisplayName", profile[0].Timezone); //Available Time Zones
            ViewBag.usercountry = new SelectList(CountryList(), "Value", "Text", profile[0].Ucountry); //CountryList(); 
            ViewBag.companycountry = new SelectList(CountryList(), "Value", "Text", profile[0].Ccountry);//CountryList();//new SelectList(CountryList(), "EnglishName", "EnglishName", profile[0].Ccountry); //CountryList(); 
            //ViewBag.country = CountryList();
            //SelectList sl = new SelectList(CountryList(), "Value", "Text", "8");
            ViewBag.profile = profile.Take(1); //current user record
            ViewBag.jobpositions = AvailableJobPositions(id);
            ViewBag.typeofuser = LoginService.GetUserTypeId("", (int)loginService.GetUserProfile(int.Parse(id)).FirstOrDefault().UserTypeId).ToString();
            // profile.Clear();
            return View();
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

        [HttpPost]
        public ActionResult UpdateCompanyPic(HttpPostedFileBase helpSectionImages, string id)
        {
            if (System.Web.HttpContext.Current.Request.Files.AllKeys.Any())
            {
                var pic = System.Web.HttpContext.Current.Request.Files["helpSectionImages"];
                Image img = Bitmap.FromStream(pic.InputStream);
                ImageConverter _imageConverter = new ImageConverter();
                byte[] companypic = (byte[])_imageConverter.ConvertTo(img, typeof(byte[]));
                string base64String = Convert.ToBase64String(companypic);
                int count = LoginService.updateuserprofile("Company", int.Parse(id), null, null, null, null, null, null, null, base64String);
                return Json(base64String);
            }
            return Json(JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult UpdateProfilePic(HttpPostedFileBase helpSectionImages, string id)
        {
            if (System.Web.HttpContext.Current.Request.Files.AllKeys.Any())
            {
                var pic = System.Web.HttpContext.Current.Request.Files["helpSectionImages"];
                Image img = Bitmap.FromStream(pic.InputStream);
                ImageConverter _imageConverter = new ImageConverter();
                byte[] companypic = (byte[])_imageConverter.ConvertTo(img, typeof(byte[]));
                string base64String = Convert.ToBase64String(companypic);
                int count = LoginService.updateuserprofile("Profile", int.Parse(id), null, null, null, base64String, null, null, null, null);
                return Json(base64String);
            }
            return Json(JsonRequestBehavior.AllowGet);
        }

        public JsonResult UpdateUserProfile(string command, string id, UserMaster usermaster, UserAddress userAddress, OwnerCompanyAddress ownerCompanyAddress, OwnerStaff ownerStaff, string staffid)
        {
            int count = 0;
            if (command == "Localization" || command == "Essentials")
            {
                count = LoginService.updateuserprofile(command, int.Parse(id), usermaster.First_Name, usermaster.Last_Name, usermaster.Password, usermaster.Profile_Picture, usermaster.Date_Format, usermaster.Timezone, usermaster.Currency, usermaster.company_logo);
                if (count > 0)
                    return Json(new { Result = "success", msg = "" + command + " Updated SuccessFully!!!" });
            }
            if (command == "Account")
            {
                if (usermaster.Password != "" && usermaster.Password != null)
                {
                    count = LoginService.updateuserprofile(command, int.Parse(id), usermaster.First_Name, usermaster.Last_Name, usermaster.Password, usermaster.Profile_Picture, usermaster.Date_Format, usermaster.Timezone, usermaster.Currency, usermaster.company_logo);
                    if (count > 0)
                        return Json(new { Result = "success", msg = "" + command + " Updated SuccessFully!!!" });
                }
                else
                    return Json(new { Result = "Password", msg = "Enter New Password" });
            }
            if (command == "useraddress")
            {
                count = LoginService.updateuseraddress(int.Parse(id), userAddress.Line1, userAddress.Line2, userAddress.city, userAddress.state, userAddress.postalcode, userAddress.country);
                if (count > 0)
                    return Json(new { Result = "success", msg = "Your Details Updated SuccessFully!!!" });
            }
            if (command == "companyaddress")
            {
                count = LoginService.updatecompanyaddress(int.Parse(id), ownerCompanyAddress.Line1, ownerCompanyAddress.Line2, ownerCompanyAddress.city, ownerCompanyAddress.state, ownerCompanyAddress.postalcode, ownerCompanyAddress.country);
                if (count > 0)
                    return Json(new { Result = "success", msg = "Company Address Updated SuccessFully!!!" });
            }
            if (command == "addstaff")
            {
                count = LoginService.CreateStaff(int.Parse(id), ownerStaff.First_Name, ownerStaff.Last_Name, ownerStaff.Mobile_No, ownerStaff.Email, ownerStaff.Vendor_Access, ownerStaff.Customer_Access, ownerStaff.Job_position, ownerStaff.UserPic);
                return Json(new { Result = "staffadded", msg = "User Added SuccessFully!!!" });
            }
            if (command == "updatestaff")
            {
                count = LoginService.UpdateStaff("staffdetails", int.Parse(ownerStaff.Staff_Id), ownerStaff.First_Name, ownerStaff.Last_Name, ownerStaff.Mobile_No, ownerStaff.Email, ownerStaff.Vendor_Access, ownerStaff.Customer_Access, ownerStaff.Job_position, ownerStaff.UserPic, null);
                return Json(new { Result = "staffupdated", msg = "User Updated SuccessFully!!!" });
            }
            return Json("Failed");
        }

        public PartialViewResult GetStaffRecords(string id)
        {
            string typeofuser = LoginService.GetUserTypeId("", (int)loginService.GetUserProfile(int.Parse(id)).FirstOrDefault().UserTypeId).ToString();
            var records = LoginService.GetStaff(int.Parse(id), "");
            var dt = new DataTable();
            dt.Load(records);
            dt.DefaultView.Sort = "Staff_Id DESC";
            DataView dv = dt.DefaultView;
            dt = dv.ToTable();
            ViewBag.records = StaffDetails(dt);
            if (typeofuser == "Franchise" || typeofuser == "Staff")
            {
                int customerstaffcount = ViewBag.records.Count;
                if (customerstaffcount == 0) { ViewBag.customerstaffcount = "Maximum 3 Staff Allowed"; }
                else { ViewBag.customerstaffcount = "Can Add upto " + (3 - customerstaffcount) + " Staff More"; }
            }
            //records.Close();
            return PartialView("StaffRecords", ViewBag.records);
        }

        public JsonResult GetParticularStaff(string id, string command)
        {
            if (command == "particular")
            {
                var record = LoginService.GetStaff(int.Parse(id), command);
                if (record.HasRows)
                {
                    var dt = new DataTable();
                    dt.Load(record);
                    var data = StaffDetails(dt);
                    //record.Close();
                    return Json(data.FirstOrDefault());
                    //return Json( JsonConvert.SerializeObject(data));
                }
            }
            if (command == "Active" || command == "InActive")
            {
                int count = LoginService.UpdateStaff("status", int.Parse(id), null, null, 0, null, 0, 0, null, null, command);
                if (count > 0)
                    return Json(command);
            }
            return Json("unique");
        }

        public List<OwnerStaff> StaffDetails(DataTable dt)
        {
            List<OwnerStaff> ownerStaff = (from DataRow row in dt.Rows
                                           select new OwnerStaff()
                                           {
                                               Staff_Id = row["Staff_Id"].ToString(),
                                               Owner_id = int.Parse(row["Owner_id"].ToString()),
                                               First_Name = row["First_Name"].ToString(),
                                               Last_Name = row["Last_Name"].ToString(),
                                               Mobile_No = long.Parse(row["Mobile_No"].ToString()),
                                               Email = row["Email"].ToString(),
                                               Status_ID = int.Parse(row["Status_ID"].ToString()),
                                               Vendor_Access = int.Parse(row["Vendor_Access"].ToString()),
                                               Customer_Access = int.Parse(row["Customer_Access"].ToString()),
                                               Job_position = row["Job_position"].ToString(),
                                               UserPic = row["UserPic"].ToString()
                                           }).ToList();
            return ownerStaff;
        }

        public class OwnerJobPosition
        {
            public int Position_ID { get; set; }
            public int ID { get; set; }
            public string Job_Position { get; set; }
        }

        public List<OwnerJobPosition> AvailableJobPositions(string id)
        {
            //var records = LoginService.GetStaff(int.Parse(id), "");
            var records = LoginService.GetJobPostions(int.Parse(id));
            var dt = new DataTable();
            dt.Load(records);
            //records.Close();
            List<OwnerJobPosition> positions = (from DataRow row in dt.Rows select new OwnerJobPosition() { Position_ID = int.Parse(row["Position_ID"].ToString()), ID = int.Parse(row["ID"].ToString()), Job_Position = row["Job_position"].ToString() }).ToList();
            return positions;
        }

        [HttpPost]
        public ActionResult UpdateStaffProfilePic(HttpPostedFileBase helpSectionImages, string id)
        {
            if (System.Web.HttpContext.Current.Request.Files.AllKeys.Any())
            {
                var pic = System.Web.HttpContext.Current.Request.Files["helpSectionImages"];
                Image img = Bitmap.FromStream(pic.InputStream);
                ImageConverter _imageConverter = new ImageConverter();
                byte[] staffpic = (byte[])_imageConverter.ConvertTo(img, typeof(byte[]));
                string base64String = Convert.ToBase64String(staffpic);
                //int count = LoginService.UpdateStaff("staffpic", int.Parse(id), "", "", 0, "", 0, 0, "", base64String);
                //int count = LoginService.updateuserprofile("staffpic", int.Parse(id), null, null, null, base64String, null, null, null, null);
                return Json(base64String);
            }
            return Json(JsonRequestBehavior.AllowGet);
        }

        public ActionResult convertpic()
        {
            if (System.Web.HttpContext.Current.Request.Files.AllKeys.Any())
            {
                var pic = System.Web.HttpContext.Current.Request.Files["helpSectionImages"];
                Image img = Bitmap.FromStream(pic.InputStream);
                ImageConverter _imageConverter = new ImageConverter();
                byte[] staffpic = (byte[])_imageConverter.ConvertTo(img, typeof(byte[]));
                string base64String = Convert.ToBase64String(staffpic);
                //int count = LoginService.UpdateStaff("staffpic", int.Parse(id), "", "", 0, "", 0, 0, "", base64String);
                //int count = LoginService.updateuserprofile("staffpic", int.Parse(id), null, null, null, base64String, null, null, null, null);
                return Json(base64String);
            }
            return View();
        }

        public JsonResult JobPosition(string id, string position, string type, string PositionID)
        {
            int count = 0;
            string id1 = id.Replace("&&type=upload", "");
            if (type == "addposition")
            {
                count = LoginService.JobPositions("add", int.Parse(id1), position, null);
                var records = AvailableJobPositions(id1);
                return Json(new { msg = "Position Added Successfullly!!!Click Close Button and Select Position", records = records });
            }
            if (PositionID != "")
            {
                count = LoginService.JobPositions("delete", 0, "", PositionID);
                var records = AvailableJobPositions(id1);
                return Json(new { msg = "Position Removed", records = records });
            }
            return Json(JsonRequestBehavior.AllowGet);
        }

        public JsonResult staffinvite(string staffid)
        {
            if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
            {
                var user = (CustomPrinciple)System.Web.HttpContext.Current.User;
                var record = LoginService.GetStaff(int.Parse(staffid), "particular");
                if (record.HasRows)
                {
                    var dt = new DataTable();
                    dt.Load(record);
                    var data = StaffDetails(dt).FirstOrDefault();
                    var profile = loginService.GetUserProfile(int.Parse(user.ID)).FirstOrDefault(); //Get's User Profile
                    string Password = Guid.NewGuid().ToString().Split('-')[0];//"ABC@123456";
                    string activationCode = Guid.NewGuid().ToString();
                    int usertype = (int)LoginService.GetUserTypeId("OwnerStaff", 0);

                    int count = LoginService.CreateUser(data.Email, data.First_Name, data.Last_Name, profile.DB_Name, DateTime.UtcNow, Password, (int)profile.SubscriptionId, usertype, profile.User_Site, profile.CompanyName, data.Mobile_No.ToString(), null, 0, activationCode, data.UserPic, profile.Date_Format, profile.Timezone,profile.Currency, profile.company_logo);
                    if (count > 0)
                    {
                        Email(data.First_Name, data.Last_Name, data.Email, activationCode, Password); //Sending Email
                        return Json("success");
                    }
                }
            }
            return Json("Failed");
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
            string message = readFile;
            abc.EmailAvtivation(EmailId, message, "Account Activation");
        }
    }
}