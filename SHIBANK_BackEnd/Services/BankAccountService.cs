using SHIBANK.Interfaces;
using SHIBANK.Models;
using SHIBANK.Repository;

namespace SHIBANK.Services
{
    public class BankAccountService : IBankAccountService
    {
        private readonly IBankAccountRepository _bankAccountRepository;
        public BankAccountService(IBankAccountRepository bankAccountRepository)
        {
            _bankAccountRepository = bankAccountRepository;
        }


        public string GenerateUniqueAccountNumber()
        {
            string accountNumber;
            do
            {
                accountNumber = GenerateRandomAccountNumber();

            }
            while (_bankAccountRepository.GetBankAccounts()
            .Any(a => a.AccountNumber.Equals(accountNumber, StringComparison.OrdinalIgnoreCase)));
            {
                return accountNumber;
            }
        }
        public string GenerateRandomAccountNumber()
        {
            string chars = "0123456789";

            Random random = new Random();
            string accountNumber = new string(Enumerable.Repeat(chars, 10)
                .Select(s => s[random.Next(s.Length)]).ToArray());

           
            while (_bankAccountRepository.BankAccountExists(accountNumber))
            {
                accountNumber = new string(Enumerable.Repeat(chars, 10)
                    .Select(s => s[random.Next(s.Length)]).ToArray());
            }

            return accountNumber;
        }


        public BankAccount CreateBankAccountForUser(int userId)
        {

            string accountNumber = GenerateUniqueAccountNumber();

            var newBankAccount = new BankAccount
            {
                UserId = userId,
                AccountNumber = accountNumber,
                Balance = 0,
            };

            if (!_bankAccountRepository.CreateBankAccount(newBankAccount))
            {
                throw new Exception("Error al crear la cuenta bancaria.");
            }

            return newBankAccount;
        }
    }
}
