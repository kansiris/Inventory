using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Inventory.Service;
using System.Data.SqlClient;
using System.Data;
using Inventory.Models;

namespace Inventory.Controllers
{
    public class AvailableCompaniesController : Controller
    {
        // GET: AvailableCompanies
        public ActionResult Index()
        {
            string emailid = Request.QueryString["email"];
            SqlDataReader value = LoginService.Authenticateuser("loginsite", emailid, null, null, 0);
            DataTable dt = new DataTable();
            dt.Load(value);
            List<UserMaster> userMaster = new List<UserMaster>();
            userMaster = (from DataRow row in dt.Rows
                          select new UserMaster()
                          {
                              ID = row["ID"].ToString(),
                              User_Site = row["User_Site"].ToString(),
                              UserTypeId = (int)row["UserTypeId"]
                          }).ToList();
            ViewBag.records = userMaster;
            return View();
        }

        [HttpPost]
        public ActionResult Index(string email,string usertype,string loginpassword)
        {
            var type = LoginService.GetUserTypeId(null, long.Parse(usertype));
            SqlDataReader value = LoginService.Authenticateuser("redirectuser", email, loginpassword, null, long.Parse(usertype));
            string controller = "";
            if (type.ToString() == "Staff" || type.ToString() == "Owner") //checking type
                controller = "UserHome";
            else
                controller = type.ToString() + "Home";
            if (value.HasRows == false) //Failed Login
                return Content("<script language='javascript' type='text/javascript'>alert('Invalid Login!!! Try Again');location.href='" + @Url.Action("Index", "AvailableCompanies",new { email = email}) + "'</script>"); // Stays in Same View
            return RedirectToAction("Index", controller); // Redirects to Particular View
        }

        //public ActionResult Authenticate(string emailid, string password, string usertype)
        //{
        //    var type = LoginService.GetUserTypeId(null, long.Parse(usertype));
        //    SqlDataReader value = LoginService.Authenticateuser("redirectuser", emailid, password, null, long.Parse(usertype));
        //    string controller = "";
        //    if (type.ToString() == "Staff" || type.ToString() == "Owner") //checking type
        //        controller = "UserHome";
        //    else
        //        controller = type.ToString()+"Home";
        //    if (value.HasRows == false) //Failed Login
        //        return Content("<script language='javascript' type='text/javascript'>alert('Failed!');location.href='" + @Url.Action("Index", "AvailableCompanies") + "'</script>");
        //    return RedirectToAction("Index", controller);
        //    //return Content("<script language='javascript' type='text/javascript'>alert('Failed!');location.href='" + @Url.Action("Index", "AvailableCompanies") + "'</script>");
        //    //return Json(new { msg = "InvalidLogin" }, JsonRequestBehavior.AllowGet); // Stays in Same View
        //    //return Json(new { msg= "Redirect", controller }); // Redirects to Particular View
        //    //url = Url.Action("Index", controller)
        //}
    }
}