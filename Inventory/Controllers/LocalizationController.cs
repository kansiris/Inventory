using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Inventory.Service;
using Inventory.Models;

namespace Inventory.Controllers
{
    public class LocalizationController : Controller
    {
        // GET: Localization
        public ActionResult Index(UserMaster userDetails)
        {
            ViewBag.timeZoneInfos = TimeZoneInfo.GetSystemTimeZones().Select(m=>m.DisplayName).ToList(); //Available Time Zones
            return View();
        }
    }
}