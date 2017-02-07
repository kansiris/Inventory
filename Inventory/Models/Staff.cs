namespace Inventory.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Staff")]
    public partial class Staff
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Staff()
        {
            Franchises = new HashSet<Franchise>();
            Invoices = new HashSet<Invoice>();
            Purchase_Order = new HashSet<Purchase_Order>();
        }

        [StringLength(50)]
        public string Staff_Name { get; set; }

        public int? Mobile_No { get; set; }

        public string Staff_Address { get; set; }

        [Key]
        [StringLength(10)]
        public string Staff_Id { get; set; }

        public int? Status_ID { get; set; }

        [StringLength(100)]
        public string Remarks { get; set; }

        [StringLength(100)]
        public string Email { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Franchise> Franchises { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Invoice> Invoices { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Purchase_Order> Purchase_Order { get; set; }

        public virtual Staff_status Staff_status { get; set; }
    }
}
