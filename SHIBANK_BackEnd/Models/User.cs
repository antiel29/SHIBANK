using Microsoft.AspNetCore.Identity;

namespace SHIBANK.Models
{
    public class User : IdentityUser<int>
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string Role { get; set; } = "user";

        //Relations
        public ICollection<BankAccount>? BankAccounts { get; set; }

    }
    public class Role : IdentityRole<int>{}
}
