namespace Inventory.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("UnitOfMeasure")]
    public partial class UnitOfMeasure
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public UnitOfMeasure()
        {
            Items = new HashSet<Item>();
        }

        [Key]
        [StringLength(10)]
        public string UnitOfMeasure_Id { get; set; }

        [StringLength(10)]
        public string UnitOfMeasure_Name { get; set; }

        public string Description { get; set; }

        [StringLength(100)]
        public string Remarks { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Item> Items { get; set; }
    }
}
