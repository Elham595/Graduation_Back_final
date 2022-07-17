using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace graduation_project.Models
{
    [Table("Message_Chat")]
    public partial class MessageChat
    {
        [Key]
        public int Id { get; set; }
        [Column(TypeName = "date")]
        public DateTime MessageDate { get; set; }
        [Required]
        [StringLength(50)]
        [Unicode(false)]
        public string SendFrom { get; set; }
        [Required]
        [StringLength(50)]
        [Unicode(false)]
        public string SendTo { get; set; }
        public int? DesignOrderNumber { get; set; }
        [Required]
        [StringLength(250)]
        [Unicode(false)]
        public string MessageText { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string MessageHead { get; set; }
    }
}
