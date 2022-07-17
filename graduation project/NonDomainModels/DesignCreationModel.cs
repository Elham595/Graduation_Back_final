using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace graduation_project.NonDomainModels
{
    public class DesignCreationModel
    {
         [Required]
        public IFormFile? Image { get; set; }
         [Required]

        public string? Status { get; set; }
        [Required]
        public string? ClotheName { get; set; }

        public List<string>? UserName { get; set; }
    }
}
