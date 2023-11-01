using SHIBANK.Models;

namespace SHIBANK.Interfaces
{
    public interface IBankAccountService
    {
        IEnumerable<BankAccount> GetBankAccounts();

        bool BankAccountExists(int id);

        bool BankAccountExists(string cbu);

        BankAccount GetBankAccount(int id);

        BankAccount GetBankAccount(string cbu);


        IEnumerable<BankAccount> GetBankAccountsByUser(int userId);

        bool Deposit(BankAccount bankAccount);

        bool Withdraw(BankAccount bankAccount);

        bool DeleteBankAccount(BankAccount bankAccount);
        BankAccount CreateBankAccountForUser(int userId);
        string GenerateUniqueAccountNumber();


    }
}
