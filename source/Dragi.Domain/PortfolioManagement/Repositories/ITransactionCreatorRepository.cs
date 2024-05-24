using Dragi.Common.Results;
using Dragi.Domain.PortfolioManagement.Models;

namespace Dragi.Domain.PortfolioManagement.Repositories;

public interface ITransactionCreatorRepository
{
    public Task<Result<Transaction>> CreateTransaction(Transaction transaction, CancellationToken cancellationToken);
}
