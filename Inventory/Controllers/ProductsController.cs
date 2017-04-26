using Inventory.Content;
using Inventory.Models;
using Inventory.Service;
using System;
using System.Collections.Generic;
using System.Data;
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
       
        public PartialViewResult allproducts()
        {
            if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
            {
                var user = (CustomPrinciple)System.Web.HttpContext.Current.User;
                var records = ProductService.GetAllProducts(user.DbName);
                var dt = new DataTable();
                dt.Load(records);
                List<Product> allproducts = (from DataRow row in dt.Rows
                                             select new Product()
                                             {
                                                 product_name = row["product_name"].ToString(),
                                                 product_type = row["product_type"].ToString(),
                                                 cost_price = row["cost_price"].ToString(),
                                                 product_images = row["product_images"].ToString(),
                                                 brand= row["brand"].ToString()
                                             }).Distinct().ToList();
                ViewBag.records = allproducts;
                return PartialView("allproducts", ViewBag.records);
            }
            return PartialView("allproducts", null);
        }
    }
}