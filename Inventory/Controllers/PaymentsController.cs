﻿using Inventory.Content;
using Inventory.Models;
using Inventory.Service;
using Inventory.Utility;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Inventory.Controllers
{
    public class PaymentsController : Controller
    {
        public ActionResult Index(string cid, string cname)
        {
            if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
            {
                var user = (CustomPrinciple)System.Web.HttpContext.Current.User;
                //var dt1 = new DataTable();
                //var records1 = PaymentsService.checkcustomerinPayments(user.DbName, cid);
                var dt = new DataTable();
                var records = PaymentsService.InvoicesForPayment(user.DbName, cid);
                dt.Load(records);
                List<Invoice> availableinvoices = (from DataRow row in dt.Rows
                                                   select new Invoice()
                                                   {
                                                       customer_id = row["customer_id"].ToString(),
                                                       company_name = row["company_name"].ToString(),
                                                       Prchaseorder_no = row["Prchaseorder_no"].ToString(),
                                                       Invoice_no = row["Invoice_no"].ToString(),
                                                       total_price = row["total_price"].ToString(),
                                                       created_date = row["created_date"].ToString(),
                                                       Payment_date = row["payment_date"].ToString(),
                                                       invoice_status = row["invoice_status"].ToString(),
                                                       totalQty = row["totalQty"].ToString(),
                                                       open_amount = row["open_amount"].ToString()
                                                   }).OrderByDescending(m => m.invoice_status).ToList();
                ViewBag.records = availableinvoices;
                ViewBag.invoice_status = availableinvoices.Select(m => m.invoice_status);
                ViewBag.total_price = availableinvoices.Select(m => float.Parse(m.open_amount)).Sum();
                ViewBag.customer_id = cid;
                ViewBag.company_name = cname;
            }
            return View();
        }
        //public JsonResult InsertPayments(string Prchaseorder_no, string payments_date, string cheque_date, string cheque_bankname, string cheque_num, string creditORdebitcard_date, string card_holder_name, string card_last4digits, string bank_taransfer_date,
        //   string bank_transfer_name, string bank_transaction_id, string cash_date, string cash_card_holdername, string wallet_date, string wallet_number, string invoiced_amount, string Received_amount, string opening_balance,
        //   string current_balance, string bank_transfer_IFSCcode, string bank_transfer_branchname, string Customer_comapnyId, string Customer_company_name, string remarks)
        //{
        //inserting payments
        public JsonResult InsertPayments(string Prchaseorder_no, Payments payments)
        {
        
            if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
            {

                var user = (CustomPrinciple)System.Web.HttpContext.Current.User;
                var count = PaymentsService.InsertPayments(user.DbName, payments.payments_date, payments.cheque_date, payments.cheque_bankname, payments.cheque_num, payments.creditORdebitcard_date, payments.card_holder_name, payments.card_last4digits, payments.bank_taransfer_date, payments.bank_transfer_name, payments.bank_transaction_id
           , payments. cash_date, payments.cash_card_holdername, payments.wallet_date, payments.wallet_number, payments.invoiced_amount, payments.Received_amount, payments.opening_balance, payments.current_balance, payments.bank_transfer_IFSCcode, payments.bank_transfer_branchname, payments.Customer_comapnyId, payments.Customer_company_name, payments.remarks);
                if (count > 0)
                {
                    string overdue = "0";
                    string due = "0"; int updatedopenamt = 0; int updatedreceivedamount = int.Parse(payments.Received_amount); int updatedinvoiceamount = int.Parse(payments.invoiced_amount);
                    Array ponumsArray = Prchaseorder_no.Split(',');
                    for (int i = 0; i < ponumsArray.Length; i++)
                    {
                        if (int.Parse(payments.Received_amount) > 0)
                        {
                            var dt = new DataTable();
                            var records = PaymentsService.ForPaymentinvoicetotal(user.DbName, Prchaseorder_no.Split(',')[i]);
                            dt.Load(records);
                            List<Invoice> invoicetotl = (from DataRow row in dt.Rows
                                                         select new Invoice()
                                                         {
                                                             Payment_date = row["payment_date"].ToString(),
                                                             sub_total = row["sub_total"].ToString(),
                                                             open_amount = row["open_amount"].ToString()
                                                         }).ToList();
                            string Payment_due_date = (invoicetotl.Select(m => m.Payment_date)).First();
                            DateTime Payment_due_date1 = DateTime.ParseExact(Payment_due_date, "MM/dd/yyyy", CultureInfo.InvariantCulture);
                            Payment_due_date = Payment_due_date1.ToString("dd/MM/yyyy");
                            DateTime paymentsdonedate = DateTime.Parse(payments.payments_date);
                            string open_amount = invoicetotl.FirstOrDefault().open_amount; //(invoicetotl.Select(m => m.open_amount)).First();
                            DateTime myPayment_due_date = DateTime.Parse(Payment_due_date);
                            //string updatedopenamt = "0";
                            if (open_amount != "" && open_amount != null && open_amount != "0")  // && int.Parse(open_amount) >= int.Parse(Received_amount)
                            {
                                if ((int.Parse(open_amount)) >= (int.Parse(payments.Received_amount)))
                                {
                                    updatedopenamt = (int.Parse(open_amount) - int.Parse(payments.Received_amount));
                                    PaymentsService.Updateinvoice(user.DbName, Prchaseorder_no.Split(',')[i], updatedopenamt.ToString());
                                    payments.Received_amount = "0";
                                }
                                else
                                {
                                    updatedreceivedamount = (int.Parse(payments.Received_amount) - int.Parse(open_amount));
                                    if (updatedreceivedamount > 0)
                                    {
                                        payments.Received_amount = updatedreceivedamount.ToString();
                                        updatedopenamt = 0;
                                        PaymentsService.Updateinvoice(user.DbName, Prchaseorder_no.Split(',')[i], updatedopenamt.ToString());
                                    }
                                }
                            }
                            else
                                break;
                            if (myPayment_due_date < paymentsdonedate)
                                overdue = updatedopenamt.ToString();
                            else
                                due = updatedopenamt.ToString();
                        }
                    }
                    int counts = PaymentsService.Updatecustomerdue(user.DbName, payments.Customer_comapnyId, due, overdue);
                    if (counts > 0)
                        return Json("success");
                }
            }

            return Json("unique");
        }
    }
}