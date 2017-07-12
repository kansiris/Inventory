namespace Inventory
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Payment")]
    public partial class Payment
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Payment()
        {
            Invoices = new HashSet<Invoice>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Payment_Id { get; set; }

        [StringLength(50)]
        public string Payment_Method { get; set; }

        [Column(TypeName = "money")]
        public decimal? Payment_Amount { get; set; }

        public string Description { get; set; }

        [StringLength(100)]
        public string Remarks { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Payment_Date { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Invoice> Invoices { get; set; }

        public virtual Payment_Method_Types Payment_Method_Types { get; set; }
    }
}
