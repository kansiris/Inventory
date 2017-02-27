using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Inventory.Service;
using Inventory.Models;
using System.Data.SqlClient;
using System.Web.Routing;
using System.Web.Caching;
using System.Globalization;
using Microsoft.Web.Administration;
using System.IO;
using System.Data;


namespace Inventory.Controllers
{
    public class WarehouseController : Controller
    {
        // GET: Warehouse

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult WarehouseAddress()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(Warehouse wh, string command)
        {

            int count = WHservice.warehouseinsert(wh.wh_name, wh.wh_Shortname);
            Response.Write("<script language='javascript' type='text/javascript'>alert('Warehouse Registration successful.') </script>");
            return View();
        }
        [HttpPost]
        public ActionResult WarehouseAddress(Warehouse wh)
        {
            int count = WHservice.WHaddressinsert(wh.Job_position, wh.phone, wh.Mobile, wh.Email, wh.conperson, wh.Note, wh.Billing_Address, wh.shipping_Address, wh.other_Address);
            Response.Write("<script language='javascript' type='text/javascript'>alert('Warehouse Registration successful.') </script>");
            return View();

        }


    }
}