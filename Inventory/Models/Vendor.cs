namespace Inventory.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    
    public partial class Vendor
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Vendor()
        {
            Invoices = new HashSet<Invoice>();
            Purchase_Order = new HashSet<Purchase_Order>();
        }

        [Key]
        [StringLength(10)]
        public string Vendor_Id { get; set; }

        [StringLength(50)]
        public string Contact_PersonFname { get; set; }

        public string Mobile_No { get; set; }

        public string Address { get; set; }


        public string Bank_Acc_Number { get; set; }

        public int Paytym_Number { get; set; }

        [StringLength(50)]

        public string Company_Name { get; set; }

        [StringLength(50)]
        public string Contact_PersonLname { get; set; }

        public int LandLine_Num { get; set; }

        [StringLength(50)]
        public string Bank_Name { get; set; }

        [StringLength(50)]
        public string Bank_Branch { get; set; }
         public string country { get; set; }
        
        public string logo { get; set; }
        public string image { get; set; }
        [StringLength(100)]
        public string Remarks { get; set; }

        [StringLength(50)]
       
        public string Email { get; set; }

        [StringLength(50)]
        public string Adhar_Number { get; set; }
        
        public string Job_position { get; set; }
       
        public string Note { get; set; }
       
        public string bill_street { get; set; }
        [StringLength(50)]
        public string bill_city { get; set; }
        [StringLength(50)]
        public string bill_state { get; set; }
        public string bill_postalcode { get; set; }
        [StringLength(50)]
        public string bill_country { get; set; }
        [StringLength(50)]
        public string ship_street { get; set; }
        public string ship_city { get; set; }
        [StringLength(50)]
        public string ship_state { get; set; }
        [StringLength(50)]
        public string ship_postalcode { get; set; }
        public string ship_country { get; set; }

        public string activationcode { get; set; }

        public int company_Id { get; set; }
        public int RetVal { get; set; }

        public string emailid { get; set; }
       // public byte[] logo { get; set; }
      public string IFSC_No { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Invoice> Invoices { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Purchase_Order> Purchase_Order { get; set; }
    }
}
