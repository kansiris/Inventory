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

namespace Inventory.Controllers
{
    public class InvoiceController : Controller
    {
        // GET: Invoice
        public ActionResult Index(string cid, string cname)
        {
            return View();
        }

        //for displaying pos of customer
        public PartialViewResult AvailblePos(string cid)
        {

            if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
            {
                var user = (CustomPrinciple)System.Web.HttpContext.Current.User;
                var dt = new DataTable();
                var records = InvoiceService.AvailablePos(user.DbName, cid);
                dt.Load(records);
                List<Invoice> availablepos = (from DataRow row in dt.Rows
                                              select new Invoice()
                                              {
                                                  customer_id = row["customer_id"].ToString(),
                                                  company_name = row["company_name"].ToString(),
                                                  Prchaseorder_no = row["Prchaseorder_no"].ToString(),
                                                  total_price = row["total_price"].ToString(),
                                                  created_date = row["created_date"].ToString(),
                                                  Payment_date = row["Payment_date"].ToString(),
                                                  totalQty = row["totalQty"].ToString()
                                              }).ToList();
                ViewBag.records = availablepos;
                return PartialView("AvailblePos", ViewBag.records);
            }
            return PartialView("AvailblePos", null);
        }

        //for generating invoice
        public PartialViewResult GenarateInvoice(string cid, string Prchaseorder_nos, string customer_name)
        {

            if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
            {

                if (Prchaseorder_nos != null)
                {
                    Array ponumsArray = Prchaseorder_nos.Split(',');

                    //int i = 0;
                    var user = (CustomPrinciple)System.Web.HttpContext.Current.User;
                    var dt = new DataTable();
                    for (int i = 0; i < ponumsArray.Length; i++)
                    {
                        var records = InvoiceService.GetPodata(user.DbName, cid, Prchaseorder_nos.Split(',')[i]);
                        dt.Load(records);
                        List<Invoice> productsinpos = (from DataRow row in dt.Rows
                                                       select new Invoice()
                                                       {
                                                           customer_id = cid,
                                                           company_name=customer_name,
                                                           Prchaseorder_no = row["Prchaseorder_no"].ToString(),
                                                           product_id = row["product_id"].ToString(),
                                                           product_name = row["product_name"].ToString(),
                                                           Quantity = row["Quantity"].ToString(),
                                                           description = row["description"].ToString(),
                                                           cost_price = row["cost_price"].ToString(),
                                                           total_price = row["total_price"].ToString(),
                                                           vat = row["vat"].ToString(),
                                                           discount = row["discount"].ToString(),
                                                           sub_total = row["sub_total"].ToString(),
                                                           grand_total = row["grand_total"].ToString(),
                                                       }).ToList();
                        ViewBag.records = productsinpos;
                        ViewBag.customer_id = cid;
                        ViewBag.company_name = customer_name;
                        ViewBag.vat = productsinpos.Select(m => m.vat).First();
                        ViewBag.discount = productsinpos.Select(m => m.discount).First();
                        ViewBag.sub_total = productsinpos.Select(m => float.Parse(m.sub_total)).Distinct().Sum();
                        ViewBag.grand_total = productsinpos.Select(m => float.Parse(m.grand_total)).Distinct().Sum();
                    }

                    return PartialView("GenarateInvoice", ViewBag.records);
                }
            }
            return PartialView("GenarateInvoice", null);
        }

        public JsonResult GenarateInvoicejson(string cid, string Prchaseorder_nos, string customer_name)
        {
            var sample = GenarateInvoice(cid, Prchaseorder_nos, customer_name);

            if (sample != null)
            {
                return Json("success");
            }
            return Json("unique");
        }


        //for generating Delivery Note
        public PartialViewResult GenarateDeliveryNote(string cid, string Prchaseorder_nos)
        {

            if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
            {

                if (Prchaseorder_nos != null)
                {
                    Array ponumsArray = Prchaseorder_nos.Split(',');

                    //int i = 0;
                    var user = (CustomPrinciple)System.Web.HttpContext.Current.User;
                    var dt = new DataTable();
                    for (int i = 0; i < ponumsArray.Length; i++)
                    {
                        var records = InvoiceService.GetPodata(user.DbName, cid, Prchaseorder_nos.Split(',')[i]);
                        dt.Load(records);
                        List<Invoice> productsinpos = (from DataRow row in dt.Rows
                                                       select new Invoice()
                                                       {
                                                           customer_id = cid,
                                                           Prchaseorder_no = row["Prchaseorder_no"].ToString(),
                                                           product_id = row["product_id"].ToString(),
                                                           product_name = row["product_name"].ToString(),
                                                           Quantity = row["Quantity"].ToString(),
                                                           description = row["description"].ToString(),
                                                           cost_price = row["cost_price"].ToString(),
                                                           total_price = row["total_price"].ToString(),
                                                           vat = row["vat"].ToString(),
                                                           discount = row["discount"].ToString(),
                                                           sub_total = row["sub_total"].ToString(),
                                                           grand_total = row["grand_total"].ToString(),
                                                       }).ToList();
                        ViewBag.records = productsinpos;
                        ViewBag.customer_id = cid;
                        ViewBag.vat = productsinpos.Select(m => m.vat).First();
                        ViewBag.discount = productsinpos.Select(m => m.discount).First();
                        ViewBag.sub_total = productsinpos.Select(m => float.Parse(m.sub_total)).Distinct().Sum();
                        ViewBag.grand_total = productsinpos.Select(m => float.Parse(m.grand_total)).Distinct().Sum();
                    }
                    return PartialView("GenarateDeliveryNote", ViewBag.records);
                }
            }
            return PartialView("GenarateDeliveryNote", null);
        }

        //Genarate delivery note
        public JsonResult GenarateDelivjson(string cid, string Prchaseorder_nos)
        {
            var sample = GenarateDeliveryNote(cid, Prchaseorder_nos);

            if (sample != null)
            {
                return Json("success");
            }
            return Json("unique");
        }

        //to get customer data

        public PartialViewResult GetCustomerdata(string cid)
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
                return PartialView("GetCustomerdata", ViewBag.records);
            }

            return PartialView("GetCustomerdata", null);
        }

    }
}