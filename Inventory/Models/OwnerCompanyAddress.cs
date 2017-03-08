using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Inventory.Models
{
    public class OwnerCompanyAddress
    {
        public char addr_id { get; set; }
        public int user_id { get; set; }
        public string Line1 { get; set; }
        public string Line2 { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string postalcode { get; set; }
        public string country { get; set; }
    }
}