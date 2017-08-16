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
    public class PaymentsRepository
    {
        private static string ConnectionString;
        //AvailableInvoices
        public static SqlDataReader InvoicesForPayment(string dbname, string cid)
        {
            GetConnectionString getConnectionString = new GetConnectionString();
            ConnectionString = getConnectionString.CustomizeConnectionString(dbname);
            return SqlHelper.ExecuteReader(ConnectionString, "availableInvoicedPos", cid);
        }

        public static int InsertPayments(string dbname, string payments_date, string cheque_date, string cheque_bankname, string cheque_num, string creditORdebitcard_date, string card_holder_name, string card_last4digits, string bank_taransfer_date,
      string bank_transfer_name, string bank_transaction_id, string cash_date, string cash_card_holdername, string wallet_date, string wallet_number, string invoiced_amount, string Received_amount, string opening_balance,
      string current_balance, string bank_transfer_IFSCcode, string bank_transfer_branchname, string Customer_comapnyId, string Customer_company_name, string remarks)
        {

            GetConnectionString getConnectionString = new GetConnectionString();
            ConnectionString = getConnectionString.CustomizeConnectionString(dbname);
            return SqlHelper.ExecuteNonQuery(ConnectionString, "insertPayments", payments_date, cheque_date, cheque_bankname, cheque_num, creditORdebitcard_date, card_holder_name, card_last4digits, bank_taransfer_date, bank_transfer_name, bank_transaction_id
           , cash_date, cash_card_holdername, wallet_date, wallet_number, invoiced_amount, Received_amount, opening_balance, current_balance, bank_transfer_IFSCcode, bank_transfer_branchname, Customer_comapnyId, Customer_company_name, remarks);
        }
        //Updatecustomerdue
             public static int Updatecustomerdue(string dbname, string customer_id, string due, string overdue, string Payment_due_date)
        {
            GetConnectionString getConnectionString = new GetConnectionString();
            ConnectionString = getConnectionString.CustomizeConnectionString(dbname);
            return SqlHelper.ExecuteNonQuery(ConnectionString, "updatecustomerdues", customer_id, due, overdue,Payment_due_date);
        }

        //updateopenamunt_invoice
             public static int Updateinvoice(string dbname, string Prchaseorder_no,string open_amount)
        {
            GetConnectionString getConnectionString = new GetConnectionString();
            ConnectionString = getConnectionString.CustomizeConnectionString(dbname);
            return SqlHelper.ExecuteNonQuery(ConnectionString, "updateopenamunt_invoice", Prchaseorder_no, open_amount);
        }

        //to check customerid in payments

        public static SqlDataReader checkcustomerinPayments(string dbname, string customer_id)
        {
            GetConnectionString getConnectionString = new GetConnectionString();
            ConnectionString = getConnectionString.CustomizeConnectionString(dbname);
            return SqlHelper.ExecuteReader(ConnectionString, "checkforcustomerinPayments", customer_id);
        }

        //forpayments podetails
        public static SqlDataReader ForPaymentinvoicetotal(string dbname, string Invoice_no)
        {
            GetConnectionString getConnectionString = new GetConnectionString();
            ConnectionString = getConnectionString.CustomizeConnectionString(dbname);
            return SqlHelper.ExecuteReader(ConnectionString, "forpaymentponum", Invoice_no);
        }

        //to get dueoverdues
        public static SqlDataReader Getdueoverdue(string dbname, string cus_company_id)
        {
            GetConnectionString getConnectionString = new GetConnectionString();
            ConnectionString = getConnectionString.CustomizeConnectionString(dbname);
            return SqlHelper.ExecuteReader(ConnectionString, "getdueoverdue", cus_company_id);
        }
    }
}
