using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace graduation_project.Models
{
    [Table("Tailor")]
    public partial class Tailor
    {
        public Tailor()
        {
            TailorOffers = new HashSet<TailorOffer>();
        }

        [Key]
        [StringLength(50)]
        [Unicode(false)]
        public string UserName { get; set; }
        [Required]
        [StringLength(50)]
        [Unicode(false)]
        public string Email { get; set; }
        [Required]
        [StringLength(50)]
        [Unicode(false)]
        public string Address { get; set; }
        [Required]
        [StringLength(50)]
        [Unicode(false)]
        public string City { get; set; }
        public int ExperienceYears { get; set; }
        [Required]
        [StringLength(250)]
        [Unicode(false)]
        public string Bio { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string Gender { get; set; }

        [ForeignKey(nameof(Email))]
        [InverseProperty(nameof(Account.Tailors))]
        public virtual Account EmailNavigation { get; set; }
        [ForeignKey(nameof(UserName))]
        [InverseProperty(nameof(User.Tailor))]
        public virtual User UserNameNavigation { get; set; }
        [InverseProperty(nameof(TailorOffer.TailorUserNameNavigation))]
        public virtual ICollection<TailorOffer> TailorOffers { get; set; }
    }
}
