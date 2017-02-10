using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Inventory.Models
{
    public class UserMaster
    {
        [Key]
        [StringLength(10)]
        public string ID { get; set; }

        [StringLength(10)]
        public string EmailId { get; set; }

        [StringLength(50)]
        public string First_Name { get; set; }

        [StringLength(50)]
        public string Last_Name { get; set; }
        [StringLength(50)]
        public string DB_Name { get; set; }
        [Column(TypeName = "date")]
        public DateTime? Created_Date { get; set; }
        [StringLength(50)]
        public string Password { get; set; }
        public int SubscriptionId { get; set; }
        public int UserTypeId { get; set; }

        [StringLength(50)]
        public string User_Site { get; set; }

        [StringLength(50)]
        public string CompanyName { get; set; }

        public string Phone { get; set; }
        public DateTime? SubscriptionDate { get; set; }
        public int IsActive { get; set; }
        public string activationcode { get; set; }
    }
}