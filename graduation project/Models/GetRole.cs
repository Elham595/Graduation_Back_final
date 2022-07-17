using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace graduation_project.Models
{
    [Keyless]
    public partial class GetRole
    {
        [Column("RoleID")]
        public int RoleId { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string RoleName { get; set; }
    }
}
