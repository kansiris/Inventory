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
using Inventory.Content;


namespace Inventory.Controllers
{
    public class WarehouseController : Controller
    {
        // GET: Warehouse

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(Warehouse wh)
        {
            var user = (CustomPrinciple)System.Web.HttpContext.Current.User;
            int count = WHservice.WHaddressinsert(wh.wh_name, wh.wh_Shortname, wh.conperson, wh.Job_position,
                wh.phone, wh.Mobile, wh.Email, wh.Note, wh.bill_Street, wh.bill_City, wh.bill_State, wh.bill_Postalcode, wh.bill_Country,
                wh.ship_Street, wh.ship_City, wh.ship_State, wh.ship_Postalcode, wh.ship_Country, user.DbName);
            if (count > 0)
                return Content("<script language='javascript' type='text/javascript'>alert('Warehouse Added successfully');location.href='" + @Url.Action("Index", "Warehouse") + "'</script>"); // Stays in Same View
            return Content("<script language='javascript' type='text/javascript'>alert('Failed!!!');location.href='" + @Url.Action("Index", "Warehouse") + "'</script>"); // Stays in Same View
        }
    }
}