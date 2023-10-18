using SHIBANK.Dto;
using SHIBANK.Interfaces;
using SHIBANK.Models;

namespace SHIBANK.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IBankAccountRepository _bankAccountRepository;
        private readonly IUserRepository _userRepository;

        public TransactionService(ITransactionRepository transactionRepository, IBankAccountRepository bankAccountRepository, IUserRepository userRepository)
        {
            _transactionRepository = transactionRepository;
            _bankAccountRepository = bankAccountRepository;
            _userRepository = userRepository;
        }
        public IEnumerable<Transaction> GetTransactions()
        {
            return _transactionRepository.GetTransactions();
        }

        public bool TransactionExists(int id)
        {
            return _transactionRepository.TransactionExists(id);
        }
        public Transaction GetTransaction(int id)
        {
            return _transactionRepository.GetTransaction(id);
        }

        public IEnumerable<Transaction> GetTransactionsByBankAccount(int bankAccountId)
        {
            return _transactionRepository.GetTransactionsByBankAccount(bankAccountId);
        }

        public IEnumerable<Transaction> GetTransactionsByBankAccount(string accountNumber)
        {
            return _transactionRepository.GetTransactionsByBankAccount(accountNumber);
        }

        public IEnumerable<Transaction> GetTransactionsByUsername(string username)
        {
            return _transactionRepository.GetTransactionsByUsername(username);
        }
        public ICollection<Transaction> GetTransactionsRecievedUsername(string username)
        {
            return _transactionRepository.GetTransactionsRecievedUsername(username);
        }

        public ICollection<Transaction> GetTransactionsRecieved(string accountNumber)
        {
            return _transactionRepository.GetTransactionsRecieved(accountNumber);
        }

        public Transaction CreateTransactionOD(TransactionCreateDto transaction, BankAccount origin, BankAccount destiny)
        {
            var originUsername = _userRepository.GetUser(origin.UserId).Username;
            var destinyUsername = _userRepository.GetUser(destiny.UserId).Username;
            var newTransaction = new Transaction
            {
                Amount = transaction.Amount,
                Date = DateTime.Now,
                Message = transaction.Message,
                OriginAccountNumber = origin.AccountNumber,
                OriginUsername = originUsername,
                DestinyAccountNumber = destiny.AccountNumber,
                DestinyUsername = destinyUsername,
                BankAccountId = origin.Id
            };

            origin.Balance -= transaction.Amount;
            destiny.Balance += transaction.Amount;

            return newTransaction;
        }

        public bool CreateTransaction(Transaction transaction)
        {
            return _transactionRepository.CreateTransaction(transaction);
        }
    }
}
