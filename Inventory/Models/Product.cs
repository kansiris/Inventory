﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Inventory.Models
{
    public class Product
    {
        public string ID { get; set; }
        public string customer_id { get; set; }
        public string company_name { get; set; }
        public string product_id { get; set; }
        public string product_name { get; set; }
        public string batch_number { get; set; }
        public string brand { get; set; }
        public string model { get; set; }
        public string category_id { get; set; }
        public string category { get; set; }
        public string subcategory_id { get; set; }
        public string sub_category { get; set; }
        public string cost_price { get; set; }
        public string selling_price { get; set; }
        public string tax { get; set; }
        public string discount { get; set; }
        public string description { get; set; }
        public string BATCHNOLIST { get; set; }
        public string CGST { get; set; }
        public string SGST { get; set; }
        public string IGST { get; set; }
        public string HSNcode { get; set; }

        public string shipping_price { get; set; }
        public string total_price { get; set; }
        public string Measurement { get; set; }
        public string weight { get; set; }
        public string size { get; set; }
        public string color { get; set; }
        public string item_shape { get; set; }
        public string product_consumable { get; set; }
        public string product_type { get; set; }
        public string product_perishability { get; set; }
        public string product_expirydate { get; set; }
        public string product_description { get; set; }
        public string product_tags { get; set; }
        public string created_date { get; set; }
        public string product_images { get; set; }
        public string distinctproducts { get; set; }
        public string Quantity { get; set; }
        public int cart_id { get; set; }

        
        public string payment_terms { get; set; }
        public string shipping_terms { get; set; }
        public string remarks { get; set; }
        public string sub_total { get; set; }
        public string vat { get; set; }
        public string grand_total { get; set; }
        public string shipping_date { get; set; }
        public string deliverynote_status { get; set; }
        public string invoice_status { get; set; }

        public string status { get; set; }

        //Quantity In Hand Related Fields
        public int Quantity_id { get; set; }
        public string Quantity_area { get; set; }
        public string Qty_Stock { get; set; }
        public List<string> Quantity_Qty { get; set; }
        public List<string> Reorder_level { get; set; }
        public List<string> Quantity_QtyStock { get; set; }
        public List<int> Qid { get; set; }
        public string Quantity_Total { get; set; }
        public string Qty_Reorder { get; set; }
        public string Prchaseorder_no { get; set; }
        public string Payment_date { get; set; }

    }
}