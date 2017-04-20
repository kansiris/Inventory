using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Inventory.Models
{
    public class Product
    {
        public string ID { get; set; }
        public string product_id { get; set; }
        public string product_name { get; set; }
        public string brand { get; set; }
        public string model { get; set; }
        public string category { get; set; }
        public string sub_category { get; set; }
        public string cost_price { get; set; }
        public string selling_price { get; set; }
        public string tax { get; set; }
        public string discount { get; set; }
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
    }
}