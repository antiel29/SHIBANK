using SHIBANK.Enums;

namespace SHIBANK.Models
{
    public class Card
    {
        public int Id { get; set; }
        public CardType Type { get; set; }
        public string? CardNumber { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string? CVC { get; set; }
        public decimal? Limit { get; set; }


        //Relations
        public int BankAccountId { get; set; }
        public BankAccount? BankAccount { get; set; }

    }
}
