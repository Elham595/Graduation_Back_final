using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace graduation_project.Models
{
    [Table("Tailor_Offer")]
    public partial class TailorOffer
    {
        public TailorOffer()
        {
            TailorOfferFabricMetters = new HashSet<TailorOfferFabricMetter>();
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
        [InverseProperty(nameof(DesignOrder.TailorOffers))]
        public virtual DesignOrder DesignOrderNumberNavigation { get; set; }
        [ForeignKey(nameof(TailorUserName))]
        [InverseProperty(nameof(Tailor.TailorOffers))]
        public virtual Tailor TailorUserNameNavigation { get; set; }
        [InverseProperty("Offer")]
        public virtual AprovedOffer AprovedOffer { get; set; }
        [InverseProperty(nameof(TailorOfferFabricMetter.Offer))]
        public virtual ICollection<TailorOfferFabricMetter> TailorOfferFabricMetters { get; set; }
    }
}
