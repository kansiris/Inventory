using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Inventory.Controllers
{
    public class FixedSidebarController : Controller
    {
        // GET: FixedSidebar
        public ActionResult Index()
        {
            return View();
        }
    }
}