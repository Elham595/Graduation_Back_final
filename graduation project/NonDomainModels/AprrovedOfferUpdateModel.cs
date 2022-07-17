using System.ComponentModel.DataAnnotations;

namespace graduation_project.NonDomainModels
{
    public class AprrovedOfferUpdateModel
    {
        [Required]
        public int DesignOrderNumber { get; set; }
        [Required]
        public DateTime DateOfRecievedFabric { get; set; }

        [Required]
        [StringLength(50)]
        public string RecievedMethod { get; set; }
    }
}
