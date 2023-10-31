using System.ComponentModel.DataAnnotations;

namespace SHIBANK.Models
{
    public class BankAccount
    {
        public int Id { get; set; }
        public string AccountNumber { get; set;}

        [Range(0, int.MaxValue)]
        public decimal Balance { get; set;}


        //Relations
        public int UserId { get; set; }
        public User? User { get; set; }
        public ICollection<Transaction>? Transactions { get; set; }

    }
}
