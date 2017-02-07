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

        public ActionResult Authenticate(string emailid, string password, string usertype)
        {
            var type = LoginService.GetUserTypeId(null, long.Parse(usertype));
            SqlDataReader value = LoginService.Authenticateuser("redirectuser", emailid, password, null, long.Parse(usertype));
            string msg = "";
            if (value.HasRows)
                msg = "exists";
            else
                return Content("<script language='javascript' type='text/javascript'>alert('Invalid Login');location.href='" + @Url.Action("Index", "AvailableCompanies")+ "'</script>");
            return Json(new { type, msg }, JsonRequestBehavior.AllowGet);
        }
    }
}