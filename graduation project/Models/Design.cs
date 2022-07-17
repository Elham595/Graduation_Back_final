using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace graduation_project.Models
{
    [Table("Design")]
    public partial class Design
    {
        public Design()
        {
            DesignOrders = new HashSet<DesignOrder>();
            DesignerOfDesigns = new HashSet<DesignerOfDesign>();
        }

        [Key]
        [Column("DesignID")]
        public int DesignId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DesignDate { get; set; }
        [Required]
        [StringLength(10)]
        [Unicode(false)]
        public string Status { get; set; }
        [Column("ClothID")]
        public int? ClothId { get; set; }
        [Unicode(false)]
        public string DesignImage { get; set; }

        [ForeignKey(nameof(ClothId))]
        [InverseProperty(nameof(Clothe.Designs))]
        public virtual Clothe Cloth { get; set; }
        [InverseProperty(nameof(DesignOrder.Design))]
        public virtual ICollection<DesignOrder> DesignOrders { get; set; }
        [InverseProperty(nameof(DesignerOfDesign.Design))]
        public virtual ICollection<DesignerOfDesign> DesignerOfDesigns { get; set; }
    }
}
