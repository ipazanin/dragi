using Dragi.Common.Results;
using Dragi.Domain.PortfolioManagement.Models;

namespace Dragi.Domain.PortfolioManagement.Repositories;

public interface IPortfolioReaderRepository
{
    public Task<Result<Portfolio>> GetPortfolio(string name, CancellationToken cancellationToken);

    public Task<Result<IReadOnlyList<Portfolio>>> GetPortfolios(CancellationToken cancellationToken);
}
