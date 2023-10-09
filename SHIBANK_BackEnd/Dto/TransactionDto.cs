using System.ComponentModel.DataAnnotations;

namespace SHIBANK.Dto
{
    public class TransactionDto
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string OriginUsername { get; set; }
        public string DestinyUsername { get; set; }
        public string OriginAccountNumber { get; set; }
        public string DestinyAccountNumber { get; set; }
    }
}
