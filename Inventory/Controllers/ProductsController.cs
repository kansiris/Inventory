using Inventory.Content;
using Inventory.Models;
using Inventory.Service;
using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Inventory.Controllers
{
    public class ProductsController : Controller
    {

        // GET: Products
        public ActionResult Index(string cid, string cname)
        {
            return View();
        }

        public ActionResult purchaseorder()
        {
            return View();
        }

        //for loading all categories and sub categories

        public PartialViewResult AllCategories()
        {
            if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
            {

                var user = (CustomPrinciple)System.Web.HttpContext.Current.User;
                var records = ProductService.GetAllCategories(user.DbName);
                var dt = new DataTable();
                dt.Load(records);
                List<Product> product = new List<Product>();
                product = (from DataRow row in dt.Rows
                           select new Product()
                           {
                               ID = row["ID"].ToString(),
                               category_id = row["category_id"].ToString(),
                               category = row["category"].ToString()
                           }).OrderByDescending(m => m.ID).ToList();
                ViewBag.records = product;
                return PartialView("AllCategories", ViewBag.records);
            }

            return PartialView("AllCategories", null);
        }
        //for loading all subcategories based on  category
        public PartialViewResult AllSubCategories(string category_id)
        {
            if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
            {

                var user = (CustomPrinciple)System.Web.HttpContext.Current.User;
                var records = ProductService.GetAllSubCategories(user.DbName, category_id);
                var dt = new DataTable();
                dt.Load(records);
                List<Product> product = new List<Product>();
                product = (from DataRow row in dt.Rows
                           select new Product()
                           {
                               //ID = row["ID"].ToString(),
                               subcategory_id = row["subcategory_id"].ToString(),
                               category_id = row["category_id"].ToString(),
                               sub_category = row["sub_category"].ToString()
                           }).OrderByDescending(m => m.ID).ToList();
                ViewBag.records = product;
                return PartialView("AllSubCategories", ViewBag.records);
            }

            return PartialView("AllCategories", null);
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

        public JsonResult Addtocart(string cid, string product_name, string cost_price, string Quantity, string Measurement, string total_price)
        {

            if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
            {
                var user = (CustomPrinciple)System.Web.HttpContext.Current.User;
                //var sample = Addtocartpartial(user.DbName, user.ID);
                var counts = ProductService.checkcartdata(user.DbName, product_name, Measurement, cid);

                if (counts.HasRows)
                {
                    return Json("exists");
                }
                else
                {
                    if (int.Parse(Quantity) > 0)
                    {
                        ProductService.Addtocart(user.DbName, cid, product_name, cost_price, Quantity, Measurement, total_price);
                        return Json("success");
                    }
                    return Json("qty");

                }
            }
            return Json("unique");
        }

        //for updatecart
        public JsonResult UpdateCart(int cart_id, string Quantity, string total_price)
        {

            if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
            {
                var user = (CustomPrinciple)System.Web.HttpContext.Current.User;
               
                int count = ProductService.Updatecart(user.DbName, cart_id, Quantity, total_price);
                if (count > 0)
                   return Json("success");
                }
            return Json("unique");
        }

        public PartialViewResult Addtocartpartial(string cid)

        {
            var user = (CustomPrinciple)System.Web.HttpContext.Current.User;
            var records = ProductService.Addtocartbyid(user.DbName, cid);
            var dt = new DataTable();
            dt.Load(records);
            List<Product> cartaddedproducts = (from DataRow row in dt.Rows
                                               select new Product()
                                               {
                                                   customer_id = row["customer_id"].ToString(),
                                                   cart_id = int.Parse(row["cart_id"].ToString()),
                                                   product_name = row["product_name"].ToString(),
                                                   cost_price = row["cost_price"].ToString(),
                                                   Quantity = row["Quantity"].ToString(),
                                                   //product_images = row["product_images"].ToString(),
                                                   Measurement = row["Measurement"].ToString(),
                                                   //weight = row["brand"].ToString(),
                                                   total_price = row["total_price"].ToString(),
                                               }).ToList();
            ViewBag.records = cartaddedproducts;
            ViewBag.totalamount = cartaddedproducts.Select(m => float.Parse(m.total_price)).Sum();
            ViewBag.cartqtycount = cartaddedproducts.Select(m => float.Parse(m.Quantity)).Sum();
            return PartialView("Addtocartpartial", ViewBag.records);
        }
        //for genRte pos
        [HttpGet]
        public PartialViewResult GenaratePOs(string cid, string cname)
        {

            if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
            {
                var user = (CustomPrinciple)System.Web.HttpContext.Current.User;
                var dt = new DataTable();
                var records = ProductService.Getcartdata(user.DbName, cid);
                dt.Load(records);

                List<Product> cartaddedproducts = (from DataRow row in dt.Rows
                                                   select new Product()
                                                   {
                                                       customer_id = row["customer_id"].ToString(),
                                                       cart_id = int.Parse(row["cart_id"].ToString()),
                                                       product_name = row["product_name"].ToString(),
                                                       //brand = row["brand"].ToString(),
                                                       Quantity = row["Quantity"].ToString(),
                                                       //product_images = row["product_images"].ToString(),
                                                       Measurement = row["Measurement"].ToString(),
                                                       cost_price = row["cost_price"].ToString(),
                                                       total_price = row["total_price"].ToString(),
                                                   }).ToList();

                var ff = cartaddedproducts.Count;
                ViewBag.records = cartaddedproducts;
                ViewBag.customer_id = cid;
                ViewBag.company_name = cname;
                ViewBag.totalamount = cartaddedproducts.Select(m => float.Parse(m.total_price)).Sum();
                return PartialView("GenaratePOs", ViewBag.records);
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

                int count = ProductService.Removefromcart(user.DbName, cart_id);
                if (count > 0)
                {
                    return Json("success");
                }
            }
            return Json("unique");
        }
        //for genarate purchase order
        public JsonResult GenratePurchaseOrder(string cid, string cname, string created_date, string Prchaseorder_no, string Payment_date, string shipping_date, string payment_terms, string shipping_terms, string remarks, string sub_total, float vat, float discount, string grand_total)

        {
            //DateTime newcreated_date = DateTime.ParseExact(created_date, "dd-MM-yyyy", CultureInfo.InvariantCulture);
            //DateTime newPayment_date = DateTime.ParseExact(Payment_date, "dd-MM-yyyy", CultureInfo.InvariantCulture);
            //DateTime newshipping_date = DateTime.ParseExact(shipping_date, "dd-MM-yyyy", CultureInfo.InvariantCulture);
            var user = (CustomPrinciple)System.Web.HttpContext.Current.User;
            // int count = ProductService.GenaratePurchaseOrder(user.DbName, cid, cname, created_date, Prchaseorder_no, Payment_date, shipping_date, payment_terms, shipping_terms, remarks, sub_total, vat, discount, grand_total);
            var dt = new DataTable();
            var records = ProductService.Getcartdata(user.DbName, cid);
            dt.Load(records);
            
            List<Product> cartaddedproduct = (from DataRow row in dt.Rows
                                               select new Product()
                                               {
                                                   customer_id = row["customer_id"].ToString(),
                                                   product_name = row["product_name"].ToString(),
                                                   cost_price = row["cost_price"].ToString(),
                                                   Quantity = row["Quantity"].ToString(),
                                                   //product_images = row["product_images"].ToString(),
                                                   Measurement = row["Measurement"].ToString(),
                                                   total_price = row["total_price"].ToString(),
                                               }).ToList();
            var ff = cartaddedproduct.Count;
            int count = 0;
            //foreach (var author in cartaddedproducts)
            //{
            //    Console.WriteLine(author);
            //}
            for (int i = 0; i < ff; i++)
            {
                string product_name = (cartaddedproduct.Select(m => m.product_name).ToList())[i];
                //string product_name = product_names[i];
                //string product_name = (cartaddedproducts.Select(m => m.product_name).ToString()).ToList();
                //var prices = cartaddedproducts.Select(m => m.cost_price).ToList();
                string price = (cartaddedproduct.Select(m => m.cost_price).ToList())[i];
               
                //var quantitys = cartaddedproducts.Select(m => m.Quantity).ToList();
                string quantity = (cartaddedproduct.Select(m => m.Quantity).ToList())[i];
               
                //var descriptions = cartaddedproducts.Select(m => m.Measurement).ToList();
                string description = (cartaddedproduct.Select(m => m.Measurement).ToList())[i];
                
                string total_price = sub_total;

                count = ProductService.GenaratePurchaseOrder(user.DbName, cid, cname, created_date, Prchaseorder_no, Payment_date, shipping_date, payment_terms, shipping_terms, product_name, description, quantity, price, total_price, remarks, sub_total, vat, discount, grand_total);
                count++;
            }

            if (count > 0)
            {
                ProductService.Emptycart(user.DbName, cid);
                return Json("success");
            }
            return Json("unique");
        }
    }
}


