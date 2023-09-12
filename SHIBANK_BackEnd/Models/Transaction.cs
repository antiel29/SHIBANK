using System.ComponentModel.DataAnnotations;

namespace SHIBANK.Models
{
    public class Transaction
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Transaction type is required.")]
        public string Type { get; set; }

        [Required(ErrorMessage ="Amount is required.")]
        [Range(0, double.MaxValue,ErrorMessage ="Amount must be non-negative.")]
        public decimal Amount { get; set; }

        public DateTime Date { get; set; }

        public int BankAccountId { get; set; }

    }
}
