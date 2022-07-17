using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace graduation_project.NonDomainModels
{
    public class TailorOfferCreation
    {
        [Required]
        public int DesignOrderNumber { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string ClientUserName { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string TailorUserName { get; set; }
       
        [Required]
        public int NumberOfDays { get; set; }

        [Column(TypeName = "money")]
        [Required]
        public decimal Price { get; set; }

        [Required]
        public List<FabricMeterCreationModel> FabricMeterList { get; set; }
        
           
    }
}
