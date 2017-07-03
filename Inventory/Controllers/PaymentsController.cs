using Inventory.Content;
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
                var dt = new DataTable();
                var records = PaymentsService.InvoicesForPayment(user.DbName, cid);
                dt.Load(records);
                List<Invoice> availableinvoices = (from DataRow row in dt.Rows
                                                   select new Invoice()
                                                   {
                                                       customer_id = row["customer_id"].ToString(),
                                                       company_name = row["company_name"].ToString(),
                                                       Prchaseorder_no = row["Prchaseorder_no"].ToString(),
                                                       total_price = row["total_price"].ToString(),
                                                       created_date = row["created_date"].ToString(),
                                                       invoice_status = row["invoice_status"].ToString(),
                                                       totalQty = row["totalQty"].ToString()
                                                   }).OrderByDescending(m => m.invoice_status).ToList();
                ViewBag.records = availableinvoices;
                ViewBag.invoice_status = availableinvoices.Select(m => m.invoice_status);
                ViewBag.total_price = availableinvoices.Select(m => float.Parse(m.total_price)).Sum();
                ViewBag.customer_id = cid;
                ViewBag.company_name = cname;
            }
            return View();
        }

        //inserting payments
        public JsonResult InsertPayments(string payments_date, string cheque_date, string cheque_bankname, string cheque_num, string creditORdebitcard_date, string card_holder_name, string card_last4digits, string bank_taransfer_date,
           string bank_transfer_name, string bank_transaction_id, string cash_date, string cash_card_holdername, string wallet_date, string wallet_number, string invoiced_amount, string Received_amount, string opening_balance,
           string current_balance, string bank_transfer_IFSCcode, string bank_transfer_branchname, string Customer_comapnyId, string Customer_company_name, string remarks)
        {

            if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
            {
                var user = (CustomPrinciple)System.Web.HttpContext.Current.User;
                var count = PaymentsService.InsertPayments(user.DbName, payments_date, cheque_date, cheque_bankname, cheque_num, creditORdebitcard_date, card_holder_name, card_last4digits, bank_taransfer_date, bank_transfer_name, bank_transaction_id
           , cash_date, cash_card_holdername, wallet_date, wallet_number, invoiced_amount, Received_amount, opening_balance, current_balance, bank_transfer_IFSCcode, bank_transfer_branchname, Customer_comapnyId, Customer_company_name, remarks);
                if (count > 0)
                {
                    string due = current_balance;
                    string overdue = 0.ToString();
                    int counts = PaymentsService.Updatecustomerdue(user.DbName, Customer_comapnyId, due, overdue);
                    if(counts>0)
                    return Json("success");
                }

            }
            return Json("unique");
        }
    }
}