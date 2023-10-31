using SHIBANK.Dto;
using Swashbuckle.AspNetCore.Filters;
using System.ComponentModel.DataAnnotations;

namespace SHIBANK.Dto
{
    public class UserRegisterDto
    {
        [Required(ErrorMessage = "Username is required.")]
        [MinLength(3, ErrorMessage = "Username must be at least 3 characters.")]
        [MaxLength(50, ErrorMessage = "Username cannot exceed 50 characters.")]
        public string? Username { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters.")]
        [MaxLength(100, ErrorMessage = "Password cannot exceed 100 characters.")]
        [RegularExpression("^((?=.*[a-z])(?=.*[A-Z])(?=.*\\d)).*", ErrorMessage = "Password must have at leat one digit,one lowercase and one uppercase.")]
        public string? Password { get; set; }

        [Required(ErrorMessage = "FirstName is required.")]
        [MaxLength(50, ErrorMessage = "First name cannot exceed 50 characters.")]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "LastName is required.")]
        [MaxLength(50, ErrorMessage = "Last name cannot exceed 50 characters.")]
        public string? LastName { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email.")]
        [MaxLength(100, ErrorMessage = "Email address cannot exceed 100 characters.")]
        public string? Email { get; set; }
    }
}

public class UserRegisterDtoExample : IExamplesProvider<UserRegisterDto>
{
    public UserRegisterDto GetExamples()
    {
        return new UserRegisterDto
        {
            Username = "example_username",
            Password = "Example123",
            FirstName = "example_firstName",
            LastName = "example_lastName",
            Email = "email@example.com"
        };
    }
}
