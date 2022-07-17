using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace graduation_project.Models
{
    [Table("User")]
    public partial class User
    {
        public User()
        {
            Stores = new HashSet<Store>();
        }

        [Key]
        [StringLength(50)]
        [Unicode(false)]
        public string UserName { get; set; }
        [Required]
        [StringLength(50)]
        [Unicode(false)]
        public string FirstName { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string MiddleName { get; set; }
        [Required]
        [StringLength(50)]
        [Unicode(false)]
        public string LastName { get; set; }
        [Column(TypeName = "date")]
        public DateTime BirthDate { get; set; }
        [Required]
        [StringLength(50)]
        [Unicode(false)]
        public string MobileNumber { get; set; }

        [InverseProperty("UserNameNavigation")]
        public virtual AdminOwner AdminOwner { get; set; }
        [InverseProperty("UserNameNavigation")]
        public virtual Client Client { get; set; }
        [InverseProperty("UserNameNavigation")]
        public virtual Designer Designer { get; set; }
        [InverseProperty("UserNameNavigation")]
        public virtual Tailor Tailor { get; set; }
        [InverseProperty(nameof(Store.UserNameNavigation))]
        public virtual ICollection<Store> Stores { get; set; }
    }
}
