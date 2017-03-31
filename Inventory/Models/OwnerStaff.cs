using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Inventory.Models
{
    public class OwnerStaff
    {
        public string Staff_Id { get; set; }
        public int Owner_id { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public long Mobile_No { get; set; }
        public string Email { get; set; }
        public int Status_ID { get; set; }
        public string Job_position { get; set; }
        public int Vendor_Access { get; set; }
        public int Customer_Access { get; set; }
        public string UserPic { get; set; }
    }
}