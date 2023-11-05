using SHIBANK.Enums;

namespace SHIBANK.Dto
{
    public class CardDto
    {
        public int Id { get; set; }
        public CardType Type { get; set; }
        public string? LastFourDigits { get; set; }
        public string? ExpirationDate { get; set; }

        public decimal? AmountSpentThisMonth { get; set; }
        public decimal? Limit { get; set; }
    }
}
