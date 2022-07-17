using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace graduation_project.Models
{
    [Table("Client_Store_Order_Items")]
    public partial class ClientStoreOrderItem
    {
        [Required]
        [StringLength(75)]
        [Unicode(false)]
        public string OrderId { get; set; }
        public int FabricId { get; set; }
        public int ColorId { get; set; }
        public double NumberOfMeters { get; set; }
        [Column(TypeName = "money")]
        public decimal Price { get; set; }
        [Key]
        public int ItemNumber { get; set; }

        [ForeignKey(nameof(ColorId))]
        [InverseProperty("ClientStoreOrderItems")]
        public virtual Color Color { get; set; }
        [ForeignKey(nameof(FabricId))]
        [InverseProperty("ClientStoreOrderItems")]
        public virtual Fabric Fabric { get; set; }
        [ForeignKey(nameof(OrderId))]
        [InverseProperty(nameof(ClientStoreOrder.ClientStoreOrderItems))]
        public virtual ClientStoreOrder Order { get; set; }
    }
}
