using SHIBANK.Data;
using SHIBANK.Enums;
using SHIBANK.Interfaces;
using SHIBANK.Models;

namespace SHIBANK.Background_Services
{
    public class AutomaticInterestService : IHostedService, IDisposable
    {
        private readonly IServiceProvider _serviceProvider;
        private Timer? _timer;

        public AutomaticInterestService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(DoWork, null, TimeSpan.Zero, 
                TimeSpan.FromHours(24));

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }

        private void DoWork(object? state)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var _bankAccountService = scope.ServiceProvider.GetRequiredService<IBankAccountService>();
                var dbContext = scope.ServiceProvider.GetRequiredService<DataContext>();

                var savingsAccounts = _bankAccountService.GetBankAccountsOfType(BankAccountType.Savings);

                foreach (var account in savingsAccounts)
                {
                    AddDailyInterest(account);

                    if(DateTime.Now.Subtract(account.LastInterestDate!.Value).Days == 30)
                    {
                        account.Balance += account.InterestGenerating!.Value;

                        account.TransactionCount = 0;
                        account.InterestGenerating = 0;
                        account.LastInterestDate = DateTime.Now;
                    }
                }
                dbContext.SaveChanges();
            }
        }

        void AddDailyInterest(BankAccount account)
        {
            var dailyInterest = account.Balance * (account.Interest / 30);
            account.InterestGenerating += dailyInterest;
        }
    }
}
