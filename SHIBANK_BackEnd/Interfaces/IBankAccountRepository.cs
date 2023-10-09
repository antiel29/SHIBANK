using SHIBANK.Models;

namespace SHIBANK.Interfaces
{
    public interface IBankAccountRepository
    {
        ICollection<BankAccount> GetBankAccounts();
        BankAccount GetBankAccount(int id);

        BankAccount GetBankAccount(string accountNumber);

        ICollection<BankAccount> GetBankAccountsByUser(int userId);

        bool BankAccountExists(int id);
        bool BankAccountExists(string accountNumber);

        bool CreateBankAccount(BankAccount bankAccount);

        bool Withdraw(BankAccount bankAccount);

        bool Deposit(BankAccount bankAccount);

        bool DeleteBankAccount(BankAccount bankAccount);
        bool Save();

    }
}
