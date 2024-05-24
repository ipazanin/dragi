using Dragi.Common.Results;
using Dragi.Domain.PortfolioManagement.Models;

namespace Dragi.Domain.PortfolioManagement.Repositories;

public interface ITransactionReaderRepository
{
    public Task<Result<Transaction>> GetTransaction(int transactionId, CancellationToken cancellationToken);
}
