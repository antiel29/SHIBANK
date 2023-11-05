using SHIBANK.Enums;

namespace SHIBANK.Models
{
    public class Card
    {
        public int Id { get; set; }
        public CardType Type { get; set; }
        public string? CardNumber { get; set; }

        public string? LastFourDigits { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string? CVC { get; set; }
        public decimal? Limit { get; set; }
        public decimal? AmountSpentThisMonth { get; set; }


        //Relations
        public int UserId { get; set; }
        public User? User { get; set; }
        public int BankAccountId { get; set; }

    }
}
