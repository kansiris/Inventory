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

       // AvailablePosforDeliv

             public static SqlDataReader AvailablePosforDeliv(string dbname, string cid)
        {
            return InvoiceRepository.AvailablePosforDeliv(dbname, cid);
        }
        //AvailableInvoices
        public static SqlDataReader AvailableInvoices(string dbname, string cid)
        {
            return InvoiceRepository.AvailableInvoices(dbname, cid);
        }
        public static SqlDataReader GetPodata(string dbname, string cid,string Prchaseorder_no)
        {
            return InvoiceRepository.GetPodata(dbname, cid, Prchaseorder_no);
        }
        
            public static SqlDataReader checkinvoicenum(string dbname,string Invoice_no)
        {
            return InvoiceRepository.checkinvoicenum(dbname, Invoice_no);
        }

        public static int InsertInvoice(string dbname, string Invoice_no, string vendor_name, string customer_id, string company_name, string created_date, string payment_date, string grand_total, string payment_terms, string comment, string sub_total, string vat, string discount, string Prchaseorder_nos,string status, string product_id, string product_name, string cost_price,string description, string po_quantity, string total_price, string cgst_rate, string cgst_amount, string sgst_rate, string sgst_amount, string igst_rate, string igst_amount)
        {

            return InvoiceRepository.InsertInvoice(dbname, Invoice_no, vendor_name, customer_id, company_name, created_date, payment_date, grand_total, payment_terms, comment, sub_total, vat, discount, Prchaseorder_nos,status, product_id, product_name, cost_price, description, po_quantity, total_price, cgst_rate, cgst_amount, sgst_rate, sgst_amount, igst_rate, igst_amount);
        }

               //update po
        public static int UpdatePoforInvoice(string dbname,string customer_id,string Prchaseorder_nos,string invoice_status)
        {

            return InvoiceRepository.UpdatePoforInvoice(dbname,customer_id,Prchaseorder_nos, invoice_status);
        }
        public static int InsertDeliverynote(string dbname, string Delivernote_no, string vendor_name, string customer_id, string created_date,string comment, string sub_total,string Prchaseorder_nos,string product_id, string product_name, string description, string po_quantity, string deliver_quantity, string cost_price, string total_price)
        {

            return InvoiceRepository.InsertDeliverynote(dbname, Delivernote_no, vendor_name, customer_id,created_date,comment, sub_total,Prchaseorder_nos,product_id, product_name, description, po_quantity, deliver_quantity, cost_price, total_price);
        }

        
            public static SqlDataReader checkdeliverynotenum(string dbname, string Delivernote_no)
        {
            return InvoiceRepository.checkdeliverynotenum(dbname, Delivernote_no);
        }

        public static int UpdatePoforDeliveryNote(string dbname, string customer_id, string Prchaseorder_nos, string deliverynote_status)
        {

            return InvoiceRepository.UpdatePoforDeliveryNote(dbname, customer_id, Prchaseorder_nos, deliverynote_status);
        }


        public static int UpdatePoinCustomer(string dbname, string customer_id, string total_pos)
        {

            return InvoiceRepository.UpdatePoinCustomer(dbname, customer_id, total_pos);
        }


        public static int updateduesinCustomer_company(string dbname, string customer_id, string dues,string payment_date)
        {

            return InvoiceRepository.updateduesinCustomer_company(dbname, customer_id, dues, payment_date);
        }
        public static int UpdatenewPoinCustomer(string dbname, string customer_id, string new_pos)
        {

            return InvoiceRepository.UpdatenewPoinCustomer(dbname, customer_id, new_pos);
        }

        public static SqlDataReader Getproductdetails(string dbname, string cid, string Prchaseorder_no)
        {
            return InvoiceRepository.Getproductdetails(dbname, cid, Prchaseorder_no);
        }

        public static SqlDataReader Getposforcustomer(string dbname, string cid, string invoice_status)
        {
            return InvoiceRepository.Getposforcustomer(dbname, cid, invoice_status);
        }

        //to update due after invoice created

        
            public static SqlDataReader Getduesforcustomer(string dbname, string cid)
        {
            return InvoiceRepository.Getduesforcustomer(dbname, cid);
        }

        public static SqlDataReader Getinvoicedata(string dbname, string cid, string invoiceno)//, string Prchaseorder_no
        {
            return InvoiceRepository.Getinvoicedata(dbname, cid,invoiceno);
        }

    }
}
