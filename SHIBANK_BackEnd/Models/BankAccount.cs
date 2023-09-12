using System.ComponentModel.DataAnnotations;

namespace SHIBANK.Models
{
    public class BankAccount
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Choose an account")]
        public string AccountNumber { get; set;}

        [Range(0, int.MaxValue)]
        public decimal Balance { get; set;}

        public int UserId { get; set; }
    }
}
