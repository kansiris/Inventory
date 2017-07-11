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
                                                  invoice_status = row["invoice_status"].ToString(),
                                                  //deliverynote_status = row["deliverynote_status"].ToString(),
                                                  totalQty = row["totalQty"].ToString()
                                              }).OrderByDescending(m => m.invoice_status).ToList();
                ViewBag.records = availablepos;
                ViewBag.invoice_status = availablepos.Select(m => m.invoice_status);
                //ViewBag.deliverynote_status = availablepos.Select(m => m.deliverynote_status);

                return PartialView("AvailblePos", ViewBag.records);
            }
            return PartialView("AvailblePos", null);
        }
        //availble pos fro deliverynote

        public PartialViewResult AvailblePosforDeliv(string cid)
        {

            if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
            {
                var user = (CustomPrinciple)System.Web.HttpContext.Current.User;
                var dt = new DataTable();
                var records = InvoiceService.AvailablePosforDeliv(user.DbName, cid);
                dt.Load(records);
                List<Invoice> availablepos = (from DataRow row in dt.Rows
                                              select new Invoice()
                                              {
                                                  customer_id = row["customer_id"].ToString(),
                                                  company_name = row["company_name"].ToString(),
                                                  Prchaseorder_no = row["Prchaseorder_no"].ToString(),
                                                  total_price = row["total_price"].ToString(),
                                                  created_date = row["created_date"].ToString(),
                                                  //invoice_status = row["invoice_status"].ToString(),
                                                  deliverynote_status = row["deliverynote_status"].ToString(),
                                                  totalQty = row["totalQty"].ToString()
                                              }).OrderByDescending(m => m.invoice_status).ToList();
                ViewBag.records = availablepos;
                //ViewBag.deliverynote_status = availablepos.Select(m => m.invoice_status);
                ViewBag.deliverynote_status = availablepos.Select(m => m.deliverynote_status);

                return PartialView("AvailblePosforDeliv", ViewBag.records);
            }
            return PartialView("AvailblePosforDeliv", null);
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
                                                           company_name = customer_name,
                                                           Prchaseorder_no = row["Prchaseorder_no"].ToString(),
                                                           product_id = row["product_id"].ToString(),
                                                           product_name = row["product_name"].ToString(),
                                                           Quantity = row["Quantity"].ToString(),
                                                           description = row["description"].ToString(),
                                                           cost_price = row["cost_price"].ToString(),
                                                           total_price = row["total_price"].ToString(),
                                                           //vat = row["vat"].ToString(),
                                                           //discount = row["discount"].ToString(),
                                                           sub_total = row["sub_total"].ToString(),
                                                           grand_total = row["grand_total"].ToString(),
                                                       }).ToList();
                        ViewBag.records = productsinpos;
                        ViewBag.customer_id = cid;
                        ViewBag.company_name = customer_name;
                        //ViewBag.vat = productsinpos.Select(m => m.vat).First();
                        // ViewBag.discount = productsinpos.Select(m => m.discount).First();
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
                                                           //vat = row["vat"].ToString(),
                                                           //discount = row["discount"].ToString(),
                                                           sub_total = row["sub_total"].ToString(),
                                                           grand_total = row["grand_total"].ToString(),
                                                       }).ToList();
                        ViewBag.records = productsinpos;
                        ViewBag.customer_id = cid;
                        // ViewBag.vat = productsinpos.Select(m => m.vat).First();
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

        //to insert invoice data



       


        public JsonResult InsertInvoice(string Invoice_no, string vendor_name, string customer_id, string company_name, string created_date, string payment_date, string grand_total, string payment_terms, string comment, string sub_total, string vat, string discount, string Prchaseorder_nos)
        {

            string status;
            if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
            {
                if (Prchaseorder_nos != null && Invoice_no != null)
                {
                    Array ponumsArray = Prchaseorder_nos.Split(',');
                    int count = 0;
                    CheckInvoiceNum(Invoice_no);
                    var user = (CustomPrinciple)System.Web.HttpContext.Current.User;
                    for (int i = 0; i < ponumsArray.Length; i++)
                    {

                        var dts = new DataTable();
                        var recordss = InvoiceService.Getproductdetails(user.DbName, customer_id, Prchaseorder_nos.Split(',')[i]);
                        dts.Load(recordss);

                        List<Invoice> productsinpo = (from DataRow row in dts.Rows
                                                      select new Invoice()
                                                      {
                                                          //customer_id = row["customer_id"].ToString(),
                                                          product_id = row["product_id"].ToString(),
                                                          product_name = row["product_name"].ToString(),
                                                          cost_price = row["cost_price"].ToString(),
                                                          Quantity = row["Quantity"].ToString(),
                                                          description = row["description"].ToString(),
                                                          total_price = row["total_price"].ToString(),
                                                      }).ToList();
                        var ff = productsinpo.Count;
                        status = 1.ToString();
                        for (int j = 0; j < ff; j++)
                        {
                            string product_id = (productsinpo.Select(m => m.product_id).ToList())[j];
                            string product_name = (productsinpo.Select(m => m.product_name).ToList())[j];
                            string cost_price = (productsinpo.Select(m => m.cost_price).ToList())[j];
                            string po_quantity = (productsinpo.Select(m => m.Quantity).ToList())[j];
                            string description = (productsinpo.Select(m => m.description).ToList())[j];
                            //string total_price = (productsinpo.Select(m => m.total_price).ToList())[i];
                            string deliver_quantity = (productsinpo.Select(m => m.Quantity).ToList())[j];//after this will be chnaged.
                            string total_price = (int.Parse(po_quantity) * float.Parse(cost_price)).ToString();

                            //dummy values for time sake
                            string cgst_rate = 2.ToString();
                            string cgst_amount = 200.ToString();
                            string sgst_rate = 3.ToString();
                            string sgst_amount = 300.ToString();
                            string igst_rate = 4.ToString();
                            string igst_amount = 400.ToString();


                            count = InvoiceService.InsertInvoice(user.DbName, Invoice_no, vendor_name, customer_id, company_name, created_date, payment_date, grand_total, payment_terms, comment, sub_total, vat, discount, Prchaseorder_nos.Split(',')[i], status
                                , product_id, product_name, cost_price, po_quantity, total_price, cgst_rate, cgst_amount, sgst_rate, sgst_amount, igst_rate, igst_amount);
                            count++;
                        }


                        //count = InvoiceService.InsertInvoice(user.DbName, Invoice_no, vendor_name, customer_id, company_name, created_date, payment_date, grand_total, payment_terms, comment, sub_total, vat, discount, Prchaseorder_nos.Split(',')[i], status);
                        if (count > 0)
                        {
                            InvoiceService.UpdatePoforInvoice(user.DbName, customer_id, Prchaseorder_nos.Split(',')[i], status);
                            var dt = new DataTable();
                            var records = InvoiceService.Getposforcustomer(user.DbName, customer_id, status);
                            dt.Load(records);
                            List<Invoice> pos = (from DataRow row in dt.Rows
                                                 select new Invoice()
                                                 {
                                                     total_pos = row["pos"].ToString(),
                                                 }).ToList();
                            string total_pos = (pos.Select(m => m.total_pos).ToList()).FirstOrDefault();
                            InvoiceService.UpdatePoinCustomer(user.DbName, customer_id, total_pos);
                        }
                        count++;
                    }
                    if (count > 0)
                        return Json("success");
                }
            }
            return Json("unique");
        }

        //for checking invoice number

        public JsonResult CheckInvoiceNum(string Invoice_no)
        {
            if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
            {
                var user = (CustomPrinciple)System.Web.HttpContext.Current.User;
                var count = InvoiceService.checkinvoicenum(user.DbName, Invoice_no);
                if (count.HasRows)
                    return Json("exists");
            }
            return Json("unique");
        }

        //for checking delivery note  number

        public JsonResult CheckDeliveryNoteNum(string Delivernote_no)
        {
            if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
            {
                var user = (CustomPrinciple)System.Web.HttpContext.Current.User;
                var count = InvoiceService.checkdeliverynotenum(user.DbName, Delivernote_no);
                if (count.HasRows)
                    return Json("exists");
            }
            return Json("unique");
        }

        //to insert DeliveryNote data

        public JsonResult InsertDeliveryNote(string Delivernote_no, string vendor_name, string customer_id, string created_date, string comment, string sub_total, string Prchaseorder_nos)
        {

            string deliv_status;
            if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
            {
                if (Prchaseorder_nos != null && Delivernote_no != null)
                {
                    Array ponumsArray = Prchaseorder_nos.Split(',');
                    int count = 0;
                    CheckDeliveryNoteNum(Delivernote_no);
                    var user = (CustomPrinciple)System.Web.HttpContext.Current.User;

                    for (int j = 0; j < ponumsArray.Length; j++)
                    {
                        var dt = new DataTable();
                        var records = InvoiceService.Getproductdetails(user.DbName, customer_id, Prchaseorder_nos.Split(',')[j]);
                        dt.Load(records);
                        List<Invoice> productsinpo = (from DataRow row in dt.Rows
                                                      select new Invoice()
                                                      {
                                                          //customer_id = row["customer_id"].ToString(),
                                                          product_id = row["product_id"].ToString(),
                                                          product_name = row["product_name"].ToString(),
                                                          cost_price = row["cost_price"].ToString(),
                                                          Quantity = row["Quantity"].ToString(),
                                                          description = row["description"].ToString(),
                                                          total_price = row["total_price"].ToString(),
                                                      }).ToList();
                        var ff = productsinpo.Count;
                        for (int i = 0; i < ff; i++)
                        {
                            string product_id = (productsinpo.Select(m => m.product_id).ToList())[i];
                            string product_name = (productsinpo.Select(m => m.product_name).ToList())[i];
                            string cost_price = (productsinpo.Select(m => m.cost_price).ToList())[i];
                            string po_quantity = (productsinpo.Select(m => m.Quantity).ToList())[i];
                            string description = (productsinpo.Select(m => m.description).ToList())[i];
                            //string total_price = (productsinpo.Select(m => m.total_price).ToList())[i];
                            string deliver_quantity = (productsinpo.Select(m => m.Quantity).ToList())[i];//after this will be chnaged.
                            string total_price = (int.Parse(deliver_quantity) * float.Parse(cost_price)).ToString();
                            count = InvoiceService.InsertDeliverynote(user.DbName, Delivernote_no, vendor_name, customer_id, created_date, comment, sub_total, Prchaseorder_nos.Split(',')[j], product_id, product_name, description, po_quantity, deliver_quantity, cost_price, total_price);
                            count++;
                        }
                        deliv_status = 1.ToString();
                        if (count > 0)
                        {
                            InvoiceService.UpdatePoforDeliveryNote(user.DbName, customer_id, Prchaseorder_nos.Split(',')[j], deliv_status);
                        }
                        count++;
                    }
                    if (count > 0)
                        return Json("success");
                }
            }
            return Json("unique");
        }

        //to update pos in customer_company


        //view invoice
        public PartialViewResult ViewInvoiceDetails(string cid, string Prchaseorder_no)
        {

            if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
            {

                if (Prchaseorder_no != null)
                {

                    var user = (CustomPrinciple)System.Web.HttpContext.Current.User;
                    var dt = new DataTable();

                    var records = InvoiceService.Getinvoicedata(user.DbName, cid, Prchaseorder_no);
                    dt.Load(records);
                    List<Invoice> productsinpos = (from DataRow row in dt.Rows
                                                   select new Invoice()
                                                   {
                                                       customer_id = cid,
                                                       company_name = row["company_name"].ToString(),
                                                       Prchaseorder_no = row["Prchaseorder_no"].ToString(),
                                                       Invoice_no = row["Invoice_no"].ToString(),
                                                       created_date = row["created_date"].ToString(),
                                                       Payment_date = row["payment_date"].ToString(),
                                                       payment_terms = row["payment_terms"].ToString(),
                                                       remarks = row["comment"].ToString(),
                                                       // description = row["description"].ToString(),
                                                       //cost_price = row["cost_price"].ToString(),
                                                       //total_price = row["total_price"].ToString(),
                                                       vat = row["vat"].ToString(),
                                                       discount = row["discount"].ToString(),
                                                       sub_total = row["sub_total"].ToString(),
                                                       grand_total = row["grand_total"].ToString(),
                                                   }).ToList();
                    ViewBag.records = productsinpos;
                    ViewBag.customer_id = cid;
                    ViewBag.Invoice_no = productsinpos.Select(m => m.Invoice_no).First();
                    ViewBag.created_date = productsinpos.Select(m => m.created_date).First();
                    ViewBag.Payment_date = productsinpos.Select(m => m.Payment_date).First();
                    ViewBag.payment_terms = productsinpos.Select(m => m.payment_terms).First();
                    ViewBag.remarks = productsinpos.Select(m => m.remarks).First();
                    ViewBag.company_name = productsinpos.Select(m => m.company_name).First();
                    ViewBag.vat = productsinpos.Select(m => m.vat).First();
                    ViewBag.discount = productsinpos.Select(m => m.discount).First();
                    ViewBag.sub_total = productsinpos.Select(m => m.sub_total).First();
                    ViewBag.grand_total = productsinpos.Select(m => m.grand_total).First();


                    return PartialView("ViewInvoiceDetails", ViewBag.records);
                }
            }
            return PartialView("ViewInvoiceDetails", null);
        }

        //to get all invoices
        public PartialViewResult AvailbleInvoices(string cid)
        {
            if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
            {
                var user = (CustomPrinciple)System.Web.HttpContext.Current.User;
                var dt = new DataTable();
                var records = InvoiceService.AvailableInvoices(user.DbName, cid);
                dt.Load(records);
                List<Invoice> availableinvoices = (from DataRow row in dt.Rows
                                                   select new Invoice()
                                                   {
                                                       customer_id = cid,
                                                       Invoice_no = row["Invoice_no"].ToString(),
                                                       company_name = row["company_name"].ToString(),
                                                       Prchaseorder_no = row["Prchaseorder_no"].ToString(),
                                                       grand_total = row["grand_total"].ToString(),
                                                       Payment_date = row["payment_date"].ToString(),
                                                   }).ToList();
                ViewBag.records = availableinvoices;

                return PartialView("AvailbleInvoices", ViewBag.records);
            }
            return PartialView("AvailbleInvoices", null);
        }
    }
}