using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace Inventory.Models
{
    public partial class Warehouse
    {
       
        [StringLength(50)]
       
        public string wh_name { get; set; }
       
        [StringLength(50)]
        public string wh_Shortname { get; set; }

        public string Job_position { get; set; }
        //[RegularExpression("([^[0-9]*$)")]
        public long phone { get; set; }
        //[RegularExpression("([^[0-9]*$)")]
        public long Mobile { get; set; }
        //[Required(ErrorMessage = "Please Enter Email Address")]
        //[Display(Name = "Email")]
        [RegularExpression(@"^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$",
         ErrorMessage = "Please Enter Correct Email Address")]
        public string Email { get; set; }
        public string conperson { get; set; }
        public string Note { get; set; }
       public string bill_Street { get; set; }
        public string bill_City { get; set; }
        public string bill_State { get; set; }
        public string bill_Postalcode { get; set; }
        public string bill_Country { get; set; }
        public string ship_Street { get; set; }
        public string ship_City { get; set; }
        public string ship_State { get; set; }
        public string ship_Postalcode { get; set; }
        public string ship_Country { get; set; }
        
    }
}