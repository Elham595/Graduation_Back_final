using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace graduation_project.Models
{
    [Table("Tailor_Offer_Fabric_Metter")]
    public partial class Tailor_Offer_Fabric_Metter
    {
        [Key]
        public int SN { get; set; }
        public double NumberOfMeter { get; set; }
        public int FabricId { get; set; }
        public int DesignOrderNumber { get; set; }
        public int ColorId { get; set; }
        public int? OfferId { get; set; }

        [ForeignKey("FabricId,DesignOrderNumber,ColorId")]
        [InverseProperty("Tailor_Offer_Fabric_Metters")]
        public virtual Design_Order_Fabric Design_Order_Fabric { get; set; }
        [ForeignKey(nameof(OfferId))]
        [InverseProperty(nameof(Tailor_Offer.Tailor_Offer_Fabric_Metters))]
        public virtual Tailor_Offer Offer { get; set; }
    }
}
