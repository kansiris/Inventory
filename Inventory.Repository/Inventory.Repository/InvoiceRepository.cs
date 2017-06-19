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
            return SqlHelper.ExecuteReader(ConnectionString, "availablePos", cid);
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

        public static int InsertInvoice(string dbname, string Invoice_no, string vendor_name, string customer_id, string company_name, string created_date, string payment_date, string grand_total, string payment_terms, string comment, string sub_total, string vat, string discount, string Prchaseorder_nos,string status)
        {

            GetConnectionString getConnectionString = new GetConnectionString();
            ConnectionString = getConnectionString.CustomizeConnectionString(dbname);
            return SqlHelper.ExecuteNonQuery(ConnectionString, "sptoinsertinvoice", Invoice_no, vendor_name, customer_id, company_name, created_date, payment_date, grand_total, payment_terms, comment, sub_total, vat, discount, Prchaseorder_nos,status);
        }
        public static int UpdatePoforInvoice(string dbname, string customer_id, string Prchaseorder_nos,string invoice_status)
        {
            GetConnectionString getConnectionString = new GetConnectionString();
            ConnectionString = getConnectionString.CustomizeConnectionString(dbname);
            return SqlHelper.ExecuteNonQuery(ConnectionString, "sptoupdateinvoicestatus",customer_id,Prchaseorder_nos, invoice_status);
        }

        public static int InsertDeliverynote(string dbname, string Delivernote_no, string vendor_name, string customer_id,string created_date, string grand_total, string comment, string sub_total, string vat, string discount, string Prchaseorder_nos)
        {

            GetConnectionString getConnectionString = new GetConnectionString();
            ConnectionString = getConnectionString.CustomizeConnectionString(dbname);
            return SqlHelper.ExecuteNonQuery(ConnectionString, "sptoinsertdeliverynote", Delivernote_no, vendor_name, customer_id,created_date, grand_total, comment, sub_total, vat, discount, Prchaseorder_nos);
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
    }
}