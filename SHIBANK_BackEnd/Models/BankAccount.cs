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



        //Relaciones
        public int UserId { get; set; }
        public User User { get; set; }
        public ICollection<Transaction> Transactions { get; set; }
    }
}
