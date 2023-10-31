using System.ComponentModel.DataAnnotations;

namespace SHIBANK.Dto
{
    public class UserUpdateDto
    {
        [MinLength(3, ErrorMessage = "Username must be at least 3 characters.")]
        [MaxLength(50, ErrorMessage = "Username cannot exceed 50 characters.")]
        public string? Username { get; set; }

        [MaxLength(50, ErrorMessage = "First name cannot exceed 50 characters.")]
        public string? FirstName { get; set; }

        [MaxLength(50, ErrorMessage = "Last name cannot exceed 50 characters.")]
        public string? LastName { get; set; }

        [EmailAddress(ErrorMessage = "Invalid email.")]
        [MaxLength(100, ErrorMessage = "Email address cannot exceed 100 characters.")]
        public string? Email { get; set; }
    }
}

