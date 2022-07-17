using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace graduation_project.Models
{
    [Table("Color")]
    public partial class Color
    {
        public Color()
        {
            ClientStoreOrderItems = new HashSet<ClientStoreOrderItem>();
            DesignOrderFabrics = new HashSet<DesignOrderFabric>();
            FabricOfStores = new HashSet<FabricOfStore>();
        }

        [Key]
        [Column("ColorID")]
        public int ColorId { get; set; }
        [StringLength(50)]
        public string ColorName { get; set; }

        [InverseProperty(nameof(ClientStoreOrderItem.Color))]
        public virtual ICollection<ClientStoreOrderItem> ClientStoreOrderItems { get; set; }
        [InverseProperty(nameof(DesignOrderFabric.Color))]
        public virtual ICollection<DesignOrderFabric> DesignOrderFabrics { get; set; }
        [InverseProperty(nameof(FabricOfStore.Color))]
        public virtual ICollection<FabricOfStore> FabricOfStores { get; set; }
    }
}
