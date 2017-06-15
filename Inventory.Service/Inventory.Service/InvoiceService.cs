using Inventory.Repository;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Service
{
    public class InvoiceService
    {
        public static SqlDataReader AvailablePos(string dbname, string cid)
        {
            return InvoiceRepository.AvailablePos(dbname, cid);
        }

        public static SqlDataReader GetPodata(string dbname, string cid,string Prchaseorder_no)
        {
            return InvoiceRepository.GetPodata(dbname, cid, Prchaseorder_no);
        }
        
            public static SqlDataReader checkinvoicenum(string dbname,string Invoice_no)
        {
            return InvoiceRepository.checkinvoicenum(dbname, Invoice_no);
        }

        public static int InsertInvoice(string dbname, string Invoice_no, string vendor_name, string customer_id, string company_name, string created_date, string payment_date, string grand_total, string payment_terms, string comment, string sub_total, string vat, string discount, string Prchaseorder_nos)
        {

            return InvoiceRepository.InsertInvoice(dbname, Invoice_no, vendor_name, customer_id, company_name, created_date, payment_date, grand_total, payment_terms, comment, sub_total, vat, discount, Prchaseorder_nos);
        }

        //update po
        public static int UpdatePoforInvoice(string dbname,string customer_id,string Prchaseorder_nos,string invoice_status)
        {

            return InvoiceRepository.UpdatePoforInvoice(dbname,customer_id,Prchaseorder_nos, invoice_status);
        }

    }
}
