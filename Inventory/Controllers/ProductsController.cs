using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Inventory.Controllers
{
    public class ProductsController : Controller
    {
        // GET: Products
        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult purchaseorder()
        {
            return View();
        }
        //public ActionResult purchaseorderback()
        //{
        //    //return RedirectToAction("purchaseorder");
        //    return View("Index");
        //}
    }
}