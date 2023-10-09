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
        public IEnumerable<BankAccount> GetBankAccounts()
        {
            return _bankAccountRepository.GetBankAccounts();
        }

        public bool BankAccountExists(int id)
        {
            return _bankAccountRepository.BankAccountExists(id);
        }

        public bool BankAccountExists(string accountNumber)
        {
            return _bankAccountRepository.BankAccountExists(accountNumber);
        }

        public BankAccount GetBankAccount(int id)
        {
            return _bankAccountRepository.GetBankAccount(id);
        }

        public BankAccount GetBankAccount(string accountNumber)
        {
            return _bankAccountRepository.GetBankAccount(accountNumber);
        }

        public IEnumerable<BankAccount> GetBankAccountsByUser(int userId)
        {
            return _bankAccountRepository.GetBankAccountsByUser(userId);
        }

        public string GenerateUniqueAccountNumber()
        {
            string chars = "0123456789";
            Random random = new Random();

            string accountNumber;
            bool isUnique;

            do 
            {
                accountNumber = new string(Enumerable.Repeat(chars, 10)
                    .Select(s => s[random.Next(s.Length)]).ToArray());

                isUnique = !_bankAccountRepository.BankAccountExists(accountNumber);

            } 
            while (!isUnique);

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
                throw new Exception("Error creating a bank account.");
            }

            return newBankAccount;
        }

        public bool Deposit(BankAccount bankAccount)
        {
            return _bankAccountRepository.Deposit(bankAccount);
        }

        public bool Withdraw(BankAccount bankAccount)
        {
            return _bankAccountRepository.Withdraw(bankAccount);
        }

        public bool DeleteBankAccount(BankAccount bankAccount)
        {
            return _bankAccountRepository.DeleteBankAccount(bankAccount);
        }

    }
}
