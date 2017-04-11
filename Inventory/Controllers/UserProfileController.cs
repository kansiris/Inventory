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
            ViewBag.usercountry = new SelectList(CountryList(),"Value", "Text", profile[0].Ucountry); //CountryList(); 
            ViewBag.companycountry = new SelectList(CountryList(), "Value", "Text", profile[0].Ccountry);//CountryList();//new SelectList(CountryList(), "EnglishName", "EnglishName", profile[0].Ccountry); //CountryList(); 
            //ViewBag.country = CountryList();
            //SelectList sl = new SelectList(CountryList(), "Value", "Text", "8");
            ViewBag.profile = profile.Take(1); //current user record
            ViewBag.jobpositions = AvailableJobPositions(id);
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

        public JsonResult UpdateUserProfile(string command, string id, UserMaster usermaster, UserAddress userAddress, OwnerCompanyAddress ownerCompanyAddress,OwnerStaff ownerStaff,string staffid)
        {
            int count = 0;
            if (command == "Localization" || command == "Account" || command == "Essentials")
            {
                count = LoginService.updateuserprofile(command, int.Parse(id), usermaster.First_Name, usermaster.Last_Name, usermaster.Password, usermaster.Profile_Picture, usermaster.Date_Format, usermaster.Timezone, usermaster.Currency, usermaster.company_logo);
            }
            if (command == "useraddress")
            {
                count = LoginService.updateuseraddress(int.Parse(id), userAddress.Line1, userAddress.Line2, userAddress.city, userAddress.state, userAddress.postalcode, userAddress.country);
            }
            if (command == "companyaddress")
            {
                count = LoginService.updatecompanyaddress(int.Parse(id), ownerCompanyAddress.Line1, ownerCompanyAddress.Line2, ownerCompanyAddress.city, ownerCompanyAddress.state, ownerCompanyAddress.postalcode, ownerCompanyAddress.country);
            }
            if (command == "addstaff")
            {
                count = LoginService.CreateStaff(int.Parse(id), ownerStaff.First_Name, ownerStaff.Last_Name, ownerStaff.Mobile_No, ownerStaff.Email, ownerStaff.Vendor_Access, ownerStaff.Customer_Access, ownerStaff.Job_position,ownerStaff.UserPic);
                return Json("staffadded");
            }
            if (command == "updatestaff")
            {
                count = LoginService.UpdateStaff("staffdetails",int.Parse(ownerStaff.Staff_Id), ownerStaff.First_Name, ownerStaff.Last_Name, ownerStaff.Mobile_No, ownerStaff.Email, ownerStaff.Vendor_Access, ownerStaff.Customer_Access, ownerStaff.Job_position,ownerStaff.UserPic);
                return Json("staffupdated");
            }
            if (count > 0)
                return Json("success");
            return Json("Failed");
        }

        public PartialViewResult GetStaffRecords(string id)
        {
            var records = LoginService.GetStaff(int.Parse(id),"");
            var dt = new DataTable();
            dt.Load(records);
            dt.DefaultView.Sort = "Staff_Id DESC";
            DataView dv = dt.DefaultView;
            dt = dv.ToTable();
            ViewBag.records = StaffDetails(dt);
            return PartialView("StaffRecords", ViewBag.records);
        }

        public JsonResult GetParticularStaff(string id, string command)
        {
            var record = LoginService.GetStaff(int.Parse(id), command);
            if (record.HasRows)
            {
                var dt = new DataTable();
                dt.Load(record);
                var data = StaffDetails(dt);
                return Json(data.FirstOrDefault());
                //return Json( JsonConvert.SerializeObject(data));
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
                              UserPic=row["UserPic"].ToString()
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
            List<OwnerJobPosition> positions= (from DataRow row in dt.Rows select new OwnerJobPosition() { Position_ID = int.Parse(row["Position_ID"].ToString()), ID = int.Parse(row["ID"].ToString()), Job_Position = row["Job_position"].ToString() }).ToList();
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

        public JsonResult JobPosition(string id,string position,string type,string PositionID)
        {
            int count = 0;
            if (type == "addposition")
            {
                count = LoginService.JobPositions("add", int.Parse(id), position,null);
                var records = AvailableJobPositions(id);
                return Json(new { msg = "Position Added Successfullly!!!Click Close Button and Select Position", records = records });
            }
            if (PositionID!="")
            {
                count = LoginService.JobPositions("delete", 0, "", PositionID);
                var records = AvailableJobPositions(id);
                return Json(new { msg = "Position Removed", records = records });
            }
            return Json(JsonRequestBehavior.AllowGet);
        }
    }
}