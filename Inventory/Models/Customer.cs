using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Inventory.Models
{
    public class Customer
    {
        public int cus_company_Id { get; set; }
        
        public string cus_company_name { get; set; }

        public string cus_email { get; set; }

        public string cus_Note { get; set; }


        public string cus_logo { get; set; }
    }
}