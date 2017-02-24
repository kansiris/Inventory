using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Inventory.Service;
using Inventory.Models;
using System.Web.Security;
using Inventory.Content;

namespace Inventory.Controllers
{
    public class UserHomeController : Controller
    {
        // GET: UserHome
        
        public ActionResult Index(UserMaster userDetails)
        {
            //var userId = (CustomPrinciple)System.Web.HttpContext.Current.User;
            string futuredate = userDetails.SubscriptionDate.Value.Date.AddDays(15).Day.ToString(); // adding 15 days to subscription date
            string currentdate = DateTime.UtcNow.Day.ToString(); //getting current date
            ViewBag.accessexpiry = int.Parse(futuredate) - int.Parse(currentdate);//calculating difference b/w current date & future date & passing value to view
            ViewBag.timeZoneInfos = TimeZoneInfo.GetSystemTimeZones().Select(m=>m.DisplayName).ToList(); //Available Time Zones
            return View(userDetails);
        }
    }
}