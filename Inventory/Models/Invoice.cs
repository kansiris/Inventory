using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace Inventory.Models
{

    public class Invoice
    {

        public string total_pos { get; set; }
        public string Invoice_no { get; set; }
        public string new_pos { get; set; }
        public string ID { get; set; }
        public string customer_id { get; set; }
        public string company_name { get; set; }
        public string product_id { get; set; }
        public string product_name { get; set; }
        public string Quantity { get; set; }
        public string cost_price { get; set; }
        public string total_price { get; set; }
        public string selling_price { get; set; }
        public string tax { get; set; }
        public string discount { get; set; }
        public string description { get; set; }
        public string payment_terms { get; set; }
        public string shipping_terms { get; set; }
        public string remarks { get; set; }
        public string sub_total { get; set; }
        public string vat { get; set; }
        public string grand_total { get; set; }
        public string shipping_date { get; set; }
        public string created_date { get; set; }


        public string Prchaseorder_no { get; set; }
        public string Payment_date { get; set; }
        public string status { get; set; }
        public string totalQty { get; set; }
        public string open_amount { get; set; }


        public string invoice_status { get; set; }
        public string deliverynote_status { get; set; }

    }
}
