using Dragi.Common.Results;
using Dragi.Domain.PortfolioManagement.Models;

namespace Dragi.Domain.PortfolioManagement.Repositories;

public interface IPortfolioCreatorRepository
{
    public Task<Result<Portfolio>> CreatePortfolio(Portfolio portfolio, CancellationToken cancellationToken);
}
