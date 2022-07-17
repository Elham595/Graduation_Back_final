using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace graduation_project.Models
{
    [Table("Tailor_Offer")]
    public partial class Tailor_Offer
    {
        public Tailor_Offer()
        {
            Tailor_Offer_Fabric_Metters = new HashSet<Tailor_Offer_Fabric_Metter>();
        }

        [Key]
        public int OfferId { get; set; }
        public int DesignOrderNumber { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string ClientUserName { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string TailorUserName { get; set; }
        [Column(TypeName = "date")]
        public DateTime OfferDate { get; set; }
        public int NumberOfDays { get; set; }
        [Column(TypeName = "money")]
        public decimal Price { get; set; }
        [Required]
        [StringLength(2)]
        [Unicode(false)]
        public string Status { get; set; }

        [ForeignKey(nameof(ClientUserName))]
        [InverseProperty(nameof(Client.TailorOffers))]
        public virtual Client ClientUserNameNavigation { get; set; }
        [ForeignKey(nameof(DesignOrderNumber))]
        [InverseProperty(nameof(Design_Order.Tailor_Offers))]
        public virtual Design_Order DesignOrderNumberNavigation { get; set; }
        [ForeignKey(nameof(TailorUserName))]
        [InverseProperty(nameof(Tailor.TailorOffers))]
        public virtual Tailor TailorUserNameNavigation { get; set; }
        [InverseProperty("Offer")]
        public virtual Aproved_Offer Aproved_Offer { get; set; }
        [InverseProperty(nameof(Tailor_Offer_Fabric_Metter.Offer))]
        public virtual ICollection<Tailor_Offer_Fabric_Metter> Tailor_Offer_Fabric_Metters { get; set; }
    }
}
