using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace graduation_project.Models
{
    [Table("Design_Order")]
    public partial class DesignOrder
    {
        public DesignOrder()
        {
            DesignOrderFabrics = new HashSet<DesignOrderFabric>();
            TailorOffers = new HashSet<TailorOffer>();
        }

        [Key]
        public int DesignOrderNumber { get; set; }
        [Column(TypeName = "date")]
        public DateTime? OrderDate { get; set; }
        [Column("DesignID")]
        public int? DesignId { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string UserName { get; set; }

        [ForeignKey(nameof(DesignId))]
        [InverseProperty("DesignOrders")]
        public virtual Design Design { get; set; }
        [ForeignKey(nameof(UserName))]
        [InverseProperty(nameof(Client.DesignOrders))]
        public virtual Client UserNameNavigation { get; set; }
        [InverseProperty("DesignOrderNumberNavigation")]
        public virtual BottomMeasurement BottomMeasurement { get; set; }
        [InverseProperty("DesignOrderNumberNavigation")]
        public virtual TopMeasurment TopMeasurment { get; set; }
        [InverseProperty(nameof(DesignOrderFabric.DesignOrderNumberNavigation))]
        public virtual ICollection<DesignOrderFabric> DesignOrderFabrics { get; set; }
        [InverseProperty(nameof(TailorOffer.DesignOrderNumberNavigation))]
        public virtual ICollection<TailorOffer> TailorOffers { get; set; }
    }
}
