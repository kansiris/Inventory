using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Inventory.Service;
using Inventory.Models;

namespace Inventory.Controllers
{
    public class UserHomeController : Controller
    {
        // GET: UserHome
        public ActionResult Index(UserMaster userDetails)
        {
            string futuredate = userDetails.SubscriptionDate.Value.Date.AddDays(15).Day.ToString();
            string currentdate = DateTime.UtcNow.Day.ToString();
            int diff = int.Parse(futuredate) - int.Parse(currentdate);
            //ViewBag.accessexpiry = userDetails.SubscriptionDate.Value.Date.AddDays(15) - userDetails.SubscriptionDate.Value.Date;
            ViewBag.accessexpiry = diff;
            return View(userDetails);
        }
    }
}