namespace graduation_project.NonDomainModels
{
    public class TailorOfferDto
    {
        public TailorOfferDto(int offerId, string status, decimal price, int designOrderNamber, string firstName, string middleName, string lastName, DateTime offerDate, int experienceYear, double rateNumber)
        {
            OfferId = offerId;
            Status = status;
            Price = price;
            DesignOrderNamber = designOrderNamber;
            FirstName = firstName;
            MiddleName = middleName;
            LastName = lastName;
            OfferDate = offerDate;
            ExperienceYear = experienceYear;
            RateNumber = rateNumber;
        }

        public int OfferId { get; set; }
        public string Status { get; set; }
        public decimal Price { get; set; }
        public int DesignOrderNamber { get; set; }
        public string FirstName { get; set; }
        public string  MiddleName { get; set; }
        public string LastName { get; set; }
        public DateTime OfferDate { get; set; }

        public int ExperienceYear { get; set; }
        public double RateNumber { get; set; }

        

    }
}
