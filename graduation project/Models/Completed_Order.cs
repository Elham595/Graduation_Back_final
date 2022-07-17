using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace graduation_project.Models
{
    [Table("Completed_Order")]
    public partial class Completed_Order
    {
        [Key]
        public int OfferId { get; set; }
        [Column(TypeName = "date")]
        public DateTime DateOfRecieved { get; set; }
        public double Rating { get; set; }

        [ForeignKey(nameof(OfferId))]
        [InverseProperty(nameof(Aproved_Offer.Completed_Order))]
        public virtual Aproved_Offer Offer { get; set; }
    }
}
