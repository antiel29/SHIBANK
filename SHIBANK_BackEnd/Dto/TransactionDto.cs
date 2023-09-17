using System.ComponentModel.DataAnnotations;

namespace SHIBANK.Dto
{
    public class TransactionDto
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
    }
}
