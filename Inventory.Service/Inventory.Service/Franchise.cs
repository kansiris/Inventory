namespace Inventory
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Franchise
    {
        [Key]
        [StringLength(10)]
        public string Franchise_Id { get; set; }

        [StringLength(10)]
        public string Staff_Id { get; set; }

        [StringLength(50)]
        public string Franchise_Name { get; set; }

        [StringLength(50)]
        public string Location { get; set; }

        [Column(TypeName = "image")]
        public byte[] Logo { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Created_Date { get; set; }

        [StringLength(50)]
        public string Bank_Name { get; set; }

        [StringLength(50)]
        public string Accunt_Number { get; set; }

        [StringLength(50)]
        public string LandLine_Number { get; set; }

        [StringLength(100)]
        public string Remarks { get; set; }

        [StringLength(50)]
        public string Paytym_Number { get; set; }

        [StringLength(50)]
        public string Adhar_Number { get; set; }

        public virtual Staff Staff { get; set; }
    }
}
