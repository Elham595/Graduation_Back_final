using System.ComponentModel.DataAnnotations;

namespace graduation_project.NonDomainModels
{
    public class DesignOrderCreationModel
    {
        [Required]
        public int DesignId { get; set; }

        [Required]
        [StringLength(50)]
        public string UserName { get; set; }

        public string Measurment { get; set; }

        public int? DesignOrderNumber { get; set; }
        public double? Thigh { get; set; }
        public double? Inseam { get; set; }
        public double? OutSeam { get; set; }
        public double? Ankle { get; set; }
        public double? CrotchDepth { get; set; }
        public double? Nick { get; set; }
        public double? Bust { get; set; }
        public double? Hip { get; set; }
        public double? Waist { get; set; }
        public double? FrontWaistLength { get; set; }
        public double? BackWaistLength { get; set; }
        public double? HighHip { get; set; }
        public double? ArmLength { get; set; }
         public DesignFabricModel[] designFabricModels { get; set; }
    }
}
