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
                                                       //Prchaseorder_no = row["Prchaseorder_no"].ToString(),
                                                       Invoice_no = row["Invoice_no"].ToString(),
                                                       total_price = row["total_price"].ToString(),
                                                       created_date = row["created_date"].ToString(),
                                                       Payment_date = row["payment_date"].ToString(),
                                                       invoice_status = row["invoice_status"].ToString(),
                                                       totalQty = row["totalQty"].ToString(),
                                                       open_amount = row["open_amount"].ToString()
                                                   }).ToList();
                ViewBag.records = availableinvoices;
                ViewBag.invoice_status = availableinvoices.Select(m => m.invoice_status);
                ViewBag.total_price = availableinvoices.Select(m => float.Parse(m.open_amount)).Sum();
                ViewBag.customer_id = cid;
                ViewBag.company_name = cname;
            }
            if (TempData["msg"] != null)
            {
                ViewBag.msg = TempData["msg"];
            }
            if (TempData["smsg"] != null)
            {
                ViewBag.smsg = TempData["smsg"];
            }

            if (TempData["enablebuttons"] != null)
            {
                ViewBag.enablebuttons = TempData["enablebuttons"];
            }
            if (TempData["invocess"] != null)
            {
                ViewBag.invocess = TempData["invocess"];
            }


            return View();
        }
        [HttpPost]
        public ActionResult Index(Payments payments)
        {

            if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
            {

                var user = (CustomPrinciple)System.Web.HttpContext.Current.User;
                var count = PaymentsService.InsertPayments(user.DbName, payments.payments_date, payments.cheque_date, payments.cheque_bankname, payments.cheque_num, payments.creditORdebitcard_date, payments.card_holder_name, payments.card_last4digits, payments.bank_taransfer_date, payments.bank_transfer_name, payments.bank_transaction_id
        , payments.cash_date, payments.cash_card_holdername, payments.wallet_date, payments.wallet_number, payments.invoiced_amount, payments.Received_amount, payments.opening_balance, payments.current_balance, payments.bank_transfer_IFSCcode, payments.bank_transfer_branchname, payments.Customer_comapnyId, payments.Customer_company_name, payments.remarks);
                List<Invoice> listglb = new List<Invoice>();
                string overdue = "0";
                string due = "0";
                if (count > 0)
                {

                    //getdueoverdue
                    var dts = new DataTable();
                    var recordss = PaymentsService.Getdueoverdue(user.DbName, payments.Customer_comapnyId);
                    dts.Load(recordss);
                    List<Customer> duesoverdues = (from DataRow row in dts.Rows
                                                   select new Customer()
                                                   {
                                                       due = row["due"].ToString(),
                                                       overdue = row["overdue"].ToString()
                                                   }).ToList();
                    if (duesoverdues.Count > 0)
                    {
                        due = duesoverdues.FirstOrDefault().due;
                        overdue = duesoverdues.FirstOrDefault().overdue;
                    }
                    string overduestrt = overdue;
                    string duestrt = due;
                    int updatedopenamt = 0; int updatedreceivedamount = int.Parse(payments.Received_amount); int updatedinvoiceamount = int.Parse(payments.invoiced_amount);
                    Array ponumsArray = payments.poid.Split(',');
                    for (int i = 0; i < ponumsArray.Length; i++)
                    {
                        if (int.Parse(payments.Received_amount) > 0)
                        {
                            var dt = new DataTable();
                            var records = PaymentsService.ForPaymentinvoicetotal(user.DbName, payments.poid.Split(',')[i]);
                            dt.Load(records);
                            List<Invoice> invoicetotl = (from DataRow row in dt.Rows
                                                         select new Invoice()
                                                         {
                                                             Payment_date = row["payment_date"].ToString(),
                                                             sub_total = row["sub_total"].ToString(),
                                                             open_amount = row["open_amount"].ToString(),
                                                             totalQty = row["po_quantity"].ToString()
                                                         }).OrderByDescending(m => m.Payment_date).ToList();
                            string Payment_due_date = (invoicetotl.Select(m => m.Payment_date)).First();
                            string open_amount = invoicetotl.FirstOrDefault().open_amount;
                            string sub_total = invoicetotl.FirstOrDefault().sub_total;
                            string totalQty = (invoicetotl.Select(m => int.Parse(m.totalQty)).Sum()).ToString();
                            
                            //For Local
                            //string[] strDate = Payment_due_date.Split('/');
                            //DateTime date1 = Convert.ToDateTime(strDate[0] + "/" + strDate[1] + "/" + strDate[2]);
                            //string[] enddate = payments.payments_date.Split('/');
                            //DateTime date2 = Convert.ToDateTime(enddate[0] + "/" + enddate[1] + "/" + enddate[2]);

                            //*****for Live deploy
                            string[] strDate = Payment_due_date.Split('/');
                            DateTime date1 = Convert.ToDateTime(strDate[0] + "/" + strDate[1] + "/" + strDate[2]);
                            string[] enddate = payments.payments_date.Split('/');
                            DateTime date2 = Convert.ToDateTime(enddate[0] + "/" + enddate[1] + "/" + enddate[2]);


                            if (open_amount != "" && open_amount != null && open_amount != "0")
                            {
                                if ((int.Parse(open_amount)) >= (int.Parse(payments.Received_amount)))
                                {
                                    updatedopenamt = (int.Parse(open_amount) - int.Parse(payments.Received_amount));
                                    PaymentsService.Updateinvoice(user.DbName, payments.poid.Split(',')[i], updatedopenamt.ToString());  /*updatedopenamt.ToString()*/
                                    listglb.Add(new Invoice() {Prchaseorder_no= payments.poid, Invoice_no = payments.poid.Split(',')[i], Payment_date = Payment_due_date, open_amount = open_amount, sub_total = payments.Received_amount, totalQty = totalQty, total_dues= updatedopenamt.ToString() });
                                    duestrt = (int.Parse(duestrt) - int.Parse(payments.Received_amount)).ToString();
                                    overduestrt = (int.Parse(overduestrt) - int.Parse(payments.Received_amount)).ToString();
                                    payments.Received_amount = "0";
                                }
                                else
                                {
                                    updatedreceivedamount = (int.Parse(payments.Received_amount) - int.Parse(open_amount));
                                    if (updatedreceivedamount > 0)
                                    {
                                        duestrt = (int.Parse(duestrt) - int.Parse(payments.Received_amount)).ToString();
                                        overduestrt = (int.Parse(overduestrt) - int.Parse(payments.Received_amount)).ToString();
                                        payments.Received_amount = updatedreceivedamount.ToString();
                                        updatedopenamt = 0;
                                        listglb.Add(new Invoice() { Prchaseorder_no = payments.poid, Invoice_no = payments.poid.Split(',')[i], Payment_date = Payment_due_date, open_amount = open_amount, sub_total = payments.Received_amount, totalQty = totalQty, total_dues = updatedopenamt.ToString() });
                                        PaymentsService.Updateinvoice(user.DbName, payments.poid.Split(',')[i], updatedopenamt.ToString());
                                    }
                                }
                            }
                            else
                                break;
                            if (date1 < date2)
                            {
                                if (int.Parse(overduestrt)> updatedopenamt) {
                                    overdue = overduestrt;//(int.Parse(overduestrt) - updatedreceivedamount).ToString();
                                }
                                else
                                {
                                overdue = updatedopenamt.ToString();
                                }

                            }
                            else
                            {
                                if (int.Parse(duestrt) > updatedopenamt)
                                {
                                 due = duestrt;
                                 }
                                else { 
                                due = updatedopenamt.ToString(); 
                                }
                            }
                        }
                    }
                    int counts = PaymentsService.Updatecustomerdue(user.DbName, payments.Customer_comapnyId, due, overdue);
                    counts = 1;
                    if (counts > 0)
                    {
                        TempData["smsg"] = "Payment saved Successfully!!! Do You Want to Send Email ? ";
                        TempData["enablebuttons"] = "emailbutton";
                        TempData["invocess"] = listglb;
                        return RedirectToAction("Index", "Payments", new { cid = payments.Customer_comapnyId, cname = payments.Customer_company_name });

                    }
                }
            }
            TempData["msg"] = "Failed To Save";

            return RedirectToAction("Index", "Customer");

        }


        //for email of invoice

        public JsonResult Email(string Invoicedata, string EmailID,string Invoicenums)
        {
            // Designing Email Part
            SendEmail abc = new SendEmail();
            string activationCode = Guid.NewGuid().ToString();
            string url = Request.Url.Scheme + "://" + Request.Url.Authority + "/Login/ActivateEmail?ActivationCode=" + activationCode + "&&Email=" + EmailID;
            FileInfo File = new FileInfo(Server.MapPath("/Content/payments.html"));//need to chnge mailer.html
            //string message = Invoicedata;//readFile + body;
            string readFile = File.OpenText().ReadToEnd();
            readFile = readFile.Replace("InvoiceData", Invoicedata);
            readFile = readFile.Replace("[Invoice numbers]", Invoicenums);
            //readFile = readFile.Replace("InvoiceData", Invoicedata);
            string message = readFile;
            abc.EmailAvtivation(EmailID, message, "Payment Details");
            return Json("success");
        }


    }
}