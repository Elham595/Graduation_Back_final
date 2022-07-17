using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace graduation_project.Models
{
    [Table("Fabric_Of_Store")]
    public partial class Fabric_Of_Store
    {
        [Key]
        public int StoreId { get; set; }
        [Key]
        public int FabricId { get; set; }
        [Key]
        public int ColorId { get; set; }
        [Column(TypeName = "money")]
        public decimal Price { get; set; }

        [ForeignKey(nameof(ColorId))]
        [InverseProperty("Fabric_Of_Stores")]
        public virtual Color Color { get; set; }
        [ForeignKey(nameof(FabricId))]
        [InverseProperty("Fabric_Of_Stores")]
        public virtual Fabric Fabric { get; set; }
        [ForeignKey(nameof(StoreId))]
        [InverseProperty("Fabric_Of_Stores")]
        public virtual Store Store { get; set; }
    }
}
