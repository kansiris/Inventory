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
        public PartialViewResult GenarateInvoice(string cid, string Prchaseorder_nos)
        {
            Array ponumsArray = Prchaseorder_nos.Split(',');
            int arraylen = ponumsArray.Length;
            if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
            {
                var user = (CustomPrinciple)System.Web.HttpContext.Current.User;
                var dt = new DataTable();
                for (int i = 0; i <= arraylen; i++)
                {
                    var records = InvoiceService.GetPodata(user.DbName, cid, Prchaseorder_nos.Split(',')[i]);
                    dt.Load(records);
                    List<Invoice> productsinpos = (from DataRow row in dt.Rows
                                                   select new Invoice()
                                                   {
                                                       //customer_id = row["customer_id"].ToString(),
                                                       product_name = row["product_name"].ToString(),
                                                       Quantity = row["Quantity"].ToString(),
                                                       description=row["description"].ToString(),
                                                       cost_price = row["cost_price"].ToString(),
                                                       total_price = row["total_price"].ToString(),
                                                   }).ToList();
                    ViewBag.records = productsinpos;
                    return PartialView("GenarateInvoice", ViewBag.records);
                }
            }
            return PartialView("GenarateInvoice", null);
        }

    }
}