using SHIBANK.Models;

namespace SHIBANK.Interfaces
{
    public interface IBankAccountService
    {
        BankAccount CreateBankAccountForUser(int userId);
        string GenerateUniqueAccountNumber();

        string GenerateRandomAccountNumber();

    }
}
