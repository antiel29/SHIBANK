using SHIBANK.Models;

namespace SHIBANK.Interfaces
{
    public interface IBankAccountRepository
    {
        ICollection<BankAccount> GetBankAccounts();
        BankAccount GetBankAccount(int id);

        BankAccount GetBankAccount(string cbu);

        ICollection<BankAccount> GetBankAccountsByUser(int userId);

        bool BankAccountExists(int id);
        bool BankAccountExists(string cbu);

        bool CreateBankAccount(BankAccount bankAccount);

        bool Withdraw(BankAccount bankAccount);

        bool Deposit(BankAccount bankAccount);

        bool DeleteBankAccount(BankAccount bankAccount);
        bool Save();

    }
}
