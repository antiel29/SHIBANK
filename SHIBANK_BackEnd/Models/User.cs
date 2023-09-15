using System.ComponentModel.DataAnnotations;

namespace SHIBANK.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Username is required.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [StringLength(maximumLength: 50, ErrorMessage = "Password must be at least 10 characters.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "FirstName is required.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "LastName is required.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email")]
        public string Email { get; set; }


        //Relaciones
        public ICollection<BankAccount> BankAccounts { get; set; }

    }
}
