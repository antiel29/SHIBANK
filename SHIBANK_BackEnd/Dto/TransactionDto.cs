namespace SHIBANK.Dto
{
    public class TransactionDto
    {
        public int Id { get; set; }
        public string? TransactionCode { get; set; }
        public string? Message { get; set; }
        public decimal Amount { get; set; }
        public string? Date { get; set; }
        public string? SourceUsername { get; set; }
        public string? DestinyUsername { get; set; }
    }
}
