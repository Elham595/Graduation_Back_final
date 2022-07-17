namespace graduation_project.Models
{
    public class AddDesginModel
    {
        public DateTime DesignDate { get; set; }

        public string Status { get; set; }

        public int ClothId { get; set; }

        public IFormFile File { get; set; }
    }
}
