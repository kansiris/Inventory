using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Inventory.Controllers
{
    public class Error403Controller : Controller
    {
        // GET: Error403
        public ActionResult Index()
        {
            return View();
        }
    }
}