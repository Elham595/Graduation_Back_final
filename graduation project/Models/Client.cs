using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace graduation_project.Models
{
    [Table("Client")]
    public partial class Client
    {
        public Client()
        {
            ClientDesignerOrders = new HashSet<ClientDesignerOrder>();
            ClientStoreOrders = new HashSet<ClientStoreOrder>();
            DesignOrders = new HashSet<DesignOrder>();
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
        [StringLength(50)]
        [Unicode(false)]
        public string Address { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string City { get; set; }

        [ForeignKey(nameof(UserName))]
        [InverseProperty(nameof(User.Client))]
        public virtual User UserNameNavigation { get; set; }
        [InverseProperty(nameof(ClientDesignerOrder.ClientUserNameNavigation))]
        public virtual ICollection<ClientDesignerOrder> ClientDesignerOrders { get; set; }
        [InverseProperty(nameof(ClientStoreOrder.ClientUserNameNavigation))]
        public virtual ICollection<ClientStoreOrder> ClientStoreOrders { get; set; }
        [InverseProperty(nameof(DesignOrder.UserNameNavigation))]
        public virtual ICollection<DesignOrder> DesignOrders { get; set; }
        [InverseProperty(nameof(TailorOffer.ClientUserNameNavigation))]
        public virtual ICollection<TailorOffer> TailorOffers { get; set; }
    }
}
