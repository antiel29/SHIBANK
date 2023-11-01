using SHIBANK.Dto;
using Swashbuckle.AspNetCore.Filters;
using System.ComponentModel.DataAnnotations;

namespace SHIBANK.Dto
{
    public class UserLoginDto
    {
        [Required(ErrorMessage = "Username is required.")]
        public string? Username { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        public string? Password { get; set; }
    }
}
public class UserLoginDtoExample : IExamplesProvider<UserLoginDto>
{
    public UserLoginDto GetExamples()
    {
        return new UserLoginDto
        {
            Username = "antiel_ilundayn",
            Password = "Password123",
        };
    }
}