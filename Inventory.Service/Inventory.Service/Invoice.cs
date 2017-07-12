namespace Inventory
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Invoice")]
    public partial class Invoice
    {
        [Key]
        [StringLength(10)]
        public string Invoice_Number { get; set; }

        [Column(TypeName = "money")]
        public decimal? Amount { get; set; }

        [StringLength(10)]
        public string Staff_Id { get; set; }

        [StringLength(10)]
        public string Vendor_Id { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Invoice_Date { get; set; }

        [StringLength(50)]
        public string Invoice_copy { get; set; }

        public int? Payment_Id { get; set; }

        [StringLength(100)]
        public string Remarks { get; set; }

        public virtual Vendor Vendor { get; set; }

        public virtual Payment Payment { get; set; }

        public virtual Staff Staff { get; set; }
    }
}
