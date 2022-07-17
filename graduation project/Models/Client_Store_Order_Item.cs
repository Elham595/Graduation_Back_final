using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace graduation_project.Models
{
    public partial class Client_Store_Order_Item
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
        [InverseProperty("Client_Store_Order_Items")]
        public virtual Color Color { get; set; }
        [ForeignKey(nameof(FabricId))]
        [InverseProperty("Client_Store_Order_Items")]
        public virtual Fabric Fabric { get; set; }
        [ForeignKey(nameof(OrderId))]
        [InverseProperty(nameof(Client_Store_Order.Client_Store_Order_Items))]
        public virtual Client_Store_Order Order { get; set; }
    }
}
