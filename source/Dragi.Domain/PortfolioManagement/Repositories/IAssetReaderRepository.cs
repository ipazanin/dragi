using Dragi.Common.Results;
using Dragi.Domain.PortfolioManagement.Models;
using Dragi.Domain.PortfolioManagement.Services;

namespace Dragi.Domain.PortfolioManagement.Repositories;

public interface IAssetReaderRepository
{
    public Task<Result<Asset>> GetAsset(string ticker, CancellationToken cancellationToken);
}
