using SHIBANK.Models;

namespace SHIBANK.Interfaces
{
    public interface ITransactionRepository
    {
        ICollection<Transaction> GetTransactions();

        Transaction GetTransaction(int id);

        ICollection<Transaction> GetTransactionsByBankAccount(int bankAccountId);

        ICollection<Transaction> GetTransactionsByBankAccount(string accountNumber);

        ICollection<Transaction> GetTransactionsByUsername(string username);
        ICollection<Transaction> GetTransactionsRecievedUsername(string username);

        ICollection<Transaction> GetTransactionsRecieved(string accountNumber);


        bool TransactionExists(int id);

        bool CreateTransaction(Transaction transaction);
        bool Save();

    }
}
