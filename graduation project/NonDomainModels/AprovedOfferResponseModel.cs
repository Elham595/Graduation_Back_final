using graduation_project.Models;

namespace graduation_project.NonDomainModels
{
    public class AprovedOfferResponseModel
    {
        public TailorOffer TailorOffer { get; set; }
        public List<TailorOfferFabricMetter> TailorOfferFabricsList { get; set; }

        
    }
}
