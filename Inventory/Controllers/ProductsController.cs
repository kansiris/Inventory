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
                                                         product_name = row["product_name"].ToString(),
                                                         brand = row["brand"].ToString(),
                                                         distinctproducts = row["BATCHNOLIST"].ToString(),
                                                     }).ToList();
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
                                   product_name = row["product_name"].ToString(),
                                   brand = row["brand"].ToString(),
                                   distinctproducts = row["BATCHNOLIST"].ToString(),
                               }).ToList();
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
                                   product_name = row["product_name"].ToString(),
                                   brand = row["brand"].ToString(),
                                   distinctproducts = row["BATCHNOLIST"].ToString(),
                               }).ToList();
                ViewBag.records = description;

                return PartialView("allproducts", ViewBag.records);
            }
            return PartialView("allproducts", null);
        }
        //for Add to cart

        public JsonResult Addtocart(string brand,string product_name,string Quantity,string Measurement,string total_price)
        {
            if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
            {
                var user = (CustomPrinciple)System.Web.HttpContext.Current.User;
                var sample = Addtocartpartial(user.DbName, user.ID);
                int count = ProductService.Addtocart(user.DbName,user.ID,brand, product_name, Quantity, Measurement, total_price);
            if (count>0)
            {
                   return Json("success");
            }
          }
            return Json("unique");
        }
        public PartialViewResult Addtocartpartial(string dbname,string id)

        {
            var user = (CustomPrinciple)System.Web.HttpContext.Current.User;
            var records = ProductService.Addtocartbyid(user.DbName, user.ID);
            var dt = new DataTable();
            dt.Load(records);
            List<Product> cartaddedproducts = (from DataRow row in dt.Rows
                                               select new Product()
                                               {
                                                   ID= row["id"].ToString(),
                                                   //cart_id=int(row["cart_id"].ToString()),
                                                   product_name = row["product_name"].ToString(),
                                                   brand = row["brand"].ToString(),
                                                   Quantity= row["Quantity"].ToString(),
                                                  //product_images = row["product_images"].ToString(),
                                                   Measurement = row["Measurement"].ToString(),
                                                   total_price = row["total_price"].ToString(),
                                               }).ToList();
                        ViewBag.records = cartaddedproducts;
            return PartialView("Addtocartpartial", ViewBag.records);
                   }
        //for genRte pos
        public PartialViewResult GenaratePOs()
        {

            if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
            {

                //var user = (CustomPrinciple)System.Web.HttpContext.Current.User;
                //var records = ProductService.Getproductbyid(user.DbName, id);
                //var dt = new DataTable();
                //dt.Load(records);
                //List<Product> cartaddedproducts = (from DataRow row in dt.Rows
                //                                   select new Product()
                //                                   {
                //                                       product_name = row["product_name"].ToString(),
                //                                       brand = row["brand"].ToString(),
                //                                       Quantity = row["Quantity"].ToString(),
                //                                       //product_images = row["product_images"].ToString(),
                //                                       Measurement = row["Measurement"].ToString(),
                //                                       //weight = row["weight"].ToString(),
                //                                       total_price = row["total_price"].ToString(),
                //                                   }).ToList();
                //ViewBag.records = cartaddedproducts;
                return PartialView("GenaratePOs");
            }
            return PartialView("GenaratePOs", null);
        }

        public JsonResult Genaratepo()
        {
            //var sample = Addtocartpartial(id);

            //if (id != null)
            //{
                return Json("success");
            //}
            //return Json("unique");
        }
    }
}


