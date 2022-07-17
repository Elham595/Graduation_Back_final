using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace graduation_project.Models
{
    [Table("Designer_Of_Design")]
    public partial class DesignerOfDesign
    {
        [Key]
        [Column("DesignID")]
        public int DesignId { get; set; }
        [Key]
        [StringLength(50)]
        [Unicode(false)]
        public string UserName { get; set; }

        [ForeignKey(nameof(DesignId))]
        [InverseProperty("DesignerOfDesigns")]
        public virtual Design Design { get; set; }
        [ForeignKey(nameof(UserName))]
        [InverseProperty(nameof(Designer.DesignerOfDesigns))]
        public virtual Designer UserNameNavigation { get; set; }
    }
}
