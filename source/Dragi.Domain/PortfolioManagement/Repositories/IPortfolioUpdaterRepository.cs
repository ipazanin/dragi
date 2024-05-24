using Dragi.Common.Results;
using Dragi.Domain.PortfolioManagement.Models;

namespace Dragi.Domain.PortfolioManagement.Repositories;

public interface IPortfolioUpdaterRepository
{
    public Task<Result<Portfolio>> UpdatePortfolio(Portfolio portfolio, CancellationToken cancellationToken);
}
