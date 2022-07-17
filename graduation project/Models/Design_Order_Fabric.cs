using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace graduation_project.Models
{
    [Table("Design_Order_Fabric")]
    public partial class Design_Order_Fabric
    {
        public Design_Order_Fabric()
        {
            Tailor_Offer_Fabric_Metters = new HashSet<Tailor_Offer_Fabric_Metter>();
        }

        [Key]
        public int FabricID { get; set; }
        [Key]
        public int DesignOrderNumber { get; set; }
        [Key]
        public int ColorID { get; set; }

        [ForeignKey(nameof(ColorID))]
        [InverseProperty("Design_Order_Fabrics")]
        public virtual Color Color { get; set; }
        [ForeignKey(nameof(DesignOrderNumber))]
        [InverseProperty(nameof(Design_Order.Design_Order_Fabrics))]
        public virtual Design_Order DesignOrderNumberNavigation { get; set; }
        [ForeignKey(nameof(FabricID))]
        [InverseProperty("Design_Order_Fabrics")]
        public virtual Fabric Fabric { get; set; }
        [InverseProperty(nameof(Tailor_Offer_Fabric_Metter.Design_Order_Fabric))]
        public virtual ICollection<Tailor_Offer_Fabric_Metter> Tailor_Offer_Fabric_Metters { get; set; }
    }
}
