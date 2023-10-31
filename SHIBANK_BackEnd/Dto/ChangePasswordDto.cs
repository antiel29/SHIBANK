using System.ComponentModel.DataAnnotations;

namespace SHIBANK.Dto
{
    public class ChangePasswordDto
    {
        [Required(ErrorMessage = "Old password is required.")]
        public string? oldPassword { get; set; }

        [Required(ErrorMessage = "New password is required.")]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters.")]
        [MaxLength(100, ErrorMessage = "Password cannot exceed 100 characters.")]
        [RegularExpression("^((?=.*[a-z])(?=.*[A-Z])(?=.*\\d)).*", ErrorMessage = "Password must have at leat one digit,one lowercase and one uppercase.")]
        public string? newPassword { get; set; }

    }
}
