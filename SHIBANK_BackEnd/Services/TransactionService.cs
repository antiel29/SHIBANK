using SHIBANK.Dto;
using SHIBANK.Interfaces;
using SHIBANK.Models;

namespace SHIBANK.Services
{
    public class TransactionService : ITransactionService
    {

        public Transaction CreateTransactionOD(TransactionCreateDto transaction, BankAccount accountOrigin, BankAccount accountDestination)
        {
            

            var newTransaction = new Transaction
            {
                Type = transaction.Type,
                Amount = transaction.Amount,
                Date = DateTime.Now,
                BankAccountId = accountOrigin.Id
            };

            accountOrigin.Balance -= transaction.Amount;
            accountDestination.Balance += transaction.Amount;

            return newTransaction;
        }
    }
}
