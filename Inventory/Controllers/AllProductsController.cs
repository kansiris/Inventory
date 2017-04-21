using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Inventory.Service;
using Inventory.Content;
using System.Data;
using Inventory.Models;

namespace Inventory.Controllers
{
    public class AllProductsController : Controller
    {
        // GET: AllProducts
        public ActionResult Index()
        {
            if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
            {
                var user = (CustomPrinciple)System.Web.HttpContext.Current.User;
                var data = ProductService.GetAllProducts(user.DbName);
                DataTable dt = new DataTable();
                dt.Load(data);
                ViewBag.products = (from DataRow row in dt.Rows
                                    select new Product()
                                    {
                                        ID = row["ID"].ToString(),
                                        product_id = row["product_id"].ToString(),
                                        product_name = row["product_name"].ToString(),
                                        Measurement = row["Measurement"].ToString(),
                                        weight = row["weight"].ToString(),
                                        total_price = row["total_price"].ToString(),
                                        brand = row["brand"].ToString(),
                                        model = row["model"].ToString(),
                                    }).ToList().OrderByDescending(m=>m.ID);
            }
            return View();
        }
    }
}