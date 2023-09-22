using System.ComponentModel.DataAnnotations;

namespace SHIBANK.Dto
{
    public class UserUpdateDto
    {
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [EmailAddress(ErrorMessage = "Invalid Email")]
        public string Email { get; set; }
    }
}
