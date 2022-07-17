using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace graduation_project.Models
{
    [Table("Completed_Order")]
    public partial class CompletedOrder
    {
        [Key]
        public int OfferId { get; set; }
        [Column(TypeName = "date")]
        public DateTime DateOfRecieved { get; set; }
        public double? Rating { get; set; }

        [ForeignKey(nameof(OfferId))]
        [InverseProperty(nameof(AprovedOffer.CompletedOrder))]
        public virtual AprovedOffer Offer { get; set; }
    }
}
