using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using System.Data;
using Inventory.Utility;

namespace Inventory.Repository
{
    public class InvoiceRepository
    {
        private static string ConnectionString;
        public static SqlDataReader AvailablePos(string dbname, string cid)
        {
            GetConnectionString getConnectionString = new GetConnectionString();
            ConnectionString = getConnectionString.CustomizeConnectionString(dbname);
            return SqlHelper.ExecuteReader(ConnectionString, "availablePosforInvoice", cid);
        }

        public static SqlDataReader AvailablePosforDeliv(string dbname, string cid)
        {
            GetConnectionString getConnectionString = new GetConnectionString();
            ConnectionString = getConnectionString.CustomizeConnectionString(dbname);
            return SqlHelper.ExecuteReader(ConnectionString, "availablePosforDeliv", cid);
        }
        //AvailableInvoices
        public static SqlDataReader AvailableInvoices(string dbname, string cid)
        {
            GetConnectionString getConnectionString = new GetConnectionString();
            ConnectionString = getConnectionString.CustomizeConnectionString(dbname);
            return SqlHelper.ExecuteReader(ConnectionString, "availableInvoices", cid);
        }
        public static SqlDataReader GetPodata(string dbname, string cid,string Prchaseorder_no)
        {
            GetConnectionString getConnectionString = new GetConnectionString();
            ConnectionString = getConnectionString.CustomizeConnectionString(dbname);
            return SqlHelper.ExecuteReader(ConnectionString, "getpodatabasedonponum", cid, Prchaseorder_no);
        }

        
             public static SqlDataReader checkinvoicenum(string dbname,string Invoice_no)
        {
            GetConnectionString getConnectionString = new GetConnectionString();
            ConnectionString = getConnectionString.CustomizeConnectionString(dbname);
            return SqlHelper.ExecuteReader(ConnectionString, "checkInvoicenum",Invoice_no);
        }

        public static int InsertInvoice(string dbname, string Invoice_no, string vendor_name, string customer_id, string company_name, string created_date, string payment_date, string grand_total, string payment_terms, string comment, string sub_total, string vat, string discount, string Prchaseorder_nos,string status, string product_id, string product_name, string cost_price, string po_quantity, string total_price, string cgst_rate, string cgst_amount, string sgst_rate, string sgst_amount, string igst_rate, string igst_amount)
        {

            GetConnectionString getConnectionString = new GetConnectionString();
            ConnectionString = getConnectionString.CustomizeConnectionString(dbname);
            return SqlHelper.ExecuteNonQuery(ConnectionString, "sptoinsertinvoice", Invoice_no, vendor_name, customer_id, company_name, created_date, payment_date, grand_total, payment_terms, comment, sub_total, vat, discount, Prchaseorder_nos,status, product_id, product_name, cost_price, po_quantity, total_price, cgst_rate, cgst_amount, sgst_rate, sgst_amount, igst_rate, igst_amount);
        }
        public static int UpdatePoforInvoice(string dbname, string customer_id, string Prchaseorder_nos,string invoice_status)
        {
            GetConnectionString getConnectionString = new GetConnectionString();
            ConnectionString = getConnectionString.CustomizeConnectionString(dbname);
            return SqlHelper.ExecuteNonQuery(ConnectionString, "sptoupdateinvoicestatus",customer_id,Prchaseorder_nos, invoice_status);
        }

        public static int InsertDeliverynote(string dbname, string Delivernote_no, string vendor_name, string customer_id,string created_date,string comment, string sub_total,string Prchaseorder_nos, string product_id, string product_name, string description, string po_quantity, string deliver_quantity, string cost_price, string total_price)
        {

            GetConnectionString getConnectionString = new GetConnectionString();
            ConnectionString = getConnectionString.CustomizeConnectionString(dbname);
            return SqlHelper.ExecuteNonQuery(ConnectionString, "sptoinsertdeliverynote", Delivernote_no, vendor_name, customer_id,created_date,comment, sub_total, Prchaseorder_nos, product_id, product_name, description, po_quantity, deliver_quantity, cost_price, total_price);
        }

        public static SqlDataReader checkdeliverynotenum(string dbname, string Delivernote_no)
        {
            GetConnectionString getConnectionString = new GetConnectionString();
            ConnectionString = getConnectionString.CustomizeConnectionString(dbname);
            return SqlHelper.ExecuteReader(ConnectionString, "checkDeliverynotenum", Delivernote_no);
        }

        

            public static int UpdatePoforDeliveryNote(string dbname, string customer_id, string Prchaseorder_nos, string deliverynote_status)
        {
            GetConnectionString getConnectionString = new GetConnectionString();
            ConnectionString = getConnectionString.CustomizeConnectionString(dbname);
            return SqlHelper.ExecuteNonQuery(ConnectionString, "sptoupdatedelivstatus", customer_id, Prchaseorder_nos, deliverynote_status);
        }

        public static int UpdatePoinCustomer(string dbname, string customer_id, string total_pos)
        {
            GetConnectionString getConnectionString = new GetConnectionString();
            ConnectionString = getConnectionString.CustomizeConnectionString(dbname);
            return SqlHelper.ExecuteNonQuery(ConnectionString, "updatePOsinCustomer_company", customer_id, total_pos);
        }
        
             public static int UpdatenewPoinCustomer(string dbname, string customer_id, string new_pos)
        {
            GetConnectionString getConnectionString = new GetConnectionString();
            ConnectionString = getConnectionString.CustomizeConnectionString(dbname);
            return SqlHelper.ExecuteNonQuery(ConnectionString, "updatenewPOsinCustomer_company", customer_id, new_pos);
        }
        public static SqlDataReader Getproductdetails(string dbname, string cid, string Prchaseorder_no)
        {
            GetConnectionString getConnectionString = new GetConnectionString();
            ConnectionString = getConnectionString.CustomizeConnectionString(dbname);
            return SqlHelper.ExecuteReader(ConnectionString, "getproductdetails", cid, Prchaseorder_no);
        }
        
            public static SqlDataReader Getposforcustomer(string dbname, string cid, string invoice_status)
        {
            GetConnectionString getConnectionString = new GetConnectionString();
            ConnectionString = getConnectionString.CustomizeConnectionString(dbname);
            return SqlHelper.ExecuteReader(ConnectionString, "togetallpos", cid, invoice_status);
        }

        
               public static SqlDataReader Getinvoicedata(string dbname, string cid, string Prchaseorder_no)
        {
            GetConnectionString getConnectionString = new GetConnectionString();
            ConnectionString = getConnectionString.CustomizeConnectionString(dbname);
            return SqlHelper.ExecuteReader(ConnectionString, "getinvoicedata", cid, Prchaseorder_no);
        }
    }
}