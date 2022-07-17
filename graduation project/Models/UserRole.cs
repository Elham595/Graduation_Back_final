using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace graduation_project.Models
{
    [Table("User_Role")]
    public partial class UserRole
    {
        public UserRole()
        {
            Accounts = new HashSet<Account>();
        }

        [Key]
        [Column("RoleID")]
        public int RoleId { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string RoleName { get; set; }

        [InverseProperty(nameof(Account.Role))]
        public virtual ICollection<Account> Accounts { get; set; }
    }
}
