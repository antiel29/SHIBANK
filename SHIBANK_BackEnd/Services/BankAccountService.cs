using SHIBANK.Interfaces;
using SHIBANK.Models;
using SHIBANK.Repository;
using SHIBANK.Helper;
using SHIBANK.Security;

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

        public bool BankAccountExists(string cbu)
        {
            string hashedCbu = Hashing.CalculateHash(cbu);
            return _bankAccountRepository.BankAccountExists(hashedCbu);
        }

        public BankAccount GetBankAccount(int id)
        {
            return _bankAccountRepository.GetBankAccount(id);
        }

        public BankAccount GetBankAccount(string cbu)
        {
            string hashedCbu = Hashing.CalculateHash(cbu);
            return _bankAccountRepository.GetBankAccount(hashedCbu);
        }

        public IEnumerable<BankAccount> GetBankAccountsByUser(int userId)
        {
            return _bankAccountRepository.GetBankAccountsByUser(userId);
        }

        public string GenerateUniqueAccountNumber()
        {
            string cbu = "";
            bool isUnique = false;

            while (!isUnique)
            {
                cbu = BankAccountHelper.GenerateRandomCbu();
                isUnique = !_bankAccountRepository.BankAccountExists(cbu);

            }
            return cbu;
        }


        public BankAccount CreateBankAccountForUser(int userId)
        {

            string cbu = GenerateUniqueAccountNumber();

            var newBankAccount = new BankAccount
            {
                UserId = userId,
                CBU = cbu,
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
