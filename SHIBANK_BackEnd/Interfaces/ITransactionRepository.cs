using SHIBANK.Models;

namespace SHIBANK.Interfaces
{
    public interface ITransactionRepository
    {
        ICollection<Transaction> GetTransactions();

        Transaction GetTransaction(int id);

        ICollection<Transaction> GetTransactionsByBankAccount(int bankAccountId);

        ICollection<Transaction> GetTransactionsByUsername(string username);
        ICollection<Transaction> GetTransactionsRecievedUsername(string username);


        bool TransactionExists(int id);

        bool CreateTransaction(Transaction transaction);
        bool Save();

    }
}
