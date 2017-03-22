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
            ViewBag.usercountry = new SelectList(CountryList(), "EnglishName", "EnglishName", profile[0].Ucountry); //CountryList(); 
            ViewBag.companycountry = CountryList();//new SelectList(CountryList(), "EnglishName", "EnglishName", profile[0].Ccountry); //CountryList(); 
            ViewBag.country = CountryList();
            ViewBag.profile = profile; //current user record
            return View();
        }

        //[HttpPost]
        //public ActionResult Index([Bind(Prefix = "Item1")]UserMaster userMaster, [Bind(Prefix = "Item2")]UserAddress userAddress, [Bind(Prefix = "Item3")]OwnerCompanyAddress ownerCompanyAddress, string command, string id)
        //{
        //    int count = 0;
        //    if (command == "Localization" || command == "Account" || command == "Essentials")
        //    {
        //        count = LoginService.updateuserprofile(command, int.Parse(id), userMaster.First_Name, userMaster.Last_Name, userMaster.Password, userMaster.Profile_Picture, userMaster.Date_Format, userMaster.Timezone, userMaster.Currency, userMaster.company_logo);
        //    }
        //    if (command == "useraddress")
        //    {
        //        count = LoginService.updateuseraddress(int.Parse(id), userAddress.Line1, userAddress.Line2, userAddress.city, userAddress.state, userAddress.postalcode, userAddress.country);
        //    }
        //    if (command == "companyaddress")
        //    {
        //        count = LoginService.updatecompanyaddress(int.Parse(id), ownerCompanyAddress.Line1, ownerCompanyAddress.Line2, ownerCompanyAddress.city, ownerCompanyAddress.state, ownerCompanyAddress.postalcode, ownerCompanyAddress.country);
        //    }
        //    if (count > 0)
        //        return Content("<script language='javascript' type='text/javascript'>alert('Profile Updated');location.href='" + @Url.Action("Index", "UserProfile", new { id = id }) + "'</script>"); // Stays in Same View
        //    return Content("<script language='javascript' type='text/javascript'>alert('Failed To Update Profile');location.href='" + @Url.Action("Index", "UserProfile", new { id = id }) + "'</script>"); // Stays in Same View
        //    //return View();
        //}

        private List<string> CountryList()
        {
            List<string> cultureList = new List<string>();
            CultureInfo[] getCultureInfo = CultureInfo.GetCultures(CultureTypes.SpecificCultures);
            if (getCultureInfo.Count() > 0)
            {
                foreach (CultureInfo cultureInfo in getCultureInfo)
                {
                    RegionInfo getRegionInfo = new RegionInfo(cultureInfo.LCID);
                    if (cultureList.Contains(getRegionInfo.EnglishName) == false)
                    {
                        cultureList.Add(getRegionInfo.EnglishName);
                    }
                }
            }
            if (cultureList.Count > 0)
                cultureList.Sort();
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

        public JsonResult UpdateUserProfile(string command, string id, UserMaster usermaster, UserAddress userAddress, OwnerCompanyAddress ownerCompanyAddress)
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
            if (count > 0)
                return Json("success");
            return Json("Failed");
        }
    }
}