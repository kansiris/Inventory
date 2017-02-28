using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Data.SqlClient;

namespace Inventory.Models
{
    public partial class Warehouse
    {
       
        [StringLength(50)]
       
        public string wh_name { get; set; }
       
        [StringLength(50)]
        public string wh_Shortname { get; set; }

        public string Job_position { get; set; }

        public int phone { get; set; }
        public int Mobile { get; set; }
        public string Email { get; set; }
        public string conperson { get; set; }
        public string Note { get; set; }
        public string Billing_Address { get; set; }
        public string shipping_Address { get; set; }
        public string other_Address { get; set; }

    }
}