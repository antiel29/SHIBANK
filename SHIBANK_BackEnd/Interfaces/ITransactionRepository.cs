using SHIBANK.Models;

namespace SHIBANK.Interfaces
{
    public interface ITransactionRepository
    {
        ICollection<Transaction> GetTransactions();
        Transaction GetTransaction(int id);
        ICollection<Transaction> GetSendedTransactionsByUsername(string username);
        ICollection<Transaction> GetRecievedTransactionsByUsername(string username);
        ICollection<Transaction> GetAllTransactionsByUsername(string username);
        bool TransactionExists(int id);
        bool TransactionExists(string transactionCode);
        bool CreateTransaction(Transaction transaction);
        bool DeleteTransaction(Transaction transaction);
        bool Save();

    }
}
