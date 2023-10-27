using System.ComponentModel.DataAnnotations;

namespace SHIBANK.Dto
{
    public class TransactionCreateDto
    {
        [Required(ErrorMessage = "Origin account number is required.")]
        public string OriginAccountNumber { get; set; }

        [Required(ErrorMessage = "Destiny account number is required.")]
        public string DestinyAccountNumber { get; set; }

        [Required(ErrorMessage = "Amount is required.")]
        [Range(0, double.MaxValue, ErrorMessage = "Amount must be non-negative.")]
        public decimal Amount { get; set; }
        public string? Message { get; set; }
    }
}
