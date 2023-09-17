using SHIBANK.Dto;
using SHIBANK.Models;

namespace SHIBANK.Interfaces
{
    public interface ITransactionService 
    {
        Transaction CreateTransactionOD(TransactionCreateDto transaction,BankAccount AccountOrigin,BankAccount AccountDestination);

    }
}
