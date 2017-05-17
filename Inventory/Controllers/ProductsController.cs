﻿using Inventory.Content;
using Inventory.Models;
using Inventory.Service;
using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Inventory.Controllers
{
    public class ProductsController : Controller
    {

        // GET: Products
        public ActionResult Index(string cid)
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

        public JsonResult Addtocart(string product_name, string cost_price,string Quantity,string Measurement, /*string weight,*/ string total_price)
        {
            
            if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
            {
                var user = (CustomPrinciple)System.Web.HttpContext.Current.User;
                var sample = Addtocartpartial(user.DbName, user.ID);
                int counts= ProductService.checkcartdata(user.DbName,product_name,Measurement);
                if (counts<1) {
                int count = ProductService.Addtocart(user.DbName,user.ID, product_name, cost_price, Quantity, Measurement, total_price);
                 if (count>0)
                 return Json("success");
                }
                else
                {
                    return Json("unique");
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
                                                   cart_id= int.Parse(row["cart_id"].ToString()),
                                                   product_name = row["product_name"].ToString(),
                                                   cost_price = row["cost_price"].ToString(),
                                                   Quantity= row["Quantity"].ToString(),
                                                  //product_images = row["product_images"].ToString(),
                                                   Measurement = row["Measurement"].ToString(),
                                                   //weight = row["brand"].ToString(),
                                                   total_price = row["total_price"].ToString(),
                                               }).ToList();
                        ViewBag.records = cartaddedproducts;
            ViewBag.totalamount = cartaddedproducts.Select(m => int.Parse(m.total_price)).Sum();
            ViewBag.cartqtycount = cartaddedproducts.Select(m => int.Parse(m.Quantity)).Sum();
            return PartialView("Addtocartpartial", ViewBag.records);
                   }
        //for genRte pos
        [HttpGet]
        public PartialViewResult GenaratePOs(string cid)
        {

            if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
            {
                var user = (CustomPrinciple)System.Web.HttpContext.Current.User;
                var dt = new DataTable();
                var records = ProductService.Getcartdata(user.DbName, user.ID);
                dt.Load(records);
                
                List<Product> cartaddedproducts = (from DataRow row in dt.Rows
                                                   select new Product()
                                                   {
                                                       ID = row["id"].ToString(),
                                                       cart_id = int.Parse(row["cart_id"].ToString()),
                                                       product_name = row["product_name"].ToString(),
                                                       //brand = row["brand"].ToString(),
                                                       Quantity = row["Quantity"].ToString(),
                                                       //product_images = row["product_images"].ToString(),
                                                       Measurement = row["Measurement"].ToString(),
                                                       total_price = row["total_price"].ToString(),
                                                   }).ToList();
              
                
                ViewBag.records = cartaddedproducts;
                ViewBag.totalamount = cartaddedproducts.Select(m => int.Parse(m.total_price)).Sum();
                return PartialView("GenaratePOs",ViewBag.records);
            }
            return PartialView("GenaratePOs", null);
        }

        public PartialViewResult CustomerforPOs(string cid)
        {

            if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
            {
                var user = (CustomPrinciple)System.Web.HttpContext.Current.User;
               
                var dt = new DataTable();
                var records = CustomerService.getAllDetailsByCompany_Id(user.DbName, cid);
                dt.Load(records);
                List<Customer> customerdata = (from DataRow row in dt.Rows
                                               select new Customer()
                                               {
                                                   cus_company_name = row["cus_company_name"].ToString(),
                                                   cus_email = row["cus_email"].ToString(),
                                                   cus_logo = row["cus_logo"].ToString(),
                                                   bill_street = row["bill_street"].ToString(),
                                                   bill_city = row["bill_city"].ToString(),
                                                   bill_state = row["bill_state"].ToString(),
                                                   bill_postalcode = row["bill_postalcode"].ToString(),
                                                   bill_country = row["bill_country"].ToString(),
                                                   ship_street = row["ship_street"].ToString(),
                                                   ship_city = row["ship_city"].ToString(),
                                                   ship_state = row["ship_state"].ToString(),
                                                   ship_postalcode = row["ship_postalcode"].ToString(),
                                                   ship_country = row["ship_country"].ToString()
                                               }).ToList();
                ViewBag.records = customerdata;
                return PartialView("CustomerforPOs", ViewBag.records);
            }
            return PartialView("CustomerforPOs", null);
        }


        //removing from cart
        public JsonResult Removefromcart(int cart_id)
        {
            if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
            {
                var user = (CustomPrinciple)System.Web.HttpContext.Current.User;
               
                int count = ProductService.Removefromcart(user.DbName,cart_id);
                if (count > 0)
                {
                    return Json("success");
                }
            }
            return Json("unique");
        }
    }
}


