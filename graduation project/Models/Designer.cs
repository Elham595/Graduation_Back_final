using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace graduation_project.Models
{
    [Table("Designer")]
    public partial class Designer
    {
        public Designer()
        {
            ClientDesignerOrders = new HashSet<ClientDesignerOrder>();
            DesignerOfDesigns = new HashSet<DesignerOfDesign>();
        }

        [Key]
        [StringLength(50)]
        [Unicode(false)]
        public string UserName { get; set; }
        [Required]
        [StringLength(50)]
        [Unicode(false)]
        public string Email { get; set; }
        public int ExperienceYear { get; set; }
        [Required]
        [StringLength(50)]
        [Unicode(false)]
        public string City { get; set; }
        [Required]
        [StringLength(50)]
        [Unicode(false)]
        public string Address { get; set; }
        [Required]
        [StringLength(250)]
        [Unicode(false)]
        public string Bio { get; set; }
        [Required]
        [StringLength(50)]
        [Unicode(false)]
        public string Gender { get; set; }

        [ForeignKey(nameof(Email))]
        [InverseProperty(nameof(Account.Designers))]
        public virtual Account EmailNavigation { get; set; }
        [ForeignKey(nameof(UserName))]
        [InverseProperty(nameof(User.Designer))]
        public virtual User UserNameNavigation { get; set; }
        [InverseProperty(nameof(ClientDesignerOrder.DesignerUserNameNavigation))]
        public virtual ICollection<ClientDesignerOrder> ClientDesignerOrders { get; set; }
        [InverseProperty(nameof(DesignerOfDesign.UserNameNavigation))]
        public virtual ICollection<DesignerOfDesign> DesignerOfDesigns { get; set; }
    }
}
