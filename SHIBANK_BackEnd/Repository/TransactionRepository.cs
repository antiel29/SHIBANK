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

        public Transaction? GetTransaction(int id)
        {
            return _context.Transactions.Where(t => t.Id == id).FirstOrDefault();
        }

        public ICollection<Transaction> GetTransactions()
        {
            return _context.Transactions.OrderBy(t => t.Id).ToList();
        }

        public ICollection<Transaction> GetSendedTransactionsByUsername(string username)
        {
            return _context.Transactions.Where(t => t.SourceUsername == username).ToList();
        }

        public ICollection<Transaction> GetRecievedTransactionsByUsername(string username)
        {
            return _context.Transactions.Where(t => t.DestinyUsername == username).ToList();
        }
        public ICollection<Transaction> GetAllTransactionsByUsername(string username)
        {
            return _context.Transactions.Where(t => t.DestinyUsername == username || t.SourceUsername == username).ToList();
        }
        public bool DeleteTransaction(Transaction transaction)
        {
            _context.Remove(transaction);
            return Save();
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
        public bool TransactionExists(string transactionCode)
        {
            return _context.Transactions.Any(t => t.TransactionCode == transactionCode);
        }
    }
}
