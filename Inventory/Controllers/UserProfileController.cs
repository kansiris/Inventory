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

        [HttpPost]
        public ActionResult Index([Bind(Prefix = "Item1")]UserMaster userMaster, [Bind(Prefix = "Item2")]UserAddress userAddress, [Bind(Prefix = "Item3")]OwnerCompanyAddress ownerCompanyAddress, string command, string id)
        {
            int count = 0;
            if (command == "Localization" || command == "Account" || command == "Essentials")
            {
                Byte[] array = new Byte[64];
                Array.Clear(array, 0, array.Length);
                userMaster.company_logo = userMaster.Profile_Picture = array;
                count = LoginService.updateuserprofile(command, int.Parse(id), userMaster.First_Name, userMaster.Last_Name, userMaster.Password, userMaster.Profile_Picture.ToArray(), userMaster.Date_Format, userMaster.Timezone, userMaster.Currency, userMaster.company_logo.ToArray());
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
                return Content("<script language='javascript' type='text/javascript'>alert('Profile Updated');location.href='" + @Url.Action("Index", "UserProfile", new { id = id }) + "'</script>"); // Stays in Same View
            return Content("<script language='javascript' type='text/javascript'>alert('Failed To Update Profile');location.href='" + @Url.Action("Index", "UserProfile", new { id = id }) + "'</script>"); // Stays in Same View
            //return View();
        }

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

        public JsonResult UpdateCompanyPic(string path,string id)
        {
            //byte[] bfoo = Convert.ToBase64String(path);
            if (id != "1")
            {
                string file = path.Split('\\')[2];
                var myArray = Encoding.UTF8.GetBytes(path);
                Byte[] array = new Byte[64];
                Array.Clear(array, 0, array.Length);
                byte[] companypic = myArray; byte[] profilepic = array;
                //int userid = int.Parse(Request.Form["id"]);
                int count = LoginService.updateuserprofile("Company", int.Parse(id), null, null, null, profilepic, null, null, null, companypic);
                return Json(companypic);
            }
            return Json(JsonRequestBehavior.AllowGet);
        }
    }
}




//public List<UserMaster> GetUserProfile(int id)
//{
//    ViewBag.userprofile = loginService.GetUserProfilesample(id);
//    SqlDataReader value = LoginService.GetUserProfile(id);
//    DataTable dt = new DataTable();
//    dt.Load(value);
//    List<UserMaster> userMaster = new List<UserMaster>();
//    userMaster = (from DataRow row in dt.Rows
//                          select new UserMaster()
//                          {
//                              ID = row["ID"].ToString(),
//                              First_Name = row["First_Name"].ToString(),
//                              Last_Name = row["Last_Name"].ToString(),
//                              EmailId = row["EmailId"].ToString(),
//                              DB_Name =row["DB_Name"].ToString(),
//                              Created_Date = (DateTime)row["Created_Date"],
//                              CompanyName = row["CompanyName"].ToString(),
//                              Phone = row["Phone"].ToString(),
//                              User_Site = row["User_Site"].ToString(),
//                              UserTypeId = (int)row["UserTypeId"],
//                              SubscriptionDate = (DateTime)row["SubscriptionDate"],
//                              Timezone = row["Timezone"].ToString(),
//                              Date_Format = row["Date_Format"].ToString()
//                          }).ToList();
//    return userMaster;
//}