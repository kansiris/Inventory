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
            SqlDataReader value = LoginService.Authenticateuser("loginsite", emailid, null, null);
            DataTable dt = new DataTable();
            dt.Load(value);
            List<UserMaster> userMaster = new List<UserMaster>();
            userMaster = (from DataRow row in dt.Rows select new UserMaster() { ID = row["ID"].ToString(),User_Site = row["User_Site"].ToString() }).ToList();
            ViewBag.records = userMaster;
            return View();
        }

        public JsonResult Authenticate()
        {
            return Json(JsonRequestBehavior.AllowGet);
        }
    }
}