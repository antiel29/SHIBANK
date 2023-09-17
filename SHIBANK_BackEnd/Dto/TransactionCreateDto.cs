namespace SHIBANK.Dto
{
    public class TransactionCreateDto
    {
        public int BankAccountId { get; set; }
        public string Type { get; set; }
        public decimal Amount { get; set; }
    }
}
