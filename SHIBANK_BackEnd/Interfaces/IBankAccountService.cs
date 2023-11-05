using SHIBANK.Enums;
using SHIBANK.Models;
using SHIBANK.Results;

namespace SHIBANK.Interfaces
{
    public interface IBankAccountService
    {
        IEnumerable<BankAccount> GetBankAccounts();

        IEnumerable<BankAccount> GetBankAccountsOfType(BankAccountType type);

        BankAccount? GetUserBankAccountOfType(int userId, BankAccountType type);


        bool BankAccountExists(int id);

        bool BankAccountExists(string cbu);

        BankAccount? GetBankAccount(int id);

        BankAccount? GetBankAccount(string cbu);


        IEnumerable<BankAccount> GetBankAccountsByUser(int userId);

        Result MoveFunds(BankAccount source, BankAccount destiny,decimal amount);

        bool DeleteBankAccount(BankAccount bankAccount);
        bool CreateBankAccount(int userId,BankAccountType type);
        string GenerateUniqueAccountNumber();


    }
}
