using Microsoft.AspNetCore.Identity;
using SHIBANK.Models;
using SHIBANK.Security;
using SHIBANK.Helper;

//Nugget->
//Add-Migration InitialCreate
//Update-Database

//Terminal->
//dotnet run seeddata

namespace SHIBANK.Data
{
    public class Seed
    {
        private readonly IServiceProvider _serviceProvider;

        public Seed(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void SeedData()
        {
            var cbu1 = BankAccountHelper.GenerateRandomCbu();
            var cbu2 = BankAccountHelper.GenerateRandomCbu();

            while (cbu1 == cbu2) cbu2 = BankAccountHelper.GenerateRandomCbu();

            var hashedCbu1 = Hashing.CalculateHash(cbu1);
            var hashedCbu2 = Hashing.CalculateHash(cbu2);

            using (var scope = _serviceProvider.CreateScope())
            {
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<Role>>();
                var context = scope.ServiceProvider.GetRequiredService<DataContext>();

                if (!userManager.Users.Any())
                {
                    if (!roleManager.Roles.Any())
                    {
                        roleManager.CreateAsync(new Role{ Name = "admin" }).Wait();
                        roleManager.CreateAsync(new Role{ Name = "user" }).Wait();
                    }

                    var adminUser = new User
                    {
                        UserName = "antiel_ilundayn",
                        FirstName = "Antiel",
                        LastName = "Ilundayn",
                        Email = "ilundayn29@gmail.com",
                        Role = "admin",
                        SecurityStamp = Guid.NewGuid().ToString()
                    };

                    var result = userManager.CreateAsync(adminUser, "Password123").Result;
                    if (result.Succeeded)
                    {
                        
                        userManager.AddToRoleAsync(adminUser, "admin").Wait();
                    }


                    var user1 = new User
                    {
                        UserName = "pedro_punpun",
                        FirstName = "Pedro",
                        LastName = "Punpun",
                        Email = "foodlover@yahoo.com",
                        SecurityStamp = Guid.NewGuid().ToString()
                    };

                    result = userManager.CreateAsync(user1, "Password456").Result;
                    if (result.Succeeded)
                    {
                        
                        userManager.AddToRoleAsync(user1, "user").Wait();
                    }

                    var user2 = new User
                    {
                        UserName = "kimika_tachibana",
                        FirstName = "Kimika",
                        LastName = "Tachibana",
                        Email = "SubarashikiHibi@hotmail.com",
                        SecurityStamp = Guid.NewGuid().ToString()
                    };

                    result = userManager.CreateAsync(user2, "Password789").Result;
                    if (result.Succeeded)
                    {

                        userManager.AddToRoleAsync(user2, "user").Wait();
                    }
                }

                if (!context.BankAccounts.Any())
                {

                    var bankAccounts = new List<BankAccount>
                    {
                        new BankAccount
                        {
                            CBU = hashedCbu1,
                            Balance = 10000.0m,
                            UserId = 1
                        },
                        new BankAccount
                        {
                            CBU = hashedCbu2,
                            Balance = 500.0m,
                            UserId = 2
                        }
                    };

                    context.BankAccounts.AddRange(bankAccounts);
                    context.SaveChanges();
                }

                if (!context.Transactions.Any())
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
                            OriginCBU = hashedCbu1,
                            DestinyCBU = hashedCbu2,
                            BankAccountId = 1,
                        }
                    };

                    context.Transactions.AddRange(transactions);
                    context.SaveChanges();
                }
                if (!context.Cards.Any())
                {
                    var cardNumber1 = CardHelper.GenerateRandomCardNumber();
                    var cvc1 = CardHelper.GenerateRandomCvc();

                    var cards = new List<Card>
                    {
                        new Card
                        {
                            Type = Enums.CardType.Debit,
                            CardNumber = Hashing.CalculateHash(cardNumber1),
                            ExpirationDate = DateTime.UtcNow.AddYears(5),
                            CVC = Hashing.CalculateHash(cvc1),
                            Limit = 0,
                            BankAccountId = 1
                        }
                    };
                    context.Cards.AddRange(cards);
                    context.SaveChanges();
                }   
            
            }
        }
    }
}
