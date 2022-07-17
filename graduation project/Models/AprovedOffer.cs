using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace graduation_project.Models
{
    [Table("Aproved_Offer")]
    public partial class AprovedOffer
    {
        [Key]
        public int OfferId { get; set; }
        [Column(TypeName = "date")]
        public DateTime DateOfAproval { get; set; }
        [Column(TypeName = "date")]
        public DateTime DateOfRecievedFabric { get; set; }
        [Column(TypeName = "date")]
        public DateTime DateOfRecievedOrder { get; set; }
        [Required]
        [StringLength(50)]
        [Unicode(false)]
        public string RecievedMethod { get; set; }

        [ForeignKey(nameof(OfferId))]
        [InverseProperty(nameof(TailorOffer.AprovedOffer))]
        public virtual TailorOffer Offer { get; set; }
        [InverseProperty("Offer")]
        public virtual CompletedOrder CompletedOrder { get; set; }
    }
}
