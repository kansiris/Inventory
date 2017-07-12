namespace Inventory
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class User
    {
        [StringLength(50)]
        public string User_Name { get; set; }

        [StringLength(50)]
        public string User_Type { get; set; }

        [StringLength(50)]
        public string Description { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int User_Id { get; set; }

        [StringLength(100)]
        public string Remarks { get; set; }
    }
}
