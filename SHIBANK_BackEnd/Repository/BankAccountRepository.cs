using SHIBANK.Data;
using SHIBANK.Interfaces;
using SHIBANK.Models;

namespace SHIBANK.Repository
{
    public class BankAccountRepository : IBankAccountRepository
    {
        private DataContext _context;
        public BankAccountRepository(DataContext context)
        {
            _context = context;
        }
        public bool BankAccountExists(int id)
        {
            return _context.BankAccounts.Any(a => a.Id == id);
        }

        public bool BankAccountExists(string accountNumber)
        {
            return _context.BankAccounts.Any(a=> a.AccountNumber == accountNumber);
        }

        public bool CreateBankAccount(BankAccount bankAccount)
        {
            _context.Add(bankAccount);
            return Save();
        }

        public bool DeleteBankAccount(BankAccount bankAccount)
        {
            _context.Remove(bankAccount);
            return Save();
        }

        public bool Deposit(BankAccount bankAccount)
        {
            _context.Update(bankAccount);
            return Save();
        }

        public BankAccount GetBankAccount(int id)
        {
            return _context.BankAccounts.Where(a => a.Id == id).FirstOrDefault();
        }

        public BankAccount GetBankAccount(string accountNumber)
        {
            return _context.BankAccounts.Where(a => a.AccountNumber == accountNumber).FirstOrDefault();
        }

        public ICollection<BankAccount> GetBankAccounts()
        {
           return _context.BankAccounts.OrderBy(a => a.Id).ToList();
        }

        public ICollection<BankAccount> GetBankAccountsByUser(int userId)
        {
            return _context.BankAccounts.Where(a => a.UserId == userId).ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Withdraw(BankAccount bankAccount)
        {
            _context.Update(bankAccount);
            return Save();

        }
    }
}
