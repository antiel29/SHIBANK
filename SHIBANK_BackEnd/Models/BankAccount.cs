namespace SHIBANK.Models
{
    public class BankAccount
    {
        public int Id { get; set; }
        public string? CBU { get; set;}
        public decimal Balance { get; set;}


        //Relations
        public int UserId { get; set; }
        public User? User { get; set; }
        public ICollection<Transaction>? Transactions { get; set; }

        public ICollection<Card>? Cards { get; set; }

    }
}
