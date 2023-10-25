using System.ComponentModel.DataAnnotations;

namespace SHIBANK.Models
{
    public class Transaction
    {
        public int Id { get; set; }

        public string Message { get; set; }

        [Required(ErrorMessage ="Amount is required.")]
        [Range(0, double.MaxValue,ErrorMessage ="Amount must be non-negative.")]
        public decimal Amount { get; set; }

        public DateTime Date { get; set; }

        public string OriginUsername { get; set; }
        public string DestinyUsername { get; set; }

        public string OriginAccountNumber { get; set; }

        public string DestinyAccountNumber { get; set; }


        //Relations
        public int BankAccountId { get; set; }
        //public BankAccount BankAccount { get; set;}

    }
}
