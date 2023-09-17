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

        public BankAccount GetBankAccount(int id)
        {
            return _context.BankAccounts.Where(a => a.Id == id).FirstOrDefault();
        }

        public ICollection<BankAccount> GetBankAccounts()
        {
           return _context.BankAccounts.OrderBy(a => a.Id).ToList();
        }

        public ICollection<BankAccount> GetBankAccountsByUser(int userId)
        {
            return _context.BankAccounts.Where(a => a.UserId == userId).ToList();
        }
    }
}
