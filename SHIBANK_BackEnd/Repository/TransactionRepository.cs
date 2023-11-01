using SHIBANK.Data;
using SHIBANK.Interfaces;
using SHIBANK.Models;
namespace SHIBANK.Repository
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly DataContext _context;
        public TransactionRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreateTransaction(Transaction transaction)
        {
            _context.Add(transaction);
            return Save();
        }

        public Transaction GetTransaction(int id)
        {
            return _context.Transactions.Where(t => t.Id == id).FirstOrDefault();
        }

        public ICollection<Transaction> GetTransactions()
        {
            return _context.Transactions.OrderBy(t => t.Id).ToList();
        }

        public ICollection<Transaction> GetTransactionsByBankAccount(int bankAccountId)
        {
            return _context.Transactions.Where(t=> t.BankAccountId == bankAccountId).ToList();
        }


        public ICollection<Transaction> GetTransactionsByUsername(string username)
        {
            return _context.Transactions.Where(t => t.OriginUsername == username).ToList();
        }


        public ICollection<Transaction> GetTransactionsRecievedUsername(string username)
        {
            return _context.Transactions.Where(t => t.DestinyUsername == username).ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool TransactionExists(int id)
        {
            return _context.Transactions.Any(t => t.Id == id);
        }
    }
}
