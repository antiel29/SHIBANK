﻿namespace SHIBANK.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public string? TransactionCode { get; set; }
        public string? Message { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string? SourceUsername { get; set; }
        public string? DestinyUsername { get; set; }

        //Relations
        public int BankAccountId { get; set; }
        public BankAccount? BankAccount { get; set; }

    }
}
