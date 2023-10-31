using Microsoft.AspNetCore.Identity;
using SHIBANK.Models;

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
                            OriginAccountNumber = "1234567891",
                            DestinyAccountNumber = "5555555555",
                            BankAccountId = 1,
                        }
                    };

                    context.Transactions.AddRange(transactions);
                    context.SaveChanges();
                }
            }
        }
    }
}
