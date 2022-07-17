using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace graduation_project.Models
{
    [Table("Account")]
    public partial class Account
    {
        public Account()
        {
            AdminOwners = new HashSet<AdminOwner>();
            Designers = new HashSet<Designer>();
            Stores = new HashSet<Store>();
            Tailors = new HashSet<Tailor>();
        }

        [Key]
        [StringLength(50)]
        [Unicode(false)]
        public string Email { get; set; }
        [Required]
        [StringLength(50)]
        [Unicode(false)]
        public string Password { get; set; }
        [Column(TypeName = "date")]
        public DateTime? CreationDate { get; set; }
        public int RoleId { get; set; }

        [ForeignKey(nameof(RoleId))]
        [InverseProperty(nameof(UserRole.Accounts))]
        public virtual UserRole Role { get; set; }
        [InverseProperty("EmailNavigation")]
        public virtual Rating Rating { get; set; }
        [InverseProperty(nameof(AdminOwner.EmailNavigation))]
        public virtual ICollection<AdminOwner> AdminOwners { get; set; }
        [InverseProperty(nameof(Designer.EmailNavigation))]
        public virtual ICollection<Designer> Designers { get; set; }
        [InverseProperty(nameof(Store.EmailNavigation))]
        public virtual ICollection<Store> Stores { get; set; }
        [InverseProperty(nameof(Tailor.EmailNavigation))]
        public virtual ICollection<Tailor> Tailors { get; set; }
    }
}
