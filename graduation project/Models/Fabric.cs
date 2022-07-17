using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace graduation_project.Models
{
    [Table("Fabric")]
    public partial class Fabric
    {
        public Fabric()
        {
            ClientStoreOrderItems = new HashSet<ClientStoreOrderItem>();
            DesignOrderFabrics = new HashSet<DesignOrderFabric>();
            FabricOfStores = new HashSet<FabricOfStore>();
        }

        [Key]
        [Column("FabricID")]
        public int FabricId { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string FabricName { get; set; }

        [InverseProperty(nameof(ClientStoreOrderItem.Fabric))]
        public virtual ICollection<ClientStoreOrderItem> ClientStoreOrderItems { get; set; }
        [InverseProperty(nameof(DesignOrderFabric.Fabric))]
        public virtual ICollection<DesignOrderFabric> DesignOrderFabrics { get; set; }
        [InverseProperty(nameof(FabricOfStore.Fabric))]
        public virtual ICollection<FabricOfStore> FabricOfStores { get; set; }
    }
}
