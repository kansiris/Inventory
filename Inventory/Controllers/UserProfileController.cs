using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Inventory.Controllers
{
    public class UserProfileController : Controller
    {
        // GET: UserProfile
        public ActionResult Index()
        {
            return View();
        }
    }
}


// GET: UserProfile
//public ActionResult Index(string email,string usertype,string site)
//{
//    SqlDataReader value = LoginService.Authenticateuser("getuserprofile", email, null, site, int.Parse(usertype));
//    DataTable dt = new DataTable();
//    dt.Load(value);
//    UserMaster userMaster = new UserMaster();
//    ViewBag.editrecord = (from DataRow row in dt.Rows
//                  select new UserMaster()
//                  {
//                      First_Name = row["First_Name"].ToString(),
//                      Last_Name = row["Last_Name"].ToString(),
//                      EmailId = row["EmailId"].ToString(),
//                      Created_Date = (DateTime)row["Created_Date"],
//                      CompanyName = row["CompanyName"].ToString(),
//                      Phone = row["Phone"].ToString(),
//                      User_Site = row["User_Site"].ToString(),
//                      UserTypeId = (int)row["UserTypeId"],
//                      SubscriptionDate = (DateTime)row["SubscriptionDate"]
//                  });
//    //ViewBag.editrecord = userMaster;
//    return View();
//}