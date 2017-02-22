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
    }
}