using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace graduation_project.Models
{
    [Table("Design_Order")]
    public partial class Design_Order
    {
        public Design_Order()
        {
            Design_Order_Fabrics = new HashSet<Design_Order_Fabric>();
            Tailor_Offers = new HashSet<Tailor_Offer>();
        }

        [Key]
        public int DesignOrderNumber { get; set; }
        [Column(TypeName = "date")]
        public DateTime? OrderDate { get; set; }
        public int? DesignID { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string UserName { get; set; }

        [ForeignKey(nameof(DesignID))]
        [InverseProperty("Design_Orders")]
        public virtual Design Design { get; set; }
        [ForeignKey(nameof(UserName))]
        [InverseProperty(nameof(Client.DesignOrders))]
        public virtual Client UserNameNavigation { get; set; }
        [InverseProperty("DesignOrderNumberNavigation")]
        public virtual BottomMeasurement Bottom_Measurement { get; set; }
        [InverseProperty("DesignOrderNumberNavigation")]
        public virtual TopMeasurment Top_Measurment { get; set; }
        [InverseProperty(nameof(Design_Order_Fabric.DesignOrderNumberNavigation))]
        public virtual ICollection<Design_Order_Fabric> Design_Order_Fabrics { get; set; }
       
        [InverseProperty(nameof(Tailor_Offer.DesignOrderNumberNavigation))]
        public virtual ICollection<Tailor_Offer> Tailor_Offers { get; set; }
    }
}
