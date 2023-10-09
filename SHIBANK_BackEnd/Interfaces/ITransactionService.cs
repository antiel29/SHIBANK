﻿using SHIBANK.Dto;
using SHIBANK.Models;

namespace SHIBANK.Interfaces
{
    public interface ITransactionService 
    {
        IEnumerable <Transaction> GetTransactions();
        Transaction GetTransaction (int id);
        bool TransactionExists(int id);

        IEnumerable<Transaction> GetTransactionsByBankAccount(int bankAccountId);

        IEnumerable<Transaction> GetTransactionsByBankAccount(string AccountNumber);

        IEnumerable<Transaction> GetTransactionsByUsername(string username);

        ICollection<Transaction> GetTransactionsRecievedUsername(string username);

        ICollection<Transaction> GetTransactionsRecieved(string accountNumber);

        Transaction CreateTransactionOD(TransactionCreateDto transaction,BankAccount origin, BankAccount destiny);

        bool CreateTransaction(Transaction transaction);

    }
}
