namespace Inventory.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Purchase_Order
    {
        [Key]
        [StringLength(10)]
        public string PO_No { get; set; }

        [StringLength(10)]
        public string Staff_ID { get; set; }

        [StringLength(10)]
        public string Vendor_ID { get; set; }

        public DateTime? PO_Date { get; set; }

        [Column(TypeName = "money")]
        public decimal? NetAmt { get; set; }

        [Column(TypeName = "money")]
        public decimal? TaxAmt { get; set; }

        [Column(TypeName = "money")]
        public decimal? GrossAmt { get; set; }

        public DateTime? DeliveryDate { get; set; }

        public virtual Staff Staff { get; set; }

        public virtual Vendor Vendor { get; set; }
    }
}
