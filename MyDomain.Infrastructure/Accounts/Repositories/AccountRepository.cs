using MyMediator.Application.Accounts.Repositories.Abstractions;
using MyMediator.Domain.Entities;

namespace MyDomain.Infrastructure.Accounts.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        public Task SaveAsync(Account account, CancellationToken cancellationToken = default)
        {
            Console.WriteLine($"Account {account.Id} has been saved.");
            return Task.CompletedTask;
        }
    }
}
