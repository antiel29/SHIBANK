using SHIBANK.Dto;
using SHIBANK.Helper;
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

        public bool TransactionExists(string transactionCode)
        {
            return _transactionRepository.TransactionExists(transactionCode);
        }
        public Transaction GetTransaction(int id)
        {
            return _transactionRepository.GetTransaction(id);
        }
        public string GenerateUniqueTransactionCode()
        {
            string transactionCode = "";
            bool isUnique = false;

            while (!isUnique)
            {
                transactionCode = TransactionHelper.GenerateRandomTransactionCode();
                isUnique = !TransactionExists(transactionCode);

            }
            return transactionCode;
        }

        public bool CreateTransaction(BankAccount origin, BankAccount destiny,TransactionCreateDto transactionDto)
        {
            var sourceUsername = _userRepository.GetUser(origin.UserId).UserName;

            var transaction = new Transaction
            {
                TransactionCode = GenerateUniqueTransactionCode(),
                Message = transactionDto.Message,
                Amount = transactionDto.Amount,
                Date = DateTime.Now,
                SourceUsername = sourceUsername,
                DestinyUsername = transactionDto.Username,
                BankAccountId = origin.Id
            };

            origin.Balance -= transaction.Amount;
            destiny.Balance += transaction.Amount;

            return _transactionRepository.CreateTransaction(transaction); ;
        }

        public IEnumerable<Transaction> GetUserRecievedTransactions(User user)
        {
            if (user.UserName == null) return Enumerable.Empty<Transaction>();
            return _transactionRepository.GetRecievedTransactionsByUsername(user.UserName);
        }

        public IEnumerable<Transaction> GetUserSendedTransactions(User user)
        {
            if (user.UserName == null) return Enumerable.Empty<Transaction>();
            return _transactionRepository.GetSendedTransactionsByUsername(user.UserName);
        }

        public IEnumerable<Transaction> GetUserAllTransactions(User user)
        {
            if (user.UserName == null) return Enumerable.Empty<Transaction>();
            return _transactionRepository.GetAllTransactionsByUsername(user.UserName);

        }
        public bool DeleteTransaction(Transaction transaction)
        {
            return _transactionRepository.DeleteTransaction(transaction);
        }
    }
}
