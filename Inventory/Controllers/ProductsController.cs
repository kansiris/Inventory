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

        //public PartialViewResult allproducts()
        //{
        //    if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
        //    {
        //        var user = (CustomPrinciple)System.Web.HttpContext.Current.User;
        //        var records = ProductService.GetAllProducts(user.DbName);

        //        var dt = new DataTable();
        //        dt.Load(records);
        //        List<Product> allproducts = (from DataRow row in dt.Rows
        //                                     select new Product()
        //                                     {
        //                                         product_name = row["product_name"].ToString(),
        //                                         product_type = row["product_type"].ToString(),
        //                                         cost_price = row["cost_price"].ToString(),
        //                                         product_images = row["product_images"].ToString().Split(',')[0],
        //                                         brand = row["brand"].ToString()
        //                                     }).Distinct().ToList();
        //        ViewBag.records = allproducts;
        //        return PartialView("allproducts", ViewBag.records);
        //    }
        //    return PartialView("allproducts", null);
        //}
        [HttpGet]
        //for subcategory
        public PartialViewResult Getproductsbysubcategory(string sub_category)
        {

            if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
            {
                //string category = Request.QueryString[sub_category];
                var user = (CustomPrinciple)System.Web.HttpContext.Current.User;
                var records = ProductService.Getproductsbysubcategory(user.DbName, sub_category);
                var dt = new DataTable();
                dt.Load(records);
                List<Product> subcategoryproducts = (from DataRow row in dt.Rows
                                                     select new Product()
                                                     {
                                                         product_name = row["product_name"].ToString(),
                                                         brand = row["brand"].ToString(),
                                                         total_price = row["total_price"].ToString(),
                                                         distinctproducts = row["BATCHNOLIST"].ToString(),



                                                         //product_name = row["product_name"].ToString(),
                                                         //product_type = row["product_type"].ToString(),
                                                         //cost_price = row["cost_price"].ToString(),
                                                         //Measurement = row["Measurement"].ToString() + row["weight"].ToString(),
                                                         //total_price = row["total_price"].ToString(),
                                                         //product_images = row["product_images"].ToString().Split(',')[0],
                                                         //brand = row["brand"].ToString()
                                                     }).ToList();
                ViewBag.records = subcategoryproducts.Distinct();
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
            var dt = new DataTable();
            var user = (CustomPrinciple)System.Web.HttpContext.Current.User;
            var records = ProductService.Getdistinctproducts(user.DbName);
            dt.Load(records);
            List<Product> description = new List<Product>();
            description = (from DataRow row in dt.Rows
                           select new Product()
                           {
                               product_name = row["product_name"].ToString(),
                               brand= row["brand"].ToString(),
                               total_price = row["total_price"].ToString(),
                               distinctproducts = row["BATCHNOLIST"].ToString(),
                           }).ToList();
            return description;
        }
        
        //for mesurments

        // public PartialViewResult allproducts()
        //{

        //     List<Product> li = new List<Product>();
        //     List<Product> li1 = new List<Product>();
        //     List<Product> li2 = new List<Product>();
        //     List<Product> description = new List<Product>();
        //     li = getdistinctproducts();
        //     for (int i = 0; i < li.Count; i++)
        //     {
        //         var dt = new DataTable();
        //         var user = (CustomPrinciple)System.Web.HttpContext.Current.User;
        //         string product_name = li[i].product_name;
        //         string distinct = li[i].distinctproducts;
        //         if (product_name != "" && distinct != "")
        //         {
        //             //li2 = getweight(product_name);
        //             string[] StrSpli = distinct.Split(',');
        //             for (int j = 0; j < StrSpli.Length; j = j + 2)
        //             {
        //                 //string Measurement = StrSpli[j];
        //                 //string total_price = StrSpli[j + 1];

        //                 var records = ProductService.Getdescripton(user.DbName, product_name);
        //                 dt.Load(records);
        //                 description = (from DataRow row in dt.Rows
        //                                select new Product()
        //                                {
        //                                    ID = row["ID"].ToString(),
        //                                    product_id = row["product_id"].ToString(),
        //                                    product_name = row["product_name"].ToString(),
        //                                    Measurement = distinct,
        //                                    cost_price = row["cost_price"].ToString(),
        //                                    weight = row["weight"].ToString(),
        //                                    total_price = row["total_price"].ToString(),
        //                                    brand = row["brand"].ToString(),
        //                                    model = row["model"].ToString(),
        //                                    product_images = row["product_images"].ToString().Split(',')[0],
        //                                    created_date = row["created_date"].ToString(),
        //                                }).ToList();
        //                 li1.AddRange(description);
        //                 ViewBag.records = li1;
        //             }
        //         }
        //           }
        //     return PartialView("allproducts", ViewBag.records);
        // }

        public PartialViewResult allproducts()
        {
            List<Product> li = new List<Product>();
            List<Product> li1 = new List<Product>();
            li = getdistinctproducts();
            for (int i = 0; i < li.Count; i++)
            {
                string product_name = li[i].product_name;
                string distinct = li[i].distinctproducts;
                if (product_name != "" && distinct != "")
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
                                       total_price = row["total_price"].ToString(),
                                       distinctproducts = row["BATCHNOLIST"].ToString(),
                                   }).ToList();
                                    ViewBag.records = description;
                                    }
                            }
            return PartialView("allproducts", ViewBag.records);
        }

    }
}
   
