using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace graduation_project.Models
{
    [Table("Blogger")]
    public partial class Blogger
    {
        public Blogger()
        {
            Instructions = new HashSet<Instruction>();
        }

        [Key]
        [Column("BloggerID")]
        public int BloggerId { get; set; }
        [Required]
        [StringLength(50)]
        [Unicode(false)]
        public string BloggerName { get; set; }
        [Required]
        [Column(TypeName = "image")]
        public byte[] BloggerImage { get; set; }
        [Required]
        [StringLength(150)]
        public string BloggerInstagram { get; set; }
        [Column(TypeName = "date")]
        public DateTime AddingDate { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string AdminUserName { get; set; }

        [ForeignKey(nameof(AdminUserName))]
        [InverseProperty(nameof(AdminOwner.Bloggers))]
        public virtual AdminOwner AdminUserNameNavigation { get; set; }
        [InverseProperty(nameof(Instruction.Blogger))]
        public virtual ICollection<Instruction> Instructions { get; set; }
    }
}
