using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Inventory.Service;
using Inventory.Models;

namespace Inventory.Controllers
{
    public class FranchiseHomeController : Controller
    {
        // GET: FranchiseHome
        public ActionResult Index(UserMaster userDetails)
        {
            return View(userDetails);
        }
    }
}