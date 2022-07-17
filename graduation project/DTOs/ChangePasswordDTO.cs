using System.ComponentModel.DataAnnotations;

namespace graduation_project.DTOs
{
    public class ChangePasswordDTO
    {
        [Required(ErrorMessage = "Old Password is Required")]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "New Password is Required")]
        public string NewPassword { get; set; }

        [Required]
        [Compare(nameof(NewPassword))]
        public string ConfirmPassword { get; set; }
    }
}
