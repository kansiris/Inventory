using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Inventory.Controllers
{
    public class GeneralFormController : Controller
    {
        // GET: GeneralForm
        public ActionResult Index()
        {
            return View();
        }
    }
}