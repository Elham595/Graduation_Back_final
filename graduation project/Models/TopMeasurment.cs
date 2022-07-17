using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace graduation_project.Models
{
    [Table("Top_Measurment")]
    public partial class TopMeasurment
    {
        [Key]
        public int DesignOrderNumber { get; set; }
        public double Nick { get; set; }
        public double Bust { get; set; }
        public double Hip { get; set; }
        public double Waist { get; set; }
        public double FrontWaistLength { get; set; }
        public double BackWaistLength { get; set; }
        public double HighHip { get; set; }
        public double ArmLength { get; set; }

        [ForeignKey(nameof(DesignOrderNumber))]
        [InverseProperty(nameof(DesignOrder.TopMeasurment))]
        public virtual DesignOrder DesignOrderNumberNavigation { get; set; }
    }
}
