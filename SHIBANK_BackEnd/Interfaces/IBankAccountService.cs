using SHIBANK.Models;

namespace SHIBANK.Interfaces
{
    public interface IBankAccountService
    {
        IEnumerable<BankAccount> GetBankAccounts();

        bool BankAccountExists(int id);

        bool BankAccountExists(string accountNumber);

        BankAccount GetBankAccount(int id);

        BankAccount GetBankAccount(string accountNumber);


        IEnumerable<BankAccount> GetBankAccountsByUser(int userId);

        bool Deposit(BankAccount bankAccount);

        bool Withdraw(BankAccount bankAccount);

        bool DeleteBankAccount(BankAccount bankAccount);
        BankAccount CreateBankAccountForUser(int userId);
        string GenerateUniqueAccountNumber();


    }
}
