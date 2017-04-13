using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Inventory.Models
{
    public class ProductItems
    {
        public string weight_id { get; set; }
        public string weight { get; set; }
        public string size_id { get; set; }
        public string size { get; set; }
        public string color_id { get; set; }
        public string color { get; set; }
        public string itemshape_id { get; set; }
        public string itemshape { get; set; }
        public string category_id { get; set; }
        public string category { get; set; }
        public string subcategory_id { get; set; }
        public string subcategory { get; set; }
    }
}