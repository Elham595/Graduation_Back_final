using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace graduation_project.Models
{
    [Table("Clothe")]
    public partial class Clothe
    {
        public Clothe()
        {
            Designs = new HashSet<Design>();
        }

        [Key]
        [Column("ClotheID")]
        public int ClotheId { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string ClotheName { get; set; }

        [InverseProperty(nameof(Design.Cloth))]
        public virtual ICollection<Design> Designs { get; set; }
    }
}
