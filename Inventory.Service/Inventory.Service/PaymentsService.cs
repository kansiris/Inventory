using Inventory.Repository;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Service
{
    public class PaymentsService
    {
        //AvailableInvoices
        public static SqlDataReader InvoicesForPayment(string dbname, string cid)
        {
            return PaymentsRepository.InvoicesForPayment(dbname, cid);
        }
        //insert payments

        public static int InsertPayments(string dbname, string payments_date, string cheque_date, string cheque_bankname, string cheque_num, string creditORdebitcard_date, string card_holder_name, string card_last4digits, string bank_taransfer_date,
           string bank_transfer_name, string bank_transaction_id, string cash_date, string cash_card_holdername, string wallet_date, string wallet_number, string invoiced_amount, string Received_amount, string opening_balance,
           string current_balance, string bank_transfer_IFSCcode, string bank_transfer_branchname, string Customer_comapnyId, string Customer_company_name,string remarks)
        {

            return PaymentsRepository.InsertPayments(dbname, payments_date, cheque_date, cheque_bankname, cheque_num, creditORdebitcard_date, card_holder_name, card_last4digits, bank_taransfer_date, bank_transfer_name, bank_transaction_id
           , cash_date, cash_card_holdername, wallet_date, wallet_number, invoiced_amount, Received_amount, opening_balance, current_balance, bank_transfer_IFSCcode, bank_transfer_branchname, Customer_comapnyId, Customer_company_name, remarks);
        }

        //update due in customer
        public static int Updatecustomerdue(string dbname, string customer_id, string due, string overdue,string  Payment_due_date)
        {
            return PaymentsRepository.Updatecustomerdue(dbname, customer_id, due, overdue,Payment_due_date);
        }
        //Updateinvoice
             public static int Updateinvoice(string dbname, string Prchaseorder_no, string open_amount)
        {
            return PaymentsRepository.Updateinvoice(dbname, Prchaseorder_no, open_amount);
        }
        public static SqlDataReader checkcustomerinPayments(string dbname, string customer_id)
        {
            return PaymentsRepository.checkcustomerinPayments(dbname, customer_id);
        }

        //ForPaymentinvoicetotal
             public static SqlDataReader ForPaymentinvoicetotal(string dbname, string Invoice_no)
        {
            return PaymentsRepository.ForPaymentinvoicetotal(dbname, Invoice_no);
        }

        //Getdueoverdue(string dbname, string cus_company_id)
        public static SqlDataReader Getdueoverdue(string dbname, string cus_company_id)
        {
            return PaymentsRepository.Getdueoverdue(dbname, cus_company_id);
        }
    }
}
