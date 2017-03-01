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
        //public ActionResult WarehouseAddress()
        //{
        //    return View();
        //}
        [HttpPost]
        public ActionResult Index(Warehouse wh)
        {

            int count = WHservice.WHaddressinsert(wh.wh_name, wh.wh_Shortname, wh.conperson, wh.Job_position, wh.phone, wh.Mobile, wh.Email, wh.Note, wh.bill_Street, wh.bill_City, wh.bill_State, wh.bill_Postalcode, wh.bill_Country, wh.ship_Street, wh.ship_City, wh.ship_State, wh.ship_Postalcode, wh.ship_Country);
            Response.Write("<script language='javascript' type='text/javascript'>alert('Warehouse Registration successful.') </script>");
            return View();
        }
    }
}