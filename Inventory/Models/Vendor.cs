namespace Inventory.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Vendor")]
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
        public string Contact_Person1Fname { get; set; }

        public int? Mobile_No { get; set; }

        public string Address { get; set; }

        public int? Bank_Acc_Number { get; set; }

        public int? Paytym_Number { get; set; }

        [StringLength(50)]
        public string Company_Name { get; set; }

        [StringLength(50)]
        public string Contact_Person1Lname { get; set; }

        public int? LandLine_Num { get; set; }

        [StringLength(50)]
        public string Bank_Name { get; set; }

        [StringLength(50)]
        public string Bank_Branch { get; set; }

        [Column(TypeName = "image")]
        public byte[] Logo { get; set; }

        [StringLength(100)]
        public string Remarks { get; set; }

        [StringLength(50)]
        public string Contact_Person2Lname { get; set; }

        [StringLength(50)]
        public string Contact_Person2Fname { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        [StringLength(50)]
        public string Adhar_Number { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Invoice> Invoices { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Purchase_Order> Purchase_Order { get; set; }
    }
}
