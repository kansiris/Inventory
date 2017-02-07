using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Inventory.Models
{
    public partial class Login
    {
        [StringLength(50)]
        public string User_Type { get; set; }
        [StringLength(50)]
        public string Description { get; set; }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string User_Id { get; set; }
        [StringLength(100)]
        public string Remarks { get; set; }
        [StringLength(50)]
        public string User_FName { get; set; }
        [StringLength(50)]
        public string User_LName { get; set; }
        [StringLength(200)]
        public string Email_ID { get; set; }
        [StringLength(50)]
        public string Password { get; set; }
        [StringLength(50)]
        public string Company_ID { get; set; }
    }
}