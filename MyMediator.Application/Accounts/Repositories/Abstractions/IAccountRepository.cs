using MyMediator.Application.Accounts.UseCases.Create;
using MyMediator.Domain.Entities;

namespace MyMediator.Application.Accounts.Repositories.Abstractions
{
    public interface IAccountRepository
    {
        Task SaveAsync(Account account, CancellationToken cancellationToken = default);
    }
}
