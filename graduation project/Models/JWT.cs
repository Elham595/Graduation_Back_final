namespace graduation_project.Models
{
    public class JWT
    {

        public string Key { get; set; }

        public string Audience { get; set; }

        public string Issuer { get; set; }

        public double DurationInHours { get; set; }

    }
}
