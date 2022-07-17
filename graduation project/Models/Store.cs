using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace graduation_project.Models
{
    [Table("Store")]
    public partial class Store
    {
        public Store()
        {
            ClientStoreOrders = new HashSet<ClientStoreOrder>();
            FabricOfStores = new HashSet<FabricOfStore>();
        }

        [Key]
        public int StoreId { get; set; }
        [Required]
        [StringLength(50)]
        [Unicode(false)]
        public string StoreName { get; set; }
        [Required]
        [StringLength(50)]
        [Unicode(false)]
        public string Email { get; set; }
        [Required]
        [StringLength(50)]
        [Unicode(false)]
        public string UserName { get; set; }
        [Required]
        [StringLength(50)]
        [Unicode(false)]
        public string City { get; set; }
        [Required]
        [StringLength(50)]
        [Unicode(false)]
        public string Address { get; set; }

        [ForeignKey(nameof(Email))]
        [InverseProperty(nameof(Account.Stores))]
        public virtual Account EmailNavigation { get; set; }
        [ForeignKey(nameof(UserName))]
        [InverseProperty(nameof(User.Stores))]
        public virtual User UserNameNavigation { get; set; }
        [InverseProperty(nameof(ClientStoreOrder.Store))]
        public virtual ICollection<ClientStoreOrder> ClientStoreOrders { get; set; }
        [InverseProperty(nameof(FabricOfStore.Store))]
        public virtual ICollection<FabricOfStore> FabricOfStores { get; set; }
    }
}
