using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace graduation_project.Models
{
    [Table("Instruction")]
    public partial class Instruction
    {
        [Key]
        public int InstructionNumber { get; set; }
        [Required]
        [StringLength(50)]
        public string InstructionName { get; set; }
        public int? BloggerId { get; set; }

        [ForeignKey(nameof(BloggerId))]
        [InverseProperty("Instructions")]
        public virtual Blogger Blogger { get; set; }
    }
}
