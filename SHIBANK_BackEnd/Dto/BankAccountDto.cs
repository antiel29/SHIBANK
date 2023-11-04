using SHIBANK.Enums;

namespace SHIBANK.Dto
{   
    public class BankAccountDto
    {
        public int Id { get; set; }
        public decimal Balance { get; set; }
        public BankAccountType Type { get; set; }
        public DateTime OpeningDate { get; set; }
        public decimal Interest { get; set; } 
        public int TransactionCount { get; set; }
        public int TransactionLimit { get; set; }

    }
}
