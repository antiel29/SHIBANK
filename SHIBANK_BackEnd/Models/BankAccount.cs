using SHIBANK.Enums;

namespace SHIBANK.Models
{
    public class BankAccount
    {
        public int Id { get; set; }
        public string? CBU { get; set;}
        public decimal Balance { get; set;}
        public BankAccountType Type { get; set;}
        public DateTime OpeningDate { get; set;}


        public DateTime? LastInterestDate { get; set;}
        public decimal? InterestGenerating { get; set;}
        public decimal? Interest { get; set; } 
        public int? TransactionCount { get; set; }
        public int? TransactionLimit { get; set; }

        //Relations
        public int UserId { get; set; }
        public User? User { get; set; }
        public ICollection<Transaction>? Transactions { get; set; }

    }
}
