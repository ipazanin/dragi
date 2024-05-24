using Dragi.Common.Results;
using Dragi.Domain.PortfolioManagement.Models;

namespace Dragi.Domain.PortfolioManagement.Repositories;

public interface IAssetReaderRepository
{
    public Task<Result<Asset>> GetAsset(string ticker, CancellationToken cancellationToken);
}
