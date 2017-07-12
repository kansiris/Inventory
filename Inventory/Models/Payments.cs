namespace Inventory.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Payments")]
    public class Payments
    {
        public string poid { get; set; }
        public string payments_date { get; set; }
        public string cheque_date { get; set; }
        public string cheque_bankname { get; set; }
        public string cheque_num { get; set; }

        public string creditORdebitcard_date { get; set; }
        public string card_holder_name { get; set; }
        public string card_last4digits { get; set; }
        public string bank_taransfer_date { get; set; }
        public string bank_transfer_name { get; set; }
        public string bank_transaction_id { get; set; }
        public string cash_date { get; set; }
        public string cash_card_holdername { get; set; }
        public string wallet_date { get; set; }
        public string wallet_number { get; set; }
        public string invoiced_amount { get; set; }
        public string Received_amount { get; set; }
        public string opening_balance { get; set; }
        public string current_balance { get; set; }
        public string bank_transfer_IFSCcode { get; set; }
        public string bank_transfer_branchname { get; set; }
        public string Customer_comapnyId { get; set; }
        public string remarks { get; set; }
  
        public string Customer_company_name { get; set; }


    }
}
