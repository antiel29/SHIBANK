using SHIBANK.Dto;
using SHIBANK.Models;

namespace SHIBANK.Interfaces
{
    public interface ITransactionService 
    {
        IEnumerable <Transaction> GetTransactions();
        Transaction? GetTransaction (int id);
        bool TransactionExists(int id);
        bool TransactionExists(string transactionCode);
        public string GenerateUniqueTransactionCode();
        IEnumerable<Transaction> GetUserRecievedTransactions(User user);

        IEnumerable<Transaction> GetUserSendedTransactions(User user);

        bool DeleteTransaction(Transaction transaction);
        IEnumerable<Transaction> GetUserAllTransactions(User user);
        bool CreateTransaction(BankAccount origin, BankAccount destiny,TransactionCreateDto transaction);

    }
}
