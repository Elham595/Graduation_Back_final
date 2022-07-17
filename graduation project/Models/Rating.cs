using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace graduation_project.Models
{
    [Table("Rating")]
    public partial class Rating
    {
        [Key]
        [StringLength(50)]
        [Unicode(false)]
        public string Email { get; set; }
        public double RateNumber { get; set; }

        [ForeignKey(nameof(Email))]
        [InverseProperty(nameof(Account.Rating))]
        public virtual Account EmailNavigation { get; set; }
    }
}
