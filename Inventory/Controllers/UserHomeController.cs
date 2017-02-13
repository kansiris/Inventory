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
            string futuredate = userDetails.SubscriptionDate.Value.Date.AddDays(15).Day.ToString(); // adding 15 days to subscription date
            string currentdate = DateTime.UtcNow.Day.ToString(); //getting current date
            int diff = int.Parse(futuredate) - int.Parse(currentdate);//calculating difference b/w current date & future date
            //ViewBag.accessexpiry = userDetails.SubscriptionDate.Value.Date.AddDays(15) - userDetails.SubscriptionDate.Value.Date;
            ViewBag.accessexpiry = diff; // passing value to view
            return View(userDetails);
        }
    }
}