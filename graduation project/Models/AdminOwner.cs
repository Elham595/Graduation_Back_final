using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace graduation_project.Models
{
    [Table("Admin_Owner")]
    public partial class AdminOwner
    {
        public AdminOwner()
        {
            Bloggers = new HashSet<Blogger>();
        }

        [Key]
        [StringLength(50)]
        [Unicode(false)]
        public string UserName { get; set; }
        [Required]
        [StringLength(50)]
        [Unicode(false)]
        public string Email { get; set; }

        [ForeignKey(nameof(Email))]
        [InverseProperty(nameof(Account.AdminOwners))]
        public virtual Account EmailNavigation { get; set; }
        [ForeignKey(nameof(UserName))]
        [InverseProperty(nameof(User.AdminOwner))]
        public virtual User UserNameNavigation { get; set; }
        [InverseProperty(nameof(Blogger.AdminUserNameNavigation))]
        public virtual ICollection<Blogger> Bloggers { get; set; }
    }
}
