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
using System.Data.SqlClient;
using Inventory.Utility;
using System.IO;

namespace Inventory.Controllers
{
    public class ProductsController : Controller
    {
        LoginService loginService = new LoginService();
        // GET: Products
        public ActionResult Index(string cid, string cname)
        {
            if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
            {
                return View();
            }
            return RedirectToAction("Index", "Login");
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
                               sub_category = row["sub_category"].ToString(),
                               Quantity = row["availableqty"].ToString(),
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
                                                         product_images = row["productimage"].ToString(),
                                                         Quantity_Total = row["quantity"].ToString(),
                                                         product_type = row["producttype"].ToString(),
                                                         discount = row["discount"].ToString(),
                                                         product_consumable = row["consumable"].ToString(),
                                                     }).ToList();
                ViewBag.records = subcategoryproducts;
                //ViewBag.loc = loginService.GetUserProfile((int.Parse(user.ID))).FirstOrDefault().Currency.Split('(')[1].Replace(")", "");
                var currency = loginService.GetUserProfile((int.Parse(user.ID))).FirstOrDefault().Currency;
                if (currency != "" && currency != null)
                    ViewBag.loc = currency.Split('(')[1].Replace(")", "");
                else
                    ViewBag.loc = "$";
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
                var products = (from DataRow row in dt.Rows
                                select new Product()
                                {
                                    product_name = row["product_name"].ToString(),
                                    brand = row["brand"].ToString(),
                                    distinctproducts = row["BATCHNOLIST"].ToString(),
                                    product_images = row["productimage"].ToString(),
                                    Quantity_Total = row["quantity"].ToString(),
                                    Qty_Stock = row["ids"].ToString(),
                                    product_type = row["producttype"].ToString(),
                                    discount = row["discount"].ToString(),
                                    product_consumable = row["consumable"].ToString(),
                                }).ToList();
                List<Product> sorteddata = new List<Product>();
                for (int i = 0; i < products.Count; i++)
                {
                    if (products[i].Quantity_Total == "0" || products[i].Quantity_Total == "")
                    {

                    }
                    else
                    {
                        sorteddata.Add(new Product
                        {
                            product_name = products[i].product_name,
                            brand = products[i].brand,
                            distinctproducts = products[i].distinctproducts,
                            product_images = products[i].product_images,
                            Quantity_Total = products[i].Quantity_Total,
                            Qty_Stock = products[i].Qty_Stock,
                        });
                    }
                }
                //ViewBag.records = sorteddata;
                ViewBag.records = products;
                var currency = loginService.GetUserProfile((int.Parse(user.ID))).FirstOrDefault().Currency;
                if (currency != "" && currency != null)
                    ViewBag.loc = currency.Split('(')[1].Replace(")", "");
                else
                    ViewBag.loc = "$";
                return PartialView("allproducts", ViewBag.records);
            }
            return PartialView("allproducts", null);
        }


        //for images

        //public PartialViewResult allImagesonPid(string product_id)
        //{
        //    if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
        //    {
        //        var dt = new DataTable();
        //        var user = (CustomPrinciple)System.Web.HttpContext.Current.User;
        //        var records = ProductService.Getimages(user.DbName, product_id);
        //        dt.Load(records);
        //        List<Product> iamges = new List<Product>();
        //        iamges = (from DataRow row in dt.Rows
        //                  select new Product()
        //                  {
        //                      product_images = row["product_images"].ToString(),
        //                  }).ToList();
        //        ViewBag.records = iamges;

        //        return PartialView("allImagesonPid", ViewBag.records);
        //    }
        //    return PartialView("allImagesonPid", null);
        //}
        [HttpGet]
        public JsonResult allImagesonPid(string product_id)
        {
            if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
            {
                //getting particular image
                #region particular image
                var dt = new DataTable();
                var user = (CustomPrinciple)System.Web.HttpContext.Current.User;
                var records = ProductService.Getimages(user.DbName, product_id);
                dt.Load(records);
                List<Product> images = new List<Product>();
                images = (from DataRow row in dt.Rows
                          select new Product()
                          {
                              product_images = row["product_images"].ToString(),
                              discount = row["discount"].ToString(),
                              product_type = row["product_type"].ToString(),
                          }).ToList();
                if (images.Count != 0)
                    ViewBag.records = images[0].product_images.Split(',')[0];
                else
                    ViewBag.records = "";
                #endregion
                //getting qty in hand from warehouse
                #region Qty in hand
                records = ProductService.GetqtyInHand(user.DbName, product_id);
                dt = new DataTable();
                dt.Load(records);
                List<Product> qtyinhnd = new List<Product>();
                qtyinhnd = (from DataRow row in dt.Rows
                            select new Product()
                            {
                                Quantity = row["Qty"].ToString(),
                                Quantity_Total = row["Total"].ToString()
                            }).ToList();
                var quantity = "";
                if (qtyinhnd.Count > 0)
                { quantity = qtyinhnd.FirstOrDefault().Quantity_Total; }//qtyinhnd.Select(m => m.Quantity_Total);
                else { quantity = ""; }
                #endregion
                var result = new { images = ViewBag.records, qty = quantity,discount=images[0].discount, product_type=images[0].product_type };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            return Json("unique", JsonRequestBehavior.AllowGet);
        }


        public JsonResult Addtocart(string cid, string product_name, string cost_price, string Quantity, string Measurement, string total_price, string product_images, string product_id)
        {
            if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
            {
                var user = (CustomPrinciple)System.Web.HttpContext.Current.User;
                //var records = ProductService.GetqtyInHand(user.DbName, product_id);
                //var dt = new DataTable();
                //dt.Load(records);
                //List<Product> qtyinhnd = new List<Product>();
                //qtyinhnd = (from DataRow row in dt.Rows
                //            select new Product()
                //            {
                //                Quantity = row["Qty"].ToString(),
                //                Quantity_Total = row["Total"].ToString()
                //            }).ToList();
                //var quantity = qtyinhnd.Select(m => m.Quantity_Total);
                //if (int.Parse(Quantity) < int.Parse(quantity.ToString()))
                //{
                var counts = ProductService.checkcartdata(user.DbName, product_id,cid);
                if (counts.HasRows)
                {
                    return Json("exists");
                }
                else
                {
                    if (int.Parse(Quantity) > 0)
                    {
                        ProductService.Addtocart(user.DbName, cid, product_id, product_name, cost_price, Quantity, Measurement, total_price, product_images);
                        return Json("success");
                    }
                    return Json("qty");
                }
                //}
                //return Json("qtyexcess");
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
            if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
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
                                                       product_images = row["product_images"].ToString(),
                                                       Measurement = row["Measurement"].ToString(),
                                                       Quantity_Total = row["warehouseqty"].ToString(),
                                                       total_price = row["total_price"].ToString(),
                                                   }).ToList();
                ViewBag.records = cartaddedproducts;
                ViewBag.totalamount = cartaddedproducts.Select(m => float.Parse(m.total_price)).Sum();
                ViewBag.cartqtycount = cartaddedproducts.Select(m => float.Parse(m.Quantity)).Sum();
                return PartialView("Addtocartpartial", ViewBag.records);
            }
            return PartialView("Addtocartpartial", "");
        }
        //for genRte pos
        [HttpGet]
        public PartialViewResult GenaratePOs(string cid, string cname, string type)
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
                                                       product_id = row["product_id"].ToString(),
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
                ViewBag.Productcount = ff;
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
        public JsonResult GenratePurchaseOrder(string cid, string cname, string created_date, string Prchaseorder_no, string shipping_date, string shipping_terms, string remarks, string sub_total, string grand_total)// float vat, float discount,
        {
            if (Prchaseorder_no.Contains(' '))
            {
                return Json("ponumspace");
            }
            else {
            var user = (CustomPrinciple)System.Web.HttpContext.Current.User;
            var dt = new DataTable();
            var records = ProductService.Getcartdata(user.DbName, cid);
            dt.Load(records);
            var county = ProductService.checkponum(user.DbName, Prchaseorder_no);
            if (county.HasRows) {
                        return Json("exists");
            }
            else
                { 
            if (shipping_date=="" || shipping_date==null) {
                return Json("shipdate");
            }
            else {
            List<Product> cartaddedproduct = (from DataRow row in dt.Rows
                                              select new Product()
                                              {
                                                  customer_id = row["customer_id"].ToString(),
                                                  product_id = row["product_id"].ToString(),
                                                  product_name = row["product_name"].ToString(),
                                                  cost_price = row["cost_price"].ToString(),
                                                  Quantity = row["Quantity"].ToString(),
                                                  //product_images = row["product_images"].ToString(),
                                                  Measurement = row["Measurement"].ToString(),
                                                  total_price = row["total_price"].ToString(),
                                              }).ToList();
            var ff = cartaddedproduct.Count;
            int count = 0;
                    ViewBag.Productcount = ff;
            if (Prchaseorder_no != "")
            {

                for (int i = 0; i < ff; i++)
                {
                    string product_id = (cartaddedproduct.Select(m => m.product_id).ToList())[i];
                    string product_name = (cartaddedproduct.Select(m => m.product_name).ToList())[i];
                    string price = (cartaddedproduct.Select(m => m.cost_price).ToList())[i];
                    string quantity = (cartaddedproduct.Select(m => m.Quantity).ToList())[i];
                    string description = (cartaddedproduct.Select(m => m.Measurement).ToList())[i];
                    string total_price = (cartaddedproduct.Select(m => m.total_price).ToList())[i];
                    int removeditems = UpdateStock(user.DbName, product_id, quantity);
                    if (removeditems > 0)
                    {
                        count = ProductService.GenaratePurchaseOrder(user.DbName, cid, product_id, cname.Replace("%20", " "), created_date, Prchaseorder_no, shipping_date, shipping_terms, product_name, description, quantity, price, total_price, remarks, sub_total, grand_total);
                        string status = 0.ToString();
                        var dt1 = new DataTable();
                        var records1 = InvoiceService.Getposforcustomer(user.DbName, cid, status);
                        dt1.Load(records1);
                        List<Invoice> pos = (from DataRow row in dt1.Rows
                                             select new Invoice()
                                             {
                                                 new_pos = row["pos"].ToString(),
                                             }).ToList();
                        string new_pos = pos.Select(m => m.new_pos).ToList().First();
                        InvoiceService.UpdatenewPoinCustomer(user.DbName, cid, new_pos);
                    }
                    else
                    {
                        return Json("out of stock");
                    }
                }
                if (count > 0)
                {
                    ProductService.Emptycart(user.DbName, cid);
                    return Json("success");
                }
                //if (count > 0)
                //{
                //    ProductService.Emptycart(user.DbName, cid);
                //    return Json("success");
                //}
            }

            }
            }
            return Json("unique");
        }
        }

        //public JsonResult CheckPoNum(string Prchaseorder_no)
        //{
        //    if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
        //    {
        //        var user = (CustomPrinciple)System.Web.HttpContext.Current.User;

        //        var count = ProductService.checkponum(user.DbName, Prchaseorder_no);
        //        if (count.HasRows)
        //            return Json("exists");
        //    }
        //    return Json("unique");
        //}

        //for displaying pos of customer
        public PartialViewResult PosOfCustomer(string cid,string cname)
        {
            if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
            {
                var user = (CustomPrinciple)System.Web.HttpContext.Current.User;
                var dt = new DataTable();
                var records = ProductService.PosOfCustomer(user.DbName, cid);
                dt.Load(records);
                List<Product> posofcustom = (from DataRow row in dt.Rows
                                             select new Product()
                                             {
                                                 customer_id = row["customer_id"].ToString(),
                                                 company_name = cname,
                                                 Prchaseorder_no = row["Prchaseorder_no"].ToString(),
                                                 created_date = row["created_date"].ToString(),
                                                 deliverynote_status = row["deliverynote_status"].ToString(),
                                                 invoice_status = row["invoice_status"].ToString(),
                                             }).ToList();
                ViewBag.records = posofcustom;
                return PartialView("PosOfCustomer", ViewBag.records);
            }
            return PartialView("PosOfCustomer", null);
        }

        //for view of pos details

        //for displaying pos of customer
        public PartialViewResult ViewPoDetails(string Prchaseorder_no, string cid)
        {
            if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
            {
                var user = (CustomPrinciple)System.Web.HttpContext.Current.User;
                var dt = new DataTable();
                var records = ProductService.Getpodata(user.DbName, Prchaseorder_no);
                dt.Load(records);
                List<Product> podetails = (from DataRow row in dt.Rows
                                           select new Product()
                                           {

                                               Prchaseorder_no = row["Prchaseorder_no"].ToString(),
                                               //payment_terms = row["payment_terms"].ToString(),
                                               shipping_terms = row["shipping_terms"].ToString(),
                                               remarks = row["remarks"].ToString(),
                                               sub_total = row["sub_total"].ToString(),
                                               //vat = row["vat"].ToString(),
                                               //discount = row["discount"].ToString(),
                                               grand_total = row["grand_total"].ToString(),
                                               //total_price = row["total_price"].ToString(),
                                               created_date = row["created_date"].ToString(),
                                               //Payment_date = row["Payment_date"].ToString(),
                                               shipping_date = row["shipping_date"].ToString(),
                                           }).ToList();
                ViewBag.records = podetails;
                return PartialView("ViewPoDetails", ViewBag.records);
            }
            return PartialView("ViewPoDetails", null);
        }

        //for displaying pos of customer(multiple products partview)
        public PartialViewResult ViewPoproducts(string Prchaseorder_no, string cid)
        {
            if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
            {
                var user = (CustomPrinciple)System.Web.HttpContext.Current.User;
                var dt = new DataTable();
                var records = ProductService.Getpoproductdata(user.DbName, Prchaseorder_no);
                dt.Load(records);
                List<Product> podetails = (from DataRow row in dt.Rows
                                           select new Product()
                                           {
                                               Prchaseorder_no = Prchaseorder_no,
                                               product_id = row["product_id"].ToString(),
                                               product_name = row["product_name"].ToString(),
                                               description = row["description"].ToString(),
                                               Quantity = row["Quantity"].ToString(),
                                               cost_price = row["cost_price"].ToString(),
                                               total_price = row["total_price"].ToString(),
                                           }).ToList();
                ViewBag.records = podetails;
                return PartialView("ViewPoproducts", ViewBag.records);
            }
            return PartialView("ViewPoproducts", null);
        }

        public int UpdateStock(string dbname, string product_id, string quantity)
        {
            int count = 0;
            var records = ProductService.GetMaxWarehouseQty(dbname, product_id);
            DataTable dt = new DataTable();
            dt.Load(records);
            Product qty = (from DataRow row in dt.Rows select new Product() { ID = row["id"].ToString(), Quantity = row["Qty"].ToString(), Quantity_Total = row["Total"].ToString() }).FirstOrDefault();
            int updatedqty = int.Parse(qty.Quantity) - int.Parse(quantity);
            int updatedTotal = int.Parse(qty.Quantity_Total) - int.Parse(quantity);
            if (updatedTotal >= 0)
                count = ProductService.updateMaxWarehouseQty(dbname, product_id, qty.ID, updatedqty, updatedTotal.ToString());
            else
                count = 0;
            return count;
        }

        //for email of purchase order

        public JsonResult Email(string POdata, string EmailID, string Grandtotal,string Companyname, string Shipdate, string Pnums,string ProductCount)
        {
            // Designing Email Part
            SendEmail abc = new SendEmail();
            string activationCode = Guid.NewGuid().ToString();
            string url = Request.Url.Scheme + "://" + Request.Url.Authority + "/Login/ActivateEmail?ActivationCode=" + activationCode + "&&Email=" + EmailID;
            FileInfo File = new FileInfo(Server.MapPath("/Content/purchaseorder.html"));
            string readFile = File.OpenText().ReadToEnd();
            readFile = readFile.Replace("[POData]", POdata);
            readFile = readFile.Replace("[Purchaseorder]", Pnums);
            readFile = readFile.Replace("[GrandTotal]", Grandtotal);
            readFile = readFile.Replace("[CompanyName]", Companyname);
            readFile = readFile.Replace("[ShipDate]", Shipdate);
            readFile = readFile.Replace("[PNums]", Pnums);
            readFile = readFile.Replace("[Productcount]", ProductCount);
            string message = readFile;
            abc.EmailAvtivation(EmailID, message, "Purchase Order Details");
            return Json("success");
        }
        

    }
}


