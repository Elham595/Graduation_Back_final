using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace graduation_project.Models
{
    [Table("Tailor_Offer_Fabric_Metter")]
    public partial class TailorOfferFabricMetter
    {
        [Key]
        [Column("SN")]
        public int Sn { get; set; }
        public double NumberOfMeter { get; set; }
        public int FabricId { get; set; }
        public int DesignOrderNumber { get; set; }
        public int ColorId { get; set; }
        public int? OfferId { get; set; }

        [ForeignKey(nameof(OfferId))]
        [InverseProperty(nameof(TailorOffer.TailorOfferFabricMetters))]
        public virtual TailorOffer Offer { get; set; }
    }
}
