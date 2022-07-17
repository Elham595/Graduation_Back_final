using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace graduation_project.Models
{
    [Table("Bottom_Measurement")]
    public partial class BottomMeasurement
    {
        [Key]
        public int DesignOrderNumber { get; set; }
        public double? Thigh { get; set; }
        public double? Inseam { get; set; }
        public double? OutSeam { get; set; }
        public double? Ankle { get; set; }
        public double? CrotchDepth { get; set; }

        [ForeignKey(nameof(DesignOrderNumber))]
        [InverseProperty(nameof(DesignOrder.BottomMeasurement))]
        public virtual DesignOrder DesignOrderNumberNavigation { get; set; }
    }
}
