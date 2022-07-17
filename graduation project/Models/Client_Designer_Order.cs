using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace graduation_project.Models
{
    [Table("Client_Designer_Order")]
    public partial class Client_Designer_Order
    {
        [Column(TypeName = "date")]
        public DateTime Date { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string ClientUserName { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string DesignerUserName { get; set; }
        public int? Price { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string DesignPicture { get; set; }
        [Key]
        public int Id { get; set; }

        [ForeignKey(nameof(ClientUserName))]
        [InverseProperty(nameof(Client.ClientDesignerOrders))]
        public virtual Client ClientUserNameNavigation { get; set; }
        [ForeignKey(nameof(DesignerUserName))]
        [InverseProperty(nameof(Designer.ClientDesignerOrders))]
        public virtual Designer DesignerUserNameNavigation { get; set; }
    }
}
