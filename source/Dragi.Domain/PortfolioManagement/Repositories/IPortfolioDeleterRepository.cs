using Dragi.Common.Results;
using Dragi.Domain.PortfolioManagement.Models;

namespace Dragi.Domain.PortfolioManagement.Repositories;

public interface IPortfolioDeleterRepository
{
    public Task<Result> DeletePortfolio(Portfolio portfolio, CancellationToken cancellationToken);
}
