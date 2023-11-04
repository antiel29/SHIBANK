using System.ComponentModel.DataAnnotations;

namespace SHIBANK.Dto
{
    public class TransactionCreateDto
    {
        [Required(ErrorMessage = "Username is required.")]
        public string? Username { get; set; }

        [Required(ErrorMessage = "Amount is required.")]
        [Range(100, double.MaxValue, ErrorMessage = "Amount must be a non-negative value and minimum transfer amount is $100.")]
        public decimal Amount { get; set; }
        public string? Message { get; set; }
    }
}
