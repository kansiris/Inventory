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


        [HttpGet]
        //for subcategory
        public PartialViewResult Getproductsbysubcategory(string sub_category)
        {

            if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
            {

                var user = (CustomPrinciple)System.Web.HttpContext.Current.User;
                var records = ProductService.Getproductsbysubcategory(user.DbName, sub_category);
                var dt = new DataTable();
                dt.Load(records);
                List<Product> subcategoryproducts = (from DataRow row in dt.Rows
                                                     select new Product()
                                                     {
                                                         //product_id = row["product_id"].ToString(),
                                                         product_name = row["product_name"].ToString(),
                                                         brand = row["brand"].ToString(),
                                                         //total_price = row["total_price"].ToString(),
                                                         distinctproducts = row["BATCHNOLIST"].ToString(),
                                                     })/*.OrderByDescending(m => m.product_id)*/.ToList();
                ViewBag.records = subcategoryproducts;
                return PartialView("allproducts", ViewBag.records);
            }
            return PartialView("allproducts", null);
        }

        public JsonResult Getproducts(string sub_category)
        {
            var sample = Getproductsbysubcategory(sub_category);
            string myString = sub_category.Replace(" ", "-");
            if (sample != null)
            {
                return Json(myString);
            }
            return Json("unique");
        }

        //for decsription drop down
        public List<Product> getdistinctproducts()
        {
            if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
            {
                var dt = new DataTable();
                var user = (CustomPrinciple)System.Web.HttpContext.Current.User;
                var records = ProductService.Getdistinctproducts(user.DbName);
                dt.Load(records);
                List<Product> description = new List<Product>();
                description = (from DataRow row in dt.Rows
                               select new Product()
                               {
                                   //product_id = row["product_id"].ToString(),
                                   product_name = row["product_name"].ToString(),
                                   brand = row["brand"].ToString(),
                                   //total_price = row["total_price"].ToString(),
                                   distinctproducts = row["BATCHNOLIST"].ToString(),
                               })/*.OrderByDescending(m => m.product_id)*/.ToList();
                return description;
            }
            return null;
        }


        public PartialViewResult allproducts()
        {
            if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
            {
                var dt = new DataTable();
                var user = (CustomPrinciple)System.Web.HttpContext.Current.User;
                var records = ProductService.Getdistinctproducts(user.DbName);
                dt.Load(records);
                List<Product> description = new List<Product>();
                description = (from DataRow row in dt.Rows
                               select new Product()
                               {
                                   //product_id=row["product_id"].ToString(),
                                   product_name = row["product_name"].ToString(),
                                   brand = row["brand"].ToString(),
                                   //total_price = row["total_price"].ToString(),
                                   distinctproducts = row["BATCHNOLIST"].ToString(),
                               })/*.OrderByDescending(m => m.product_id)*/.ToList();
                ViewBag.records = description;

                return PartialView("allproducts", ViewBag.records);
            }
            return PartialView("allproducts", null);
        }
    }
}

   
