using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace graduation_project.Models
{
    [Table("Design_Order_Fabric")]
    public partial class DesignOrderFabric
    {
        [Key]
        [Column("FabricID")]
        public int FabricId { get; set; }
        [Key]
        public int DesignOrderNumber { get; set; }
        [Key]
        [Column("ColorID")]
        public int ColorId { get; set; }

        [ForeignKey(nameof(ColorId))]
        [InverseProperty("DesignOrderFabrics")]
        public virtual Color Color { get; set; }
        [ForeignKey(nameof(DesignOrderNumber))]
        [InverseProperty(nameof(DesignOrder.DesignOrderFabrics))]
        public virtual DesignOrder DesignOrderNumberNavigation { get; set; }
        [ForeignKey(nameof(FabricId))]
        [InverseProperty("DesignOrderFabrics")]
        public virtual Fabric Fabric { get; set; }
    }
}
