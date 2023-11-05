using SHIBANK.Interfaces;
using SHIBANK.Models;
using SHIBANK.Helper;
using SHIBANK.Security;
using SHIBANK.Enums;
using SHIBANK.Results;
using System.Collections.Generic;
using System;

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
                isUnique = !BankAccountExists(cbu);

            }
            return cbu;
        }

        public bool CreateBankAccount(int userId,BankAccountType type)
        {

            string cbu = GenerateUniqueAccountNumber();
            string hashedCbu = Hashing.CalculateHash(cbu);

            var newBankAccount = new BankAccount
            {
                CBU = hashedCbu,
                Balance = 0,
                Type = type,
                OpeningDate = DateTime.Now,
                UserId = userId,
            };
            if (type == BankAccountType.Savings)
            {
                newBankAccount.InterestGenerating = 0;
                newBankAccount.LastInterestDate = DateTime.Now;
                newBankAccount.Interest = 0.02m;
                newBankAccount.TransactionLimit = 5;
            }

            return _bankAccountRepository.CreateBankAccount(newBankAccount);
        }

        public Result MoveFunds(BankAccount source,BankAccount destiny,decimal amount)
        {
            if (source.Balance < amount)
                return new Result(false,"Insufficent funds.");

            if ( destiny.Type == BankAccountType.Savings )
            {
                if(destiny.TransactionCount == destiny.TransactionLimit)
                    return new Result(false, "Destiny has reach the transaction limit for this month");

                destiny.TransactionCount++;
            }
            if (source.Type == BankAccountType.Savings)
            {
                if (source.TransactionCount == source.TransactionLimit)
                    return new Result(false, "Source has reach the transaction limit for this month");

               source.TransactionCount++;
            }

            source.Balance -= amount;
            destiny.Balance += amount;

            var moveResult = _bankAccountRepository.MoveFunds(source, destiny);

            if (!moveResult)
    {
                return new Result(false, "Error trying to save");
            }

            return new Result(true,"");
        }

        public bool DeleteBankAccount(BankAccount bankAccount)
        {
            return _bankAccountRepository.DeleteBankAccount(bankAccount);
        }

        public IEnumerable<BankAccount> GetBankAccountsOfType(BankAccountType type)
        {
            return _bankAccountRepository.GetBankAccountsOfType(type);
        }

        public BankAccount GetUserBankAccountOfType(int userId, BankAccountType type)
        {
            return _bankAccountRepository.GetUserBankAccountOfType(userId, type);
        }

    }
}
