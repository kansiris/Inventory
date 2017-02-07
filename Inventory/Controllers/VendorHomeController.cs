using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Inventory.Controllers
{
    public class VendorHomeController : Controller
    {
        // GET: VendorHome
        public ActionResult Index()
        {
            return View();
        }
    }
}