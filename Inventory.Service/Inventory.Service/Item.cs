namespace Inventory
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Item
    {
        [Key]
        [StringLength(10)]
        public string SKU { get; set; }

        [StringLength(50)]
        public string Item_Name { get; set; }

        [StringLength(50)]
        public string Short_Description { get; set; }

        public string Long_Description { get; set; }

        [StringLength(10)]
        public string Quantity { get; set; }

        [StringLength(10)]
        public string UnitOfMeasure_Id { get; set; }

        public int? Perishable { get; set; }

        [StringLength(50)]
        public string StockIn_Hand { get; set; }

        [StringLength(50)]
        public string Reoredr_Level { get; set; }

        [Column(TypeName = "image")]
        public byte[] Item_Image { get; set; }

        [Column(TypeName = "money")]
        public decimal? Cost_Price { get; set; }

        [Column(TypeName = "money")]
        public decimal? Selling_Price { get; set; }

        [StringLength(50)]
        public string Bar_Code { get; set; }

        public int? MinimumBeforeOrder { get; set; }

        [StringLength(100)]
        public string Remarks { get; set; }

        public virtual UnitOfMeasure UnitOfMeasure { get; set; }
    }
}
