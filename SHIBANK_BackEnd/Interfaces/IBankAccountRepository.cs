﻿using SHIBANK.Models;

namespace SHIBANK.Interfaces
{
    public interface IBankAccountRepository
    {
        ICollection<BankAccount> GetBankAccounts();
        BankAccount GetBankAccount(int id);

        ICollection<BankAccount> GetBankAccountsByUser(int userId);

        bool BankAccountExists(int id);

    }
}
