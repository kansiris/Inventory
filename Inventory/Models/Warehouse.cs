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
        [StringLength(10)]
        [Required]
        public string wh_id { get; set; }
        [StringLength(50)]
       
        [Display(Name = "Warehouse Name")]
        [Required(ErrorMessage = "Enter Warehouse Name")]
        public string wh_name { get; set; }
       
        [Display(Name = "Warehouse ShortName")]
        [Required(ErrorMessage = "Enter Warehouse Name")]
        [StringLength(50)]
        public string wh_Shortname { get; set; }
    }
}