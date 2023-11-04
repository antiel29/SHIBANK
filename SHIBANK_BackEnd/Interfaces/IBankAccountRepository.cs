using SHIBANK.Enums;
using SHIBANK.Models;

namespace SHIBANK.Interfaces
{
    public interface IBankAccountRepository
    {
        ICollection<BankAccount> GetBankAccounts();

        ICollection<BankAccount> GetBankAccountsOfType(BankAccountType type);

        BankAccount GetUserBankAccountOfType(int userId,BankAccountType type);
        BankAccount GetBankAccount(int id);

        BankAccount GetBankAccount(string cbu);

        ICollection<BankAccount> GetBankAccountsByUser(int userId);

        bool BankAccountExists(int id);
        bool BankAccountExists(string cbu);

        bool CreateBankAccount(BankAccount bankAccount);

        bool MoveFunds(BankAccount source,BankAccount destiny);

        bool DeleteBankAccount(BankAccount bankAccount);
        bool Save();

    }
}
