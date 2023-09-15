using System;
using System.Collections.Generic;
using System.Linq;
using SHIBANK.Data;
using SHIBANK.Models;

namespace SHIBANK.Data
{
    public class Seed
    {
        private readonly DataContext _context;

        public Seed(DataContext context)
        {
            _context = context;
        }

        public void SeedData()
        {
            if (!_context.Users.Any())
            {
                // Agrega usuarios de ejemplo
                var users = new List<User>
                {
                    new User
                    {
                        Username = "antiel_ilundayn",
                        Password = "password123",
                        FirstName = "Antiel",
                        LastName = "Ilundayn",
                        Email = "ilundayn29@gmail.com"
                    },
                    new User
                    {
                        Username = "pedro_punpun",
                        Password = "password456",
                        FirstName = "Pedro",
                        LastName = "Punpun",
                        Email = "foodlover@yahoo.com"
                    },
                    new User
                    {
                        Username = "kimika_tachibana",
                        Password = "password789",
                        FirstName = "Kimika",
                        LastName = "Tachibana",
                        Email = "SubarashikiHibi@hotmail.com"
                    }
                };

                _context.Users.AddRange(users);
                _context.SaveChanges();
            }

            if (!_context.BankAccounts.Any())
            {
                // Agrega cuentas bancarias de ejemplo
                var bankAccounts = new List<BankAccount>
                {
                    new BankAccount
                    {
                        AccountNumber = "123456789",
                        Balance = 10000.0m,
                        UserId = 1 
                    },
                    new BankAccount
                    {
                        AccountNumber = "987654321",
                        Balance = 750.0m,
                        UserId = 2
                    },
                    new BankAccount
                    {
                        AccountNumber = "555555555",
                        Balance = 0.0m,
                        UserId = 3 
                    }
                };

                _context.BankAccounts.AddRange(bankAccounts);
                _context.SaveChanges();
            }

            if (!_context.Transactions.Any())
            {
                // Agrega transacciones de ejemplo
                var transactions = new List<Transaction>
                {
                    new Transaction
                    {
                        Type = "Deposit",
                        Amount = 500.0m,
                        Date = DateTime.Now,
                        BankAccountId = 1 
                    },
                    new Transaction
                    {
                        Type = "Withdrawal",
                        Amount = 200.0m,
                        Date = DateTime.Now,
                        BankAccountId = 2 
                    },
                 
                };

                _context.Transactions.AddRange(transactions);
                _context.SaveChanges();
            }
        }
    }
}
