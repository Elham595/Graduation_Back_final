using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace graduation_project.Models
{
    [Table("Client_Store_Order")]
    public partial class ClientStoreOrder
    {
        public ClientStoreOrder()
        {
            ClientStoreOrderItems = new HashSet<ClientStoreOrderItem>();
        }

        [Key]
        [StringLength(75)]
        [Unicode(false)]
        public string OrderId { get; set; }
        [Column(TypeName = "date")]
        public DateTime OrderDate { get; set; }
        [Required]
        [StringLength(50)]
        [Unicode(false)]
        public string RecievedMethod { get; set; }
        [Column(TypeName = "date")]
        public DateTime RecievedDate { get; set; }
        [Column(TypeName = "money")]
        public decimal TotalPrice { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string ClientUserName { get; set; }
        public int? StoreId { get; set; }

        [ForeignKey(nameof(ClientUserName))]
        [InverseProperty(nameof(Client.ClientStoreOrders))]
        public virtual Client ClientUserNameNavigation { get; set; }
        [ForeignKey(nameof(StoreId))]
        [InverseProperty("ClientStoreOrders")]
        public virtual Store Store { get; set; }
        [InverseProperty(nameof(ClientStoreOrderItem.Order))]
        public virtual ICollection<ClientStoreOrderItem> ClientStoreOrderItems { get; set; }
    }
}
