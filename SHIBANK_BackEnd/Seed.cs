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
                var bankAccounts = new List<BankAccount>
                {
                    new BankAccount
                    {
                        AccountNumber = "1234567891",
                        Balance = 10000.0m,
                        UserId = 1 
                    },
                    new BankAccount
                    {
                        AccountNumber = "9876543212",
                        Balance = 0.0m,
                        UserId = 1
                    },
                    new BankAccount
                    {
                        AccountNumber = "5555555555",
                        Balance = 500.0m,
                        UserId = 2
                    }
                };

                _context.BankAccounts.AddRange(bankAccounts);
                _context.SaveChanges();
            }

            if (!_context.Transactions.Any())
            {
                var transactions = new List<Transaction>
                {
                    new Transaction
                    {
                        Message = "Giving 500 to punpun for his birthday!",
                        Amount = 500.0m,
                        Date = DateTime.Now,
                        OriginUsername = "antiel_ilundayn",
                        DestinyUsername = "pedro_punpun",
                        OriginAccountNumber = "1234567891",
                        DestinyAccountNumber = "5555555555",
                        BankAccountId = 1 
                    },            
                };

                _context.Transactions.AddRange(transactions);
                _context.SaveChanges();
            }
        }
    }
}
